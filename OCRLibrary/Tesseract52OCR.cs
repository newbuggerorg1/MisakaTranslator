using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesseractOCR;
using TesseractOCR.Enums;

namespace OCRLibrary
{
    public class Tesseract52OCR : OCREngine
    {
        private Language srcLangCode;  //OCR识别语言 jpn=日语 eng=英语
        private Engine TesseractOCREngine;

        public override async Task<string> OCRProcessAsync(Bitmap img)
        {
            try
            {
                var stream = new MemoryStream();
                img.Save(stream, ImageFormat.Bmp);
                var recog = TesseractOCREngine.Process(TesseractOCR.Pix.Image.LoadFromMemory(stream.GetBuffer(), 0, stream.Length));
                stream.Dispose();

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
                TesseractOCREngine = new Engine(Environment.CurrentDirectory + "\\tessdata", srcLangCode, EngineMode.Default);
                return true;
            }
            catch(Exception ex)
            {
                errorInfo = ex.Message;
                return false;
            }
        }

        public override void SetOCRSourceLang(string lang)
        {
            if (lang == "jpn")
            {
                srcLangCode = Language.Japanese;
            }
            else if (lang == "eng")
            {
                srcLangCode = Language.English;
            }
        }
    }
}
