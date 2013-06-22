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
        string nomeArq = @"C:\Users\ML\Documents\GitHub\CPD-Trab\periodicos.bin";

        FileStream stream = new FileStream(nomeArq, FileMode.Open);
        BinaryReader binario = new BinaryReader(stream);


        //não sei como funciona esse teu bizu aqui Program.estru.autor, mas esse trecho lê tudo e é pra colocar num array
        //fora isso parece estar funcionando
        int i = 0;
        //Até o fim do arquivo
        while (binario.PeekChar() != -1)
        {
            //le um inteiro, uma referencia, e um nome e adiciona tudo num elemento do arranjo de autores
            Program.estru.autor[i].adiciona(binario.ReadInt32(), binario.ReadString(), binario.ReadString());
        }
        binario.Close();
        stream.Close();


    }

}

