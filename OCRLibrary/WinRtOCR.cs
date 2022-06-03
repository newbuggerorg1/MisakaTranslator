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
                img.Save(stream.AsStream(), ImageFormat.Png);
                var decoder = await BitmapDecoder.CreateAsync(stream);
                var bitmap = await decoder.GetSoftwareBitmapAsync();
                var res = await rtOcr.RecognizeAsync(bitmap);
                return Task.FromResult(res.Text).Result;
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                return Task.FromResult<string>(null).Result;
            }

        }

        public override bool OCR_Init(string param1 = "", string param2 = "")
        {
            try
            {
                Language lang = new(srcLangCode);
                rtOcr = OcrEngine.TryCreateFromLanguage(lang);
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
            srcLangCode = lang;
        }

        public IReadOnlyList<Language> GetSupportLang()
        {
            return OcrEngine.AvailableRecognizerLanguages;
        }
    }
}
