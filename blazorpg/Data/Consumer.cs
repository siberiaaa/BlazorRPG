using System.Net;
using System.Text;
using blazorpg.Data.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace blazorpg.Data;
public class Consumer
{
    public readonly ProtectedLocalStorage _protectedLocalStorage;
    public Consumer(ProtectedLocalStorage protectedLocalStorage)
    {
        _protectedLocalStorage = protectedLocalStorage;
    }

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

    public static async Task<Response<Tout>> Execute<Tin, Tout>(string url, methodHttp method, Tin objectRequest, string jwt = "")
    {

        Response<Tout> response = new Response<Tout>();
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

                if (jwt != "")
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);
                }

                using (HttpResponseMessage res = await client.SendAsync(request))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            if (typeof(Tout) == typeof(string)) //JsonConvert tonto da error al deserializar strings
                            {
                                response.Data = (Tout)Convert.ChangeType(data, typeof(Tout));
                            }
                            else
                            {
                                response.Data = JsonConvert.DeserializeObject<Tout>(data);
                            }
                        }

                        response.StatusCode = res.StatusCode.ToString();


                        if (res.IsSuccessStatusCode)
                        {
                            response.Ok = true;
                        }
                      

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
            response.Ok = false; //?
            if (!response.Ok) response.Message = $"Response not OK\nStatus code: {response.StatusCode}"; //asd
        }
        catch (JsonSerializationException) //Invalid token 
        {
            //"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImhvbGEiLCJpZCI6IjEiLCJuYmYiOjE3MTA5Nzc3ODIsImV4cCI6MTcxMDk3ODM4MiwiaWF0IjoxNzEwOTc3NzgyfQ.Mw8nvDYQpZh44GVak1i8IyTyk4K_Bpu_fqUU7T4RKTw"

            response.StatusCode = "Token invalid";
            response.Message = "Token invalid error, sign in again";
            response.Ok = false; 
        }
        catch (Exception ex)
        {
            response.StatusCode = "App error";
            response.Message = ex.Message;
            response.Ok = false;
        }

        return response;


    }
}