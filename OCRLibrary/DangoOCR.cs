using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Graphics;

namespace OCRLibrary
{
    public class DangoOCR : OCREngine
    {
        private class DangoOCREngine
        {
            public Bitmap AddImageBorder(Bitmap img)
            {
                var cwidth = 3;
                var cnwidth = cwidth *2;
                var nwidth = img.Width + cnwidth;
                var nheight = img.Height + cnwidth;
                var bdcolor = Color.Black;

                var nimg = new Bitmap(nwidth, nheight);
                var g = Graphics.FromImage(nimg);
                var rec = new Rectangle(cwidth, cwidth, nwidth - cwidth, nheight - cwidth);
                g.DrawImage(img, rec, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
                g.DrawRectangle(new Pen(bdcolor, cnwidth), 0, 0, nwidth, nheight);
                g.Dispose();
                return nimg;
            }
        }

        // https://github.com/PantsuDango/DangoOCR/blob/master/app.py#L31
        private class JsonSuccess
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public string RequestId { get; set; }
            public List<Datam> Data { get; set; }
        }
        private class Datam
        {
            public Coordinatem Coordinate { get; set; }
            public string Words { get; set; }
            public float Score { get; set; }
        }
        private class Coordinatem
        {
            public List<float> UpperLeft { get; set; }
            public List<float> UpperRight { get; set; }
            public List<float> LowerRight { get; set; }
            public List<float> LowerLeft { get; set; }
        }

        private string srcLangCode;
        // private OcrEngine dangoOcr;
        private HttpClient hc;

        public override async Task<string> OCRProcessAsync(Bitmap img)
        {
            try
            {
                var filedir = Environment.CurrentDirectory;
                var filetime = DateTime.Now.ToFileTime().ToString();
                var filename = filedir + "\\jpgs\\" + filetime + ".jpg";
                img.Save(filename, ImageFormat.Jpeg);

                var jstr = JsonConvert.SerializeObject(new Dictionary<string, string>()
                {
                    { "ImagePath" , filename },
                    { "Language" , srcLangCode }
                });
                var req = new StringContent(jstr, Encoding.UTF8, "application/json");

                var resp = await hc.PostAsync("http://localhost:6666/ocr/api", req);
                File.Delete(filename);

                var content = await resp.Content.ReadAsStringAsync();
                var jc = JsonConvert.DeserializeObject<JsonSuccess>(content);
                var chara = "null";
                for (int i = 0; i < jc.Data.Count; i++)
                {
                    if (chara == "null")
                    {
                        chara = "";
                    }
                    chara += jc.Data[i].Words + " ";
                }
                return Task.FromResult(chara).Result;
                // return Task.FromResult(content).Result;
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                return Task.FromResult(string.Empty).Result;
            }
        }

        public override bool OCR_Init(string param1 = "", string param2 = "")
        {
            hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return true;
        }

        public override void SetOCRSourceLang(string lang)
        {
            if (lang == "jpn")
            {
                srcLangCode = "JAP";
            }
            else if (lang == "eng")
            {
                srcLangCode = "ENG";
            }
        }
    }
}
