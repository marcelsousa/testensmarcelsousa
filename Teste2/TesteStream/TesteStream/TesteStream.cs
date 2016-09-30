using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteStream
{

    public interface IStream
    {
        char getNext();
        bool hasNext();
    }

    public class TesteStream : IStream
    {
        private string str;

        private List<char> charRead;

        private int lastIndex = -1;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="str"></param>
        public TesteStream(string str)
        {
            this.str = str;
            charRead = new List<char>();
        }


        /// <summary>
        /// Pega o próximo char
        /// </summary>
        /// <returns></returns>
        public char getNext()
        {
            char next = str.ElementAt(++lastIndex);
            charRead.Add(next);
            return next;
        }

        /// <summary>
        /// Verifica se tem próximo char
        /// </summary>
        /// <returns></returns>
        public bool hasNext()
        {
            try
            {
                str.ElementAt(lastIndex + 1);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
