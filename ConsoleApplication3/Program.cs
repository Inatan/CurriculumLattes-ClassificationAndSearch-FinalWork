
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {

            //quando selecionar, guarda caminho de cada file em um vetor de strings
            //para cada caminho, abrir o arquivo XML
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load("curriculo.xml");
            Console.WriteLine(xmldoc.DocumentElement.Name);
            for (int i = 0; i < xmldoc.DocumentElement.ChildNodes.Count; i++)
            {
                //Escreve na tela o conteudo do nó filho
                if (xmldoc.DocumentElement.ChildNodes[i].InnerText != "")
                {
                    Console.WriteLine(xmldoc.DocumentElement.ChildNodes[i].Name/* + "<br>"*/);
                    XmlNode decida1 = xmldoc.DocumentElement.ChildNodes[i];
                    for (int j = 0; j < decida1.ChildNodes.Count; j++)
                    {
                        if (decida1.ChildNodes[j].InnerText != "")
                        {
                            Console.WriteLine(decida1.ChildNodes[j].FirstChild.FirstChild.FirstChild/* + "<br>"*/);
                        }
                    }
                }
            }

            /*
            XmlElement root = (XmlElement)xmldoc.DocumentElement.FirstChild;
            XmlNode next = xmldoc.SelectSingleNode("CURRICULO-VITAE/DADOS-GERAIS");
            XmlElement nome = (XmlElement)next;

            Console.WriteLine(nome.GetAttribute("NOME-COMPLETO"));*/
         /*   foreach (XmlNode filho in next)
            {
                XmlElement elemento = (XmlElement)filho.NextSibling;
                if (elemento.HasAttribute("TEXTO-RESUMO-CV-RH"))
                {
                    string curso = elemento.GetAttribute("TEXTO-RESUMO-CV-RH");
                    Console.WriteLine(curso);
                }
            }

            //Aqui eu faço um cast para transformar o .FirstChild que devolve um nodo para um elemento,
            //assim podendo ler os atributos
            if (root.HasAttribute("NOME-COMPLETO"))
            {
                String nome = root.GetAttribute("NOME-COMPLETO"); //WORKS BITCH
                Console.WriteLine(nome);
            }*/
            


            Console.ReadKey();
        }
    }
}