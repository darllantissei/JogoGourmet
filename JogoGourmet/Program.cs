using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoGourmet
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Class.Gourmet gourmet = Class.Gourmet.GetInstancia;
            do
            {
                gourmet.IniciarPergunta();
            } while (gourmet.RespostaUsuario == DialogResult.OK);
        }
    }
}
