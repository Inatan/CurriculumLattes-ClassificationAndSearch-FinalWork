using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1;
using XML;


public class leArquivos
{
    public static void leAutores()
    {
        string nomeArq = "autores.bin";

        FileStream stream = new FileStream(nomeArq, FileMode.Open);
        BinaryReader binario = new BinaryReader(stream);

        if (!stream.CanRead)
            return;
        

        int i = 0;
        //Até o fim do arquivo
        //stream.CanRead
        //binario.PeekChar() != -1
        while (binario.PeekChar() != -1)
        {
            //le um inteiro, uma referencia, e um nome e adiciona tudo num elemento do arranjo de autores
            Program.estru.declara_autor(i);
            Program.estru.autor[i].adiciona(binario.ReadInt32(), binario.ReadString(), binario.ReadString(), binario.ReadString(), binario.ReadString());
            i++;
        }
        binario.Close();
        stream.Close();
    }

    public static void lePeriodicos()
    {
        string nomeArq = "periodicos.bin";

        FileStream stream = new FileStream(nomeArq, FileMode.Open);
        BinaryReader binario = new BinaryReader(stream);
        Program.estru.artigo = new List<artigo>();
        if (!stream.CanRead)
            return;

        int i = 0;
        //Até o fim do arquivo
        while (binario.BaseStream.Position != binario.BaseStream.Length)
        {
            //le um inteiro, uma referencia, e um nome e adiciona tudo num elemento do arranjo de autores
            Program.estru.artig = new artigo();
            Program.estru.artig.adiciona(binario.ReadInt32(), binario.ReadString(), binario.ReadInt32(), binario.ReadInt32(), binario.ReadInt32(), binario.ReadString());
            Program.estru.artig.qualis = binario.ReadString();
            Program.estru.artigo.Add(Program.estru.artig);
            i++;
        }
        binario.Close();
        stream.Close();
    }

    public static void leConferencias()
    {
        string nomeArq = "conferencias.bin";

        FileStream stream = new FileStream(nomeArq, FileMode.Open);
        BinaryReader binario = new BinaryReader(stream);
        Program.estru.coferencia = new List<conferencias>();

        if (!stream.CanRead)
            return;

        int i = 0;
        //Até o fim do arquivo
        while (binario.BaseStream.Position != binario.BaseStream.Length)
        {
            //le um inteiro, uma referencia, e um nome e adiciona tudo num elemento do arranjo de autores
            Program.estru.conf = new conferencias();
            Program.estru.conf.adiciona(binario.ReadInt32(), binario.ReadString(), binario.ReadInt32(), binario.ReadInt32(), binario.ReadInt32(), binario.ReadString());
            Program.estru.conf.qualis = binario.ReadString();
            Program.estru.coferencia.Add(Program.estru.conf);
            i++;
        }
        binario.Close();
        stream.Close();
    }

    void lerprodaut()
    {
        string nomeArq = "prodAut.bin.bin";

        FileStream stream = new FileStream(nomeArq, FileMode.Open);
        BinaryReader binario = new BinaryReader(stream);
        Program.estru.coferencia = new List<conferencias>();

        if (!stream.CanRead)
            return;

        int i = 0;
        //Até o fim do arquivo
        while (binario.BaseStream.Position != binario.BaseStream.Length)
        {
            //le um inteiro, uma referencia, e um nome e adiciona tudo num elemento do arranjo de autores
            Program.estru.conf = new conferencias();
            Program.estru.conf.adiciona(binario.ReadInt32(), binario.ReadString(), binario.ReadInt32(), binario.ReadInt32(), binario.ReadInt32(), binario.ReadString());
            Program.estru.conf.qualis = binario.ReadString();
            Program.estru.coferencia.Add(Program.estru.conf);
            i++;
        }
        binario.Close();
        stream.Close();
    }

}
