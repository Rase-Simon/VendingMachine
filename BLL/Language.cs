using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    class Language
    {
        private int languageId;
        private string languageName;

        public string getName()
        {
            return languageName;
        }

        public Language(int id, string name)
        {
            languageId = id;
            languageName = name;
        }
    }
}
