namespace MultiTenantServices.ServiceLogger
{
    public interface ILogger
    {
        void Log(string category, string message, TraceLevel level);
    }

    public enum TraceLevel : int
    {
        Info = 0,
        Verbose,
        Error,
        Warning,
        Fatal
    }
}
