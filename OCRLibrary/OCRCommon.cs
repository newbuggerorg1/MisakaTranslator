using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCRLibrary
{
    public class OCRCommon
    {
        public static List<string> lstOCR = new List<string>()
        {
            "BaiduOCR",
            "BaiduFanyiOCR",
            "Tesseract52OCR",
            "Tesseract5",
            "WinRtOCR",
            "DangoOCR"
        };

        public static List<string> GetOCRList()
        {
            return lstOCR;
        }

        public static OCREngine OCRAuto(string ocr) {
            switch (ocr)
            {
                case "BaiduOCR":
                    return new BaiduGeneralOCR(); ;
                case "BaiduFanyiOCR":
                    return new BaiduFanyiOCR();
                case "Tesseract52OCR":
                    return new Tesseract52OCR(); ;
                case "Tesseract5":
                    return new Tesseract5OCR();
                case "WinRtOCR":
                    return new WinRtOCR();
                case "DangoOCR":
                    return new DangoOCR();
                default:
                    return null;
            }
        }
    }
}
