using System;
using System.Collections.Generic;
using System.Text;
using WordFileTest.NumberToArabicText.Classes;

namespace NumberToArabicText.NumberToArabicText
{
    public class ArabicWord
    {
        private ArabicWordConfig config = new ArabicWordConfig();
        private NumberSection numberSection;

        private string delimiter = "و";

        public ArabicWord(Config config = null)
        {
            if (config != null)
            {
                SetConfig(config);
            }

            numberSection = new NumberSection(this.config);
        }

        public ArabicWord SetConfig(Config config)
        {
            this.config.overrideConfig(config);
            return this;
        }

        public object Process(decimal num)
        {
            string[] sections = SplitIntoSections(num);

            if (config.GetAll().strict)
            {
                ProcessResult result = new ProcessResult();

                if (!string.IsNullOrEmpty(sections[0]))
                {
                    result.Base = string.Join($" {delimiter} ", numberSection.Process(sections[0]));
                }

                if (sections.Length > 1 && !string.IsNullOrEmpty(sections[1]))
                {
                    result.delimiter = config.GetAll().delimiter;
                    result.Reminder = string.Join($" {delimiter} ", numberSection.Process(sections[1]));
                }

                return result;
            }
            else
            {
                List<string> resultInStringSections = new List<string>();

                if (!string.IsNullOrEmpty(sections[0]))
                {
                    var leftSide = numberSection.Process(sections[0]);
                    resultInStringSections.Add(string.Join($" {delimiter} ", leftSide));
                }

                if (sections.Length > 1 && !string.IsNullOrEmpty(sections[1]))
                {
                    var rightSide = numberSection.Process(sections[1]);
                    resultInStringSections.Add(string.Join($" {delimiter} ", rightSide));
                }

                return string.Join($" {config.GetAll().delimiter} ", resultInStringSections);
            }
        }

        public ArabicWord Create()
        {
            return new ArabicWord();
        }

        private string[] SplitIntoSections(decimal num)
        {
            return num.ToString().Split('.');
        }
    }
}
