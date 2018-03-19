using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;

namespace Web.Controllers
{
    public class BaseController : ApiController
    {

        public BaseController()
        {

        }


        [ApiExplorerSettings(IgnoreApi = true)]
        protected void ThrowHttpException(HttpStatusCode code, string message)
        {
            var response = new HttpResponseMessage(code)
            {
                Content = new StringContent(message)
            };
            throw new HttpResponseException(response);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected HttpResponseException CreateModelStateException()
        {
            var errorList = ModelState.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.Errors.Select(e => e.Exception?.Message?.ToString() ?? e.ErrorMessage).ToArray()
        );
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.Content = new ObjectContent<Dictionary<string, string[]>>(errorList, new JsonMediaTypeFormatter(), "application/json");
            return new HttpResponseException(response);
        }
    }
}