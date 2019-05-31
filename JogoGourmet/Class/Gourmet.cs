using JogoGourmet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoGourmet.Class
{
    public class Gourmet : IGourmet
    {
        private const string TITULO_MENSAGEM = "Jogo Gourmet";

        public DialogResult RespostaUsuario { get; private set; }

        private static Gourmet instancia = null;

        public static Gourmet GetInstancia
        {
            get
            {
                if (instancia == null)
                    return new Gourmet();

                return instancia;
            }
        }

        public Gourmet()
        {
            IniciarPergunta();
        }

        private void IniciarPergunta()
        {
            RespostaUsuario = MessageBox.Show("Pense em um prato que gosta", TITULO_MENSAGEM, MessageBoxButtons.OK);

            TipoPrato();
        }

        private void TipoPrato()
        {
            RespostaUsuario = MessageBox.Show("O prato que você pensou é massa?", TITULO_MENSAGEM, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (RespostaUsuario == DialogResult.Yes)
                EscolhePratoLasanha();
        }

        private void EscolhePratoLasanha()
        {
            RespostaUsuario = MessageBox.Show("O prato que você pensou é Lasanha?")
        }
    }
}
