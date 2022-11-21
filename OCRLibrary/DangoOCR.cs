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

namespace OCRLibrary
{
    public class DangoOCR : OCREngine
    {
        public string srcLangCode;
        // private OcrEngine dangoOcr;

        public override async Task<string> OCRProcessAsync(Bitmap img)
        {
            try
            {
                var filedir = Environment.CurrentDirectory;
                var filetime = DateTime.Now.ToFileTime().ToString();
                var filename = string.Join(filedir, "\\jpgs\\", filetime, ".jpg");
                img.Save(filename, ImageFormat.Jpeg);

                var jstr = JsonConvert.SerializeObject(new Dictionary<string, string>()
                {
                    { "ImagePath" , filename },
                    { "Language" , srcLangCode }
                });
                var content = new StringContent(jstr, Encoding.UTF8, "application/json");

                var hc = new HttpClient();
                hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var resp = await hc.PostAsync("http://192.168.244.16:6666/ocr/api", content);
                var data = await resp.Content.ReadAsStringAsync();
                // var char = new ;
                return Task.FromResult(data).Result;
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                return string.Empty;
            }
        }

        public override bool OCR_Init(string param1 = "", string param2 = "")
        {
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
