using NumberToArabicText.NumberToArabicText;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WordFileTest.NumberToArabicText.Classes;

namespace WordFileTest.NumberToArabicText
{
    public static class ArabicWordConverter
    {
        private static ArabicWord arabicWord = new ArabicWord();

        public static object ToArabicWord(decimal number)
        {
            return arabicWord.Process(number);
        }
    }
}

