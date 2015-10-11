﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Azure.WebJobs.Host.Listeners;

namespace Microsoft.Azure.WebJobs.Extensions.Monitoring.Listener
{
    internal class ErrorTriggerListener : IListener
    {
        private const string ErrorHandlerSuffix = "ErrorHandler";
        private readonly JobHostConfiguration _config;
        private readonly TraceMonitor _traceMonitor;

        public ErrorTriggerListener(JobHostConfiguration config, ParameterInfo parameter, ITriggeredFunctionExecutor executor)
        {
            _config = config;
            _traceMonitor = CreateTraceMonitor(parameter, executor);
        }

        internal static TraceMonitor CreateTraceMonitor(ParameterInfo parameter, ITriggeredFunctionExecutor executor)
        {
            ErrorTriggerAttribute attribute = parameter.GetCustomAttribute<ErrorTriggerAttribute>(inherit: false);

            // Determine whether this is a method level filter, and if so, create the filter
            Func<TraceEvent, bool> methodFilter = null;
            MethodInfo method = (MethodInfo)parameter.Member;
            string message = null;
            if (method.Name.EndsWith(ErrorHandlerSuffix, StringComparison.OrdinalIgnoreCase))
            {
                string sourceMethodName = method.Name.Substring(0, method.Name.Length - ErrorHandlerSuffix.Length);
                MethodInfo sourceMethod = method.DeclaringType.GetMethod(sourceMethodName);
                if (sourceMethod != null)
                {
                    string sourceMethodFullName = string.Format("{0}.{1}", method.DeclaringType.FullName, sourceMethod.Name);
                    methodFilter = p =>
                    {
                        FunctionInvocationException functionException = p.Exception as FunctionInvocationException;
                        return p.Level == System.Diagnostics.TraceLevel.Error && functionException != null &&
                               string.Compare(functionException.MethodName, sourceMethodFullName, StringComparison.OrdinalIgnoreCase) == 0;
                    };

                    string sourceMethodShortName = string.Format("{0}.{1}", method.DeclaringType.Name, sourceMethod.Name);
                    message = string.Format("Function '{0}' failed.", sourceMethodShortName);
                }
            }

            message = message ?? attribute.Message;

            // Create the TraceFilter instance
            TraceFilter traceFilter = null;
            if (attribute.FilterType != null)
            {
                if (methodFilter != null)
                {
                    TraceFilter innerTraceFilter = (TraceFilter)Activator.CreateInstance(attribute.FilterType);
                    traceFilter = new CompositeTraceFilter(innerTraceFilter, methodFilter, message);
                }
                else
                {
                    traceFilter = (TraceFilter)Activator.CreateInstance(attribute.FilterType);
                }
            }
            else if (!string.IsNullOrEmpty(attribute.Window))
            {
                TimeSpan window = TimeSpan.Parse(attribute.Window);
                traceFilter = new SlidingWindowTraceFilter(window, attribute.Threshold, methodFilter, message);
            }
            else
            {
                traceFilter = TraceFilter.Create(methodFilter, message);
            }
            TraceMonitor traceMonitor = new TraceMonitor(System.Diagnostics.TraceLevel.Error).Filter(traceFilter);

            // Apply any additional monitor options
            if (!string.IsNullOrEmpty(attribute.Throttle))
            {
                TimeSpan throttle = TimeSpan.Parse(attribute.Throttle);
                traceMonitor.Throttle(throttle);
            }

            // Subscribe the TraceFilter to the event stream
            traceMonitor.Subscribe(p =>
            {
                TriggeredFunctionData triggerData = new TriggeredFunctionData
                {
                    TriggerValue = p
                };
                Task<FunctionResult> task = executor.TryExecuteAsync(triggerData, CancellationToken.None);
                task.Wait();
            });

            return traceMonitor;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _config.Tracing.Tracers.Add(_traceMonitor);

            return Task.FromResult(true);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _config.Tracing.Tracers.Remove(_traceMonitor);

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            // TODO: Perform any final cleanup
        }

        public void Cancel()
        {
            // TODO: cancel any outstanding tasks initiated by this listener
        }

        internal class CompositeTraceFilter : TraceFilter
        {
            private readonly string _message;
            private readonly Func<TraceEvent, bool> _predicate;

            public CompositeTraceFilter(TraceFilter innerTraceFilter, Func<TraceEvent, bool> predicate = null, string message = null)
            {
                InnerTraceFilter = innerTraceFilter;
                _predicate = predicate;
                _message = message;
            }

            public TraceFilter InnerTraceFilter { get; private set; }

            public override string Message
            {
                get
                {
                    return _message ?? InnerTraceFilter.Message;
                }
            }

            public override Collection<TraceEvent> Traces
            {
                get
                {
                    return InnerTraceFilter.Traces;
                }
            }

            public override bool Filter(TraceEvent traceEvent)
            {
                if (_predicate(traceEvent))
                {
                    return InnerTraceFilter.Filter(traceEvent);
                }

                return false;
            }
        }
    }
}
