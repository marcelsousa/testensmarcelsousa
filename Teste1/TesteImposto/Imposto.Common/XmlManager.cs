using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Imposto.Common
{
    public class XmlManager
    {
        /// <summary>
        /// Método para gerar um arquivo XML
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        public static void CriarXML(object obj, string path, string fileName)
        {
            try
            {
                bool exists = System.IO.Directory.Exists(path);

                if (!exists)
                    System.IO.Directory.CreateDirectory(path);

                XmlSerializer xs = new XmlSerializer(obj.GetType());

                StreamWriter file = new StreamWriter(path + fileName);
                xs.Serialize(file, obj);
                file.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
