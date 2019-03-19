using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Slutprojekt
{
    static class LoadData
    {
        public static XmlDocument XmlLoad(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return doc;
        }

        public static void Load<T>(T type, string[] paths)
        {
            for (int i = 0; i < paths.Length; i++)
            {

            }
        }
    }
}
