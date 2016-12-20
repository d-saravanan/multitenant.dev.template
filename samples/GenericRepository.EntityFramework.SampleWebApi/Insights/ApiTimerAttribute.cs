using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MultiTenantRepositry.EF.Api.Insights
{
    public class ApiTimerAttribute : ActionFilterAttribute
    {
        private const string _timerKey = "__api_timer__";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            actionContext.Request.Properties[_timerKey] = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            var stopWatch = actionExecutedContext.Request.Properties[_timerKey] as Stopwatch;
            if (stopWatch != null)
            {
                Trace.WriteLine("Time elapsed in Milliseconds: " + stopWatch.ElapsedMilliseconds + "\n", "performanceLogger");
            }
        }
    }
}