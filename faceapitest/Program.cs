using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
namespace faceapitest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var imagepath = @"image.jpg";
            var urladdress = "http://localhost:8000/api/faces";
            imageutility imgutil = new imageutility();
            var bytes = imgutil.converttobyte(imagepath);
            List<byte[]> facelist = null;
            var bytecontent = new ByteArrayContent(bytes);
            bytecontent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.PostAsync(urladdress, bytecontent))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    facelist = JsonConvert.DeserializeObject<List<byte[]>>(apiresponse);
                }
            }

            if (facelist.Count > 0)
            {
                for(int i = 0; i < facelist.Count; i++)
                {
                    imgutil.frombytestoimage(facelist[i], "face" + i);
                }
            }

        }
    }
}