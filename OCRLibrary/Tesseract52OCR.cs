using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;
using TesseractOCR;

namespace OCRLibrary
{
    public class Tesseract52OCR : OCREngine
    {
        public Language srcLangCode;  //OCR识别语言 jpn=日语 eng=英语
        private TesseractOCR.Engine TesseractOCREngine;

        public override async Task<string> OCRProcessAsync(Bitmap img)
        {
            try
            {
                var chara = TesseractOCREngine.Process(img);
                return Task.FromResult(chara.Text).Result;
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
                TesseractOCREngine = new TesseractOCR.Engine(Environment.CurrentDirectory + "\\tessdata", srcLangCode, EngineMode.Default);
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
                srcLangCode = Language.English;
            }
            else if (lang == "eng")
            {
                srcLangCode = Language.Japanese;
            }
        }
    }
}
