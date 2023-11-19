using System;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SampleCode
{
	public class Common
	{
        public static JsonResult GetResult<T>(T result, ErrorCode errorCode) where T : class
        {
            if (ErrorCode.ErrorNone != errorCode)
            {
                var jsonResult = result as JsonApiResult;
                if (null != jsonResult)
                {
                    jsonResult.Result = false;
                    jsonResult.ErrorCode = errorCode.GetHashCode();
                }
            }

            return new JsonResult(result);
        }


        public static async Task<HttpStatusCode> PostRequestAsync<T>(string url, T obj, Action<string> data) where T : class
        {
            HttpStatusCode error_cdoe = HttpStatusCode.NotFound;
            
            byte[] contentsBytes = Serializer.SerializeBytes(obj);
            int contentsLenth = contentsBytes.Length;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Timeout = 1000000;
            request.ContentType = "application/json";//"application/x-www-form-urlencoded";
            request.ContentLength = contentsLenth;
            request.Proxy = null;

            try
            {
                using(Stream stream = request.GetRequestStream())
                {
                    stream.Write(contentsBytes, 0, contentsLenth);
                    stream.Close();
                }

                using(HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(false))
                {
                    error_cdoe = response.StatusCode;
                    if (HttpStatusCode.OK != error_cdoe)
                    {
                        //error 
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return error_cdoe;
        }

        public class Serializer
        {
            public static string Serialize<T>(T data) where T : class
            {
                try
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject(data);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                return null;
            }

            public static byte[] SerializeBytes<T>(T info) where T : class
            {
                try
                {
                    var buffer = System.Text.Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(info));
                    return buffer;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }


                return null;
            }

            public static T Deserialize<T>(string info) where T : class
            {
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(info);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                return default(T);
            }
        }
    }
}

