using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using log4net;

namespace UnionSwiss.Api.Controllers.Api.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        protected ILog Log { get; set; }

        public ApiExceptionFilter()
        {
            Log = log4net.LogManager.GetLogger(GetType());
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            try
            {

                var exception = actionExecutedContext.Exception;

                if (exception == null)
                {
                    base.OnException(actionExecutedContext);
                    return;
                }

                Log.Error(exception.Message, exception);

                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(exception.Message)
                };

                var httpException = exception as HttpException;
                if (httpException != null)
                {
                    response.StatusCode = (HttpStatusCode) httpException.GetHttpCode();
                    response.Content = new StringContent(httpException.Message);
                    response.ReasonPhrase = exception.Message;
                }
                ;

                actionExecutedContext.Response = response;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

        }
    }
}