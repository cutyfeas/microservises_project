using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenCvSharp;

namespace facesapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class facesController : ControllerBase
    {
        [HttpPost]
        public async Task<List<byte[]>> readfaces()
        {
            using (var ms = new MemoryStream(2048))
            {
                await Request.Body.CopyToAsync(ms);
                var faces = GetFaces(ms.ToArray());
                return faces;
            }
        }
        private List<byte[]> GetFaces(byte[] image)
        {
            Mat src = Cv2.ImDecode(image, ImreadModes.Color);
            //convert byte array into jpeg image and save the image comming from the source
            //in the root directory for testing purpose
            src.SaveImage("image.jpg", new ImageEncodingParam(ImwriteFlags.JpegProgressive, 255));
            var file = Path.Combine(Directory.GetCurrentDirectory(), "cascadefile", "haarcascade_frontalface_default.xml");
            var facecascade = new CascadeClassifier();
            facecascade.Load(file);
            var faces = facecascade.DetectMultiScale(src, 1.1, 6, HaarDetectionTypes.DoRoughSearch, new Size(60, 60));
            var faselist = new List<byte[]>();
            int j = 0;
            foreach (var rect in faces)
            {
                var faceimage = new Mat(src, rect);
                faselist.Add(faceimage.ToBytes(".jpg"));
                faceimage.SaveImage("face" + j+".jpg", new ImageEncodingParam(ImwriteFlags.JpegProgressive, 255));
                j++;
            }
            return faselist;
        }
    }
}
