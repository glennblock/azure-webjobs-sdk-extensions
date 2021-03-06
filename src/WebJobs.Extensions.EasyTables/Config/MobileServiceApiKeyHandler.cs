﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Azure.WebJobs.Extensions.EasyTables
{
    /// <summary>
    /// A <see cref="DelegatingHandler"/> that adds a ZUMO-API-KEY header to every request.
    /// </summary>
    internal class MobileServiceApiKeyHandler : DelegatingHandler
    {
        internal const string ZumoApiKeyHeaderName = "ZUMO-API-KEY";
        private readonly string _apiKey;

        public MobileServiceApiKeyHandler(string apiKey)
        {
            _apiKey = apiKey;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            request.Headers.Add(ZumoApiKeyHeaderName, _apiKey);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
