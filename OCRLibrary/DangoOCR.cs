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
                var filename = filedir + "\\jpgs\\" + filetime + ".jpg";
                img.Save(filename, ImageFormat.Jpeg);

                var jstr = JsonConvert.SerializeObject(new Dictionary<string, string>()
                {
                    { "ImagePath" , filename },
                    { "Language" , srcLangCode }
                });
                var req = new StringContent(jstr, Encoding.UTF8, "application/json");

                var hc = new HttpClient();
                hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var resp = await hc.PostAsync("http://localhost:6666/ocr/api", req);
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
                return Task.FromResult(chara);
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                return Task.FromResult(string.Empty);
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

    // https://github.com/PantsuDango/DangoOCR/blob/master/app.py#L31
    public class JsonSuccess
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string RequestId { get; set; }
        public List<Datam> Data { get; set; }
    }
    public class Datam
    {
        public Coordinatem Coordinate { get; set; }
        public string Words { get; set; }
        public float Score { get; set; }
    }
    public class Coordinatem
    {
        public string UpperLeft { get; set; }
        public string UpperRight { get; set; }
        public string LowerRight { get; set; }
        public string LowerLeft { get; set; }
    }
}
