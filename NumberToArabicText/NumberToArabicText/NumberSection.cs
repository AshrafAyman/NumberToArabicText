using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberToArabicText.NumberToArabicText
{
    public class NumberSection
    {
        private ArabicWordConfig arabicWordConfig;

        public NumberSection(ArabicWordConfig arabicWordConfig)
        {
            this.arabicWordConfig = arabicWordConfig;
        }

        public string[] Process(string num)
        {
            return ProcessSection(num).Reverse().ToArray();
        }

        private string[] ProcessSection(string section)
        {
            if (int.Parse(section) == 0)
            {
                return ProcessZero();
            }
            else
            {
                return ProcessGreaterThanZero(section);
            }
        }

        private string[] ProcessZero()
        {
            return new string[] { arabicWordConfig.numbers["0"] };
        }

        private string[] ProcessGreaterThanZero(string section)
        {
            List<string> partsAsWords = new List<string>();
            string[] parts = SplitIntoParts(section);

            for (int i = 0; i < parts.Length; i++)
            {
                string p = parts[i];
                string wordForPart = GetWordByNumberSectionIndex(p, i);

                if (!string.IsNullOrEmpty(wordForPart))
                {
                    partsAsWords.Add(wordForPart);
                }
            }

            return partsAsWords.ToArray();
        }

        private string[] SplitIntoParts(string word)
        {
            List<string> parts = new List<string>();

            for (int i = word.Length - 1; i >= 0; i -= 3)
            {
                string part = (i - 2 >= 0 ? word[i - 2].ToString() : "0") +
                              (i - 1 >= 0 ? word[i - 1].ToString() : "0") +
                              (i >= 0 ? word[i].ToString() : "0");
                parts.Add(part);
            }

            return parts.ToArray();
        }

        private string GetWordByNumberSectionIndex(string part, int numberSectionIndex)
        {
            int partAsNumber = int.Parse(part);
            string word = null;

            if (numberSectionIndex == 0)
            {
                word = GetWordForPart(part);
            }
            else if (partAsNumber == 1)
            {
                word = arabicWordConfig.numbers[$"1e{numberSectionIndex * 3}"];
            }
            else if (partAsNumber == 2)
            {
                word = arabicWordConfig.numbers[$"2e{numberSectionIndex * 3}"];
            }
            else
            {
                string partWord = GetWordForPart(part) + " ";

                if (partAsNumber >= 3 && partAsNumber <= 10)
                {
                    word = partWord + arabicWordConfig.numbers[$"3e{numberSectionIndex * 3}-1e{numberSectionIndex * 3 + 1}"];
                }
                else if (partAsNumber >= 11)
                {
                    word = partWord + arabicWordConfig.numbers[$"1e{numberSectionIndex * 3 + 1}+"];
                }
            }

            return word;
        }
        private string GetWordForPart(string part)
        {
            char n_0 = part[0];
            char n_1 = part[1];
            char n_2 = part[2];
            string n_0Word = GetWordForHundreds(n_0.ToString());
            string nGroup_0 = n_1.ToString() + n_2.ToString();
            int nGroupNum = int.Parse(nGroup_0);
            string n_1Word = GetWordForTens(nGroup_0);

            if (nGroupNum == 0)
            {
                return n_0Word;
            }

            if (n_0Word != null)
            {
                return $"{n_0Word} {arabicWordConfig.GetAll().numberSectionsDelimiter} {n_1Word}";
            }

            return n_1Word;
        }
        private string GetWordForHundreds(string number)
        {
            int charNum = int.Parse(number);
            string word = null;

            if (charNum == 1)
            {
                word = arabicWordConfig.numbers["100"];
            }
            else if (charNum == 2)
            {
                word = arabicWordConfig.numbers["200"];
            }
            else if (charNum >= 3 && charNum <= 9)
            {
                word = GetWordFromThreeHundredToNineHundred(number);
            }

            return word;
        }

        private string GetWordFromThreeHundredToNineHundred(string number)
        {
            return $"{arabicWordConfig.numbers[number]}{arabicWordConfig.numbers["100"]}";
        }
        private string GetWordForTens(string tensGroup)
        {
            int tensNum = int.Parse(tensGroup);

            if (tensNum == 0)
            {
                return arabicWordConfig.numbers["0"];
            }
            else if (tensNum >= 1 && tensNum <= 12)
            {
                if (tensNum == 10)
                {
                    return GetWordForTen(tensNum);
                }
                return arabicWordConfig.numbers[tensNum.ToString()];
            }
            else if (tensNum >= 13 && tensNum <= 19)
            {
                return GetWordFromThirteenToNineTeen(tensGroup);
            }
            else if (tensNum >= 20 && tensNum <= 99)
            {
                return GetWordFromTwentyToNinetyNine(tensGroup);
            }

            return null;
        }

        private string GetWordForTen(int num)
        {
            return $"{arabicWordConfig.numbers[num.ToString()]}{arabicWordConfig.feminizeSign}";
        }

        private string GetWordFromThirteenToNineTeen(string numGroup)
        {
            return $"{arabicWordConfig.numbers[numGroup[1].ToString()]} {arabicWordConfig.numbers["10"]}";
        }

        private string GetWordFromTwentyToNinetyNine(string tensGroup)
        {
            char tensChar = tensGroup[0];
            char singularChar = tensGroup[1];
            int tensNum = int.Parse(tensChar.ToString());
            int singularNum = int.Parse(singularChar.ToString());
            string tensWord = null;
            string singularWord = null;

            if (tensNum == 2)
            {
                tensWord = arabicWordConfig.numbers["20"];
            }
            else if (tensNum >= 3 && tensNum <= 9)
            {
                tensWord = $"{arabicWordConfig.numbers[tensChar.ToString()]}{arabicWordConfig.GetAll().tensPrefix}";
            }

            if (singularNum == 0)
            {
                return tensWord;
            }
            else if (tensNum >= 1 && tensNum <= 9)
            {
                singularWord = arabicWordConfig.numbers[singularChar.ToString()];
            }
            else if (tensNum == 10)
            {
                singularWord = arabicWordConfig.numbers[tensNum.ToString()];
            }
            else if (tensNum >= 11 && tensNum <= 12)
            {
                singularWord = arabicWordConfig.numbers[tensGroup];
            }
            else if (tensNum >= 13 && tensNum <= 19)
            {
                singularWord = GetWordFromThirteenToNineTeen(tensGroup);
            }

            return $"{singularWord}{(string.IsNullOrEmpty(singularWord) ? string.Empty : $" {arabicWordConfig.GetAll().numberSectionsDelimiter} ")}{tensWord}";
        }
    }
}
