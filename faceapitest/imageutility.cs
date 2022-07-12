using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace faceapitest
{
    public class imageutility
    {
        public byte[] converttobyte(string imagepath)
        {
            MemoryStream ms = new MemoryStream();
            using (FileStream fs = new FileStream(imagepath, FileMode.Open))
            {
                fs.CopyTo(ms);
            }
            var bytes = ms.ToArray();
            return bytes;
        }
        public void frombytestoimage(byte[] imagebytes,string filename)
        {
            using (MemoryStream ms = new MemoryStream(imagebytes))
            {
                Image img = Image.FromStream(ms);
                img.Save(filename + ".jpg", ImageFormat.Jpeg);
            }
             
        }
    }
}
