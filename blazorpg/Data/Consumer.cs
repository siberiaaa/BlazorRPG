using System.Net;
using System.Text;
using blazorpg.Data.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace blazorpg.Data;
public class Consumer
    {
        private static HttpMethod CreateHttpMethod(methodHttp method)
        {
            switch (method)
            {
                case methodHttp.GET:
                    return HttpMethod.Get;
                case methodHttp.POST:
                    return HttpMethod.Post;
                case methodHttp.PUT:
                    return HttpMethod.Put;
                case methodHttp.DELETE:
                    return HttpMethod.Delete;
                default:
                    throw new NotImplementedException("Not implemented http method");
            }
        }


        public static async Task<Response<T>> Execute<T>(string url, methodHttp method, T objectRequest)
        {
            Response<T> response = new Response<T>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    
                    var myContent = JsonConvert.SerializeObject((method != methodHttp.GET) ? method != methodHttp.DELETE ? objectRequest : "" : "");
                    //var myContent = JsonConvert.SerializeObject(objectRequest);
                    var bytecontent = new ByteArrayContent(Encoding.UTF8.GetBytes(myContent));
                    bytecontent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    //Si es get o delete no le mandamos bytecontent. Tremenda línea.
                    var request = new HttpRequestMessage(CreateHttpMethod(method), url)
                    {
                        //Por regla general de las peticiones HTTP las peticiones tipo GET y DELETE no se le puede establecer el body
                        //Entonces valido, si method es distinta de GET y DELETE le asigno el contenido codificado, sino le asigno null
                        Content = (method != methodHttp.GET) ? method != methodHttp.DELETE ? bytecontent : null : null
                    };
    
                    using (HttpResponseMessage res = await client.SendAsync(request))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            if (data != null)
                                response.Data = JsonConvert.DeserializeObject<T>(data);
    
                            response.StatusCode = res.StatusCode.ToString();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                response.StatusCode = "ServerError";
                var res = (HttpWebResponse)ex.Response;
                if (res != null)
                    response.StatusCode = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                response.StatusCode = "AppError";
                response.Message = ex.Message;
            }
            return response;


        }
    }