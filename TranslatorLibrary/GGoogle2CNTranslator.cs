using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GTranslate.Translators;

namespace TranslatorLibrary
{
    public class GGoogle2CNTranslator : ITranslator
    {
        private GoogleTranslator2 translator;
        private string errorInfo;//错误信息

        public string GetTkkJS;

        public string GetLastError()
        {
            return errorInfo;
        }

        public async Task<string> TranslateAsync(string sourceText, string desLang, string srcLang)
        {
            if (desLang == "zh")
                desLang = "zh";
            if (srcLang == "zh")
                srcLang = "zh";

            if (desLang == "jp")
                desLang = "ja";
            if (srcLang == "jp")
                srcLang = "ja";

            if (desLang == "kr")
                desLang = "ko";
            if (srcLang == "kr")
                srcLang = "ko";

            try
            {
                var result = await translator.TranslateAsync(sourceText, desLang);
                var chara = result.Translation;
                return Task.FromResult(chara).Result;
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                return Task.FromResult(string.Empty).Result;
            }
        }

        public void TranslatorInit(string param1 = "", string param2 = "")
        {
            translator = new GoogleTranslator2(CommonFunction.GetHttpProxiedClient());
        }
    }
}
