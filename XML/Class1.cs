using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML // essas biblioteca tem o objetivo de facilitar a leitura do xml e a utilização de estrutura
{
    public class artigo //PERIÓDICO: estrutura utilizada para pegar as informações dos artigos-publicados de todos os xml
    {
        public int codigo; // id do artigo 
        public string titulo; // nome do artigo
        public int ano; // ano de publicação do artigo
        public int natureza; // 1-completo 2-estendido 3-resumo
        public string qualis; // qualidade do artigo
        public int quantcoautores; // quantidade de coautores
        public string autor; // ide do autor criador
        //public List<string> coautores; // para procurar um certo coator temos os nomes de referencia bibliografica dele
        //Adiciona, constroi toda a estrutudar do artigo periodico, mas não dá a qualis pois vai ser lida em um arq csv
        public void adiciona(int id, string titulo, int natureza, int ano, int contador, string autor)
        {
            this.natureza = natureza;
            this.codigo = id;
            this.titulo = titulo;
            this.ano = ano;
            this.qualis = "NE"; // não dá a qualis só inicializa
            this.quantcoautores = contador;
            this.autor = autor;
          //  this.coautores = coautor;
        }
        public void da_qualis(string qualis) // dá a qualis do artigo usada na leitura do csv
        {
            this.qualis = qualis;
        }
    }
    public class conferencias // Classe utiliza para representar os trabalhos aprensentados pelo autor ela será mais útil na hora de orgarnizarmos por tipo
    {
        public int codigo; // id do trabalho
        public string titulo; // nome do trabalho
        public int ano; // ano do trabalho
        public string qualis; // nota dada por a qualidade da conferencia
        public int quantcoautores; // conta quantos coautores tem
        public string autor; // a id do autor
        public int natureza; // 0-completo 1-estendido 2-resumo
        //public List<string> coautores; // referencias bibliografica dos coautores garantindo a pesquisa
        // adiciona, Constroi a estrutura na leitura do xml quando for ler as conferencias, apenas não passa a qualis pois ela sera lida no arquivo de qualis para a atribuir
        public void adiciona(int id, string titulo,int natureza, int ano, int contador, string autor)
        {
            this.codigo = id;
            this.titulo = titulo;
            this.ano = ano;
            this.qualis = "NE"; // não dá a qualis só inicializa
            this.quantcoautores = contador;
            this.autor = autor;
            this.natureza = natureza;
            //this.coautores = coautor;
        }
        public void da_qualis(string qualis) // dá a qualis do artigo usada na leitura do csv
        {
            this.qualis = qualis;
        }

    }
    public class autores // autores, classe utilizada para cada autor de artigos ou conferencias importantissimo para a pesquisa para o enunciado do trabalho
    {
        public string nome; // nome do autor
        public string refbibliografica; // referencia utilizada em documentos
        public int codigo;// a id do autor (Obs: não é id da curriculon lattes, mas sim uma id nova utilizando como chave de busca)
        public string local;
        public string pais;
        
        //adiciona, constroi completamente a estrutura dos autores importantissima para a pesquisa desejada
        public void adiciona(int id, string referencia, string autor,string local, string pais)
        {
            this.codigo = id;
            this.refbibliografica = referencia;
            this.nome = autor;
            this.local = local;
            this.pais = pais;
        }
    }
   public class estruturas // ideia de falitar o codigo assim já temos todas a estruturas  construtidas
    {
        public  autores[] autor = new autores[50]; // array para pegar cada autor já que podem ser pegos apartir de 50 o número de autores se sabe, por isso se tem o arry
        public  List<conferencias> coferencia = new List<conferencias>(); // lista de conferencias é utilizada pois não se sabe o numero total de conferencias
        public  List<artigo> artigo = new List<artigo>(); // mesmo caso para os artigos Periódicos
        public conferencias conf = new conferencias(); // construção da estrutura conferencia para adicionar na lista
        public artigo artig = new artigo(); //mesmo objetivo da conferencia
       // construção de cada elemento do array assim não há erro de referencia pois o espaço para o autor foi alocado
       public void declara_autor(int i)
       {
           this.autor[i] = new autores();
       }
    }
}
