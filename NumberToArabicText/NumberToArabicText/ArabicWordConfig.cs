using System;
using System.Collections.Generic;
using System.Text;
using WordFileTest.NumberToArabicText.Classes;

namespace NumberToArabicText.NumberToArabicText
{
    public class ArabicWordConfig
    {
        private Config config = new Config
        {
            delimiter = "و",
            numberSectionsDelimiter = "و",
            tensPrefix = "ون",
            strict = false,
        };

        public string feminizeSign = "ة";

        public Dictionary<string, string> numbers = new Dictionary<string, string>
        {
            { "0", "صفر" },
            { "1", "واحد" },
            { "2", "أثنان" },
            { "3", "ثلاث" },
            { "4", "أربع" },
            { "5", "خمس" },
            { "6", "ست" },
            { "7", "سبع" },
            { "8", "ثمان" },
            { "9", "تسع" },
            { "10", "عشر" },
            { "11", "إحدى عشر" },
            { "12", "إثنا عشر" },
            { "20", "عشرون" },
            { "100", "مائة" },
            { "200", "مائتان" },
            { "1e3", "ألف" },
            { "2e3", "ألفين" },
            { "3e3-1e4", "آلاف" },
            { "1e4+", "ألف" },
            { "1e6", "مليون" },
            { "2e6", "مليونان" },
            { "3e6-1e7", "ملاين" },
            { "1e7+", "مليون" },
            { "1e9", "مليار" },
            { "2e9", "ملياران" },
            { "3e9-1e10", "مليارات" },
            { "1e10+", "مليار" },
            { "1e12", "بليون" },
            { "2e12", "بليونان" },
            { "3e12-1e13", "بلايين" },
            { "1e13+", "بليون" },
            { "1e15", "تريليون" },
            { "2e15", "تريليونان" },
            { "2e15-1e16", "تريليونات" },
            { "1e16+", "تريليون" },
            { "1e18", "كوادرليون" },
            { "2e18", "كوادرليونان" },
            { "2e18-1e19", "كوادرليونات" },
            { "1e19+", "كوادرليون" }
        };

        public void overrideConfig(Config config)
        {
            config.tensPrefix = null;
            if (config.delimiter != null)
            {
                config.delimiter = config.delimiter.Replace(" ", "");
            }
            if (config.numberSectionsDelimiter != null)
            {
                config.numberSectionsDelimiter = config.numberSectionsDelimiter.Replace(" ", "");
            }
            this.config = MergeConfigs(this.config, config);
        }

        public Config GetAll()
        {
            return config;
        }

        private Config MergeConfigs(Config currentConfig, Config newConfig)
        {
            Config mergedConfig = new Config();

            // Copy the properties from the currentConfig
            mergedConfig.delimiter = currentConfig.delimiter;
            mergedConfig.numberSectionsDelimiter = currentConfig.numberSectionsDelimiter;

            // Copy the properties from the newConfig
            if (newConfig.delimiter != null)
            {
                mergedConfig.delimiter = newConfig.delimiter;
            }
            if (newConfig.numberSectionsDelimiter != null)
            {
                mergedConfig.numberSectionsDelimiter = newConfig.numberSectionsDelimiter;
            }

            return mergedConfig;
        }
    }
}
