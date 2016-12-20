using Castle.DynamicProxy;
using MultiTenantServices.ServiceLogger;
using System;
using System.Diagnostics;

namespace MultiTenantRepositry.EF.Api.Intercetors
{
    /// <summary>
    /// Method interceptors
    /// </summary>
    /// <seealso cref="Castle.DynamicProxy.IInterceptor" />
    public class MethodInterceptors : IInterceptor
    {
        private readonly ILogger _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodInterceptors"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public MethodInterceptors(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Intercepts the specified invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            _logger.Log("Performance", $"{invocation.Method.Name} is called at {DateTime.Now}", MultiTenantServices.ServiceLogger.TraceLevel.Info);
            var sw = Stopwatch.StartNew();
            invocation.Proceed();
            sw.Stop();
            _logger.Log("Performance", $"{invocation.Method.Name} took {sw.ElapsedMilliseconds} milliseconds to execute", MultiTenantServices.ServiceLogger.TraceLevel.Info);
        }
    }
}