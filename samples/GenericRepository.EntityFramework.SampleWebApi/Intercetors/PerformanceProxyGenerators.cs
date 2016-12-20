using MultiTenantServices.ServiceLogger;

namespace MultiTenantRepositry.EF.Api.Intercetors
{
    internal class PerfLogger : ILogger
    {
        public void Log(string category, string message, TraceLevel level)
        {
            switch (level)
            {
                default:
                case TraceLevel.Info:
                case TraceLevel.Verbose:
                    System.Diagnostics.Trace.TraceInformation(message);
                    return;
                case TraceLevel.Error:
                case TraceLevel.Fatal:
                    System.Diagnostics.Trace.TraceError(message);
                    return;
                case TraceLevel.Warning:
                    System.Diagnostics.Trace.TraceWarning(message);
                    return;
            }
        }
    }
}