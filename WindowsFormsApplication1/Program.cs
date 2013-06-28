using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Xml;
using XML;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static estruturas estru = new estruturas(); // declaração da estrutura utilizadas na referencia XML, como variavel universal para qualquer operação
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }

    public static class remove
    {
        public static string RemoverAcentuacao(this string texto)
        {
            string semAcento = string.Empty;
            var letras = texto.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letra in letras)
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(letra) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    semAcento += letra;

            return semAcento;
        }
    }
    }
