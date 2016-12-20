namespace MultiTenantServices.ServiceLogger
{
    public class TraceLogger : ILogger
    {
        public void Log(string category, string message, TraceLevel level)
        {
            System.Diagnostics.Trace.WriteLine(message, category);
        }
    }
}
