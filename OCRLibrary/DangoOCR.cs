using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

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
                var string filename = Environment.CurrentDirectory + "\\jpgs\\" + DateTime.ToFileTime().ToString() + ".jpg";
                img.Save(filename, ImageFormat.Jpg);

                var dic = new Dictionary<string, string>() {
                    { "ImagePath" , filename },
                    { "Language" , srcLangCode }
                };
                var data = await JsonSerializer.SerializeAsync(dic);

                using var hc = new HttpClient();
                var resp = await hc.PostAsync("http://localhost:10090/dango_ocr", data);
                return Task.FromResult(resp.Content.ReadAsStringAsync()).Result;
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

        public IReadOnlyList<Language> GetSupportLang()
        {
            return OcrEngine.AvailableRecognizerLanguages;
        }
    }
}
