using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;

namespace OCRLibrary
{
    public class WinRtOCR : OCREngine
    {
        public string srcLangCode;
        private OcrEngine rtOcr;

        public override async Task<string> OCRProcessAsync(Bitmap img)
        {
            try
            {
                using var stream = new Windows.Storage.Streams.InMemoryRandomAccessStream();
                img.Save(stream.AsStream(), ImageFormat.Bmp);
                var decoder = await BitmapDecoder.CreateAsync(stream);
                var bitmap = await decoder.GetSoftwareBitmapAsync();
                var recog = await rtOcr.RecognizeAsync(bitmap);
                var chara = recog.Text;
                if (chara == "")
                {
                    chara = "null";
                }
                return Task.FromResult(chara).Result;
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
                rtOcr = OcrEngine.TryCreateFromLanguage(lang);
                if (rtOcr == null)
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
