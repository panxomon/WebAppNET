using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Web.App.Validate
{
    public class ResponseWrappingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {            
            var response = await base.SendAsync(request, cancellationToken);

            return BuildApiResponse(request, response);
        }

        private HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
        { 
            object content;
            var modelStateErrors = new List<string>();

            var newResponse = request.CreateResponse(response.StatusCode, new ResponseModel(modelStateErrors));

            if (response.TryGetContentValue(out content) && !response.IsSuccessStatusCode)
            {
                var error = content as HttpError;

                if (error != null)
                {
                    content = null;

                    if (error.ModelState != null)
                    {
                        var erroresHttp = response.Content.ReadAsStringAsync().Result;

                        var anonymousErrorObject = new { message = string.Empty, ModelState = new Dictionary<string, string[]>() };

                        var deserializedErrorObject = JsonConvert.DeserializeAnonymousType(erroresHttp, anonymousErrorObject);

                        var modelStateValues = deserializedErrorObject.ModelState.Select(kvp => string.Join(". ", kvp.Value));

                        for (int i = 0; i < modelStateValues.Count(); i++)
                        {
                            modelStateErrors.Add(modelStateValues.ElementAt(i));
                        }
                    }
                    else if(error.ModelState == null)
                    {
                        var erroresHttp = response.Content.ReadAsStringAsync().Result;

                        var anonymousErrorObject = new
                        {
                            message = string.Empty,
                            ModelState = new Dictionary<string, string[]>()
                        };

                        var deserializedErrorObject = JsonConvert.DeserializeAnonymousType(erroresHttp, anonymousErrorObject);

                        newResponse.Content = new StringContent("{ \"Error\": " + erroresHttp.First() + "}");

                    }
                }
            }
            else if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                newResponse.Content = new StringContent("{ \"Error\": \"No tiene permiso para terminar la accion.\" }");
            }            
            else if(response.IsSuccessStatusCode)
            {
                return response;
            }
            
            foreach (var header in response.Headers)
            {
                newResponse.Headers.Add(header.Key, header.Value);
            }            

            return newResponse;
        }
    }
}