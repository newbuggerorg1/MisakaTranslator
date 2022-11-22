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
        private Language srcLangCode;
        private OcrEngine rtOcrEngine;

        public override async Task<string> OCRProcessAsync(Bitmap img)
        {
            try
            {
                var stream = new Windows.Storage.Streams.InMemoryRandomAccessStream();
                img.Save(stream.AsStream(), ImageFormat.Bmp);
                var decoder = await BitmapDecoder.CreateAsync(stream);
                var bitmap = await decoder.GetSoftwareBitmapAsync();
                var recog = await rtOcrEngine.RecognizeAsync(bitmap);
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
                return Task.FromResult(string.Empty).Result;
            }
        }

        public override bool OCR_Init(string param1 = "", string param2 = "")
        {
            try
            {
                rtOcrEngine = OcrEngine.TryCreateFromLanguage(srcLangCode);
                if (rtOcrEngine == null)
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

        // error CS8370: Feature 'target-typed object creation' is not available in C# 7.3.
        // Please use language version 9.0 or greater.
        public override void SetOCRSourceLang(string lang)
        {
            if (lang == "jpn")
            {
                srcLangCode = new Language("ja-jp");
            }
            else if (lang == "eng")
            {
                srcLangCode = new Language("en-us");
            }
        }
    }
}
