using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML
{
    public class artigo
    {
        public int codigo;
        public string titulo;
        public int ano;
        public string natureza;
        public string qualis;
        public int quantcoautores;
        public int autor;
        public List<int> coautores;
    }
    public class conferencias
    {
        public int codigo;
        public string titulo;
        public int ano;
        public string qualis;
        public string natureza;
        public int quantcoautores;
        public int autor;
        public List<int> coautores;
    }
    public class autores
    {
        public string nome;
        public string refbibliografica;
        public int codigo;
    }
}
