// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Code Analysis results, point to "Suppress Message", and click 
// "In Suppression File".
// You do not need to add suppressions to this file manually.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "WebJobs", Scope = "member", Target = "ExtensionsSample.SendGridSamples.#MailOnQueueMessage(ExtensionsSample.Order,SendGrid.SendGridMessage&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "WebJobs", Scope = "member", Target = "ExtensionsSample.SendGridSamples.#BindToSendGridMessage(ExtensionsSample.Order,SendGrid.SendGridMessage&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "WebJobs", Scope = "member", Target = "ExtensionsSample.SendGridSamples.#ProcessOrder(ExtensionsSample.Order,SendGrid.SendGridMessage)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "HookA", Scope = "member", Target = "ExtensionsSample.WebHookSamples.#HookA(System.String,Microsoft.Azure.WebJobs.Host.TraceWriter)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "HookC", Scope = "member", Target = "ExtensionsSample.WebHookSamples.#HookC(ExtensionsSample.Order,Microsoft.Azure.WebJobs.Host.TraceWriter)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OrderId", Scope = "member", Target = "ExtensionsSample.WebHookSamples.#HookC(ExtensionsSample.Order,Microsoft.Azure.WebJobs.Host.TraceWriter)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "HookC", Scope = "member", Target = "ExtensionsSample.WebHookSamples.#HookC(ExtensionsSample.Order,SendGrid.SendGridMessage,Microsoft.Azure.WebJobs.Host.TraceWriter)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OrderId", Scope = "member", Target = "ExtensionsSample.WebHookSamples.#HookC(ExtensionsSample.Order,SendGrid.SendGridMessage,Microsoft.Azure.WebJobs.Host.TraceWriter)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2241:Provide correct arguments to formatting methods", Scope = "member", Target = "ExtensionsSample.FileSamples.#ImportFileErrorHandler(Microsoft.Azure.WebJobs.Host.TraceEvent,System.IO.TextWriter)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Scope = "member", Target = "ExtensionsSample.SendGridSamples.#ProcessOrder_Declarative(ExtensionsSample.Order,SendGrid.SendGridMessage&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Scope = "member", Target = "ExtensionsSample.SendGridSamples.#ProcessOrder_Imperative(ExtensionsSample.Order,SendGrid.SendGridMessage&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass", Scope = "member", Target = "ExtensionsSample.ShutdownHandler.#SetConsoleCtrlHandler(ExtensionsSample.ShutdownHandler+ConsoleEventDelegate,System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1414:MarkBooleanPInvokeArgumentsWithMarshalAs", Scope = "member", Target = "ExtensionsSample.ShutdownHandler.#SetConsoleCtrlHandler(ExtensionsSample.ShutdownHandler+ConsoleEventDelegate,System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate", Scope = "member", Target = "ExtensionsSample.EasyTableSamples.#InsertItem(Microsoft.Azure.WebJobs.TimerInfo,System.Object&)")]