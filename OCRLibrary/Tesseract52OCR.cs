using System;
using System.Collections.Generic;
using System.Drawing;
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

        public override Task<string> OCRProcessAsync(Bitmap img)
        {
            try
            {
                var filedir = Environment.CurrentDirectory;
                var filetime = DateTime.Now.ToFileTime().ToString();
                var filename = filedir + "\\bmps\\" + filetime + ".bmp";
                img.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
                
                var fileimg = TesseractOCR.Pix.Image.LoadFromFile(filename);
                var chara = TesseractOCREngine.Process(fileimg);
                return Task.FromResult(chara.Text);
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                return Task.FromResult<string>(null);
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
                srcLangCode = Language.English;
            }
            else if (lang == "eng")
            {
                srcLangCode = Language.Japanese;
            }
        }
    }
}
