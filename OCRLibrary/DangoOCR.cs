using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
                var string filename = await Environment.CurrentDirectory + "\\jpgs\\" + DateTime.ToFileTime().ToString() + ".jpg";
                img.Save(filename, ImageFormat.Jpg);

                var res = await dangoOcr.RecognizeAsync(bitmap);
                return Task.FromResult(res.Text).Result;
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                return string.Empty;
            }
        }

        public override bool OCR_Init(string param1 = "", string param2 = "")
        {
            try
            {
                Language lang = new(srcLangCode);
                dangoOcr = OcrEngine.TryCreateFromLanguage(lang);
                if (dangoOcr == null)
                {
                    System.Windows.MessageBox.Show($"请在Windows设置App中添加OCR组件。{System.Environment.NewLine}Please install OCR component in Windows Settings App.");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                return false;
            }
        }

        public override void SetOCRSourceLang(string lang)
        {
            if (lang == "jpn")
            {
                srcLangCode = "ja-jp";
            }
            else if (lang == "eng")
            {
                srcLangCode = "en-us";
            }
        }

        public IReadOnlyList<Language> GetSupportLang()
        {
            return OcrEngine.AvailableRecognizerLanguages;
        }
    }
}
