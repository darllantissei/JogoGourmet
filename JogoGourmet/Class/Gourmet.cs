using JogoGourmet.Interfaces;
using Microsoft.VisualBasic;
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

        public DialogResult RespostaUsuario { get; set; }

        private IPratosGourmet PratosGourmetNaoMassa { get; set; }
        private IPratosGourmet PratosGourmetMassa { get; set; }

        private IList<IPratosGourmet> ListaPratosMassa { get; set; }
        private IList<IPratosGourmet> ListaPratosNaoMassa { get; set; }

        private static readonly Gourmet instancia = null;

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
            // Pratos iniciais
            PratosGourmetMassa = new PratosGourmet { NomePrato = "Lasanha", TipoPrato = string.Empty };
            PratosGourmetNaoMassa = new PratosGourmet { NomePrato = "Bolo de Chocolate", TipoPrato = string.Empty };

            ListaPratosMassa = new List<IPratosGourmet>
            {
                PratosGourmetMassa
            };
            ListaPratosNaoMassa = new List<IPratosGourmet>
            {
                PratosGourmetNaoMassa
            };            
        }

        /// <summary>
        /// Iniciar o jogo de adivinhação
        /// </summary>
        public void IniciarPergunta()
        {
            RespostaUsuario = MessageBox.Show("Pense em um prato que gosta", TITULO_MENSAGEM, MessageBoxButtons.OKCancel);

            if (RespostaUsuario == DialogResult.Cancel)
                return;

            RespostaUsuario = MessageBox.Show("O prato que você pensou é massa?", TITULO_MENSAGEM, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (RespostaUsuario == DialogResult.Yes)
                AdivinharTipoPrato(ListaPratosMassa);
            else
                AdivinharTipoPrato(ListaPratosNaoMassa);
        }

        private void AdivinharTipoPrato(IList<IPratosGourmet> pratos)
        {
            int count;
            int tamanhoLista = pratos.Count - 1;

            // Verificar as caracteristicas do prato pensado
            for (count = tamanhoLista; count > 0; count--)
            {
                RespostaUsuario = VerificarPrato(pratos, count, true);

                // De acordo com prato escolhido verificar a caracteristica
                if (RespostaUsuario == DialogResult.Yes)
                {
                    RespostaUsuario = VerificarPrato(pratos, count, false);

                    // Caso seja o prato que o usuário escolheu mostrar a mensagem que a aplicação adivinhou
                    if (RespostaUsuario == DialogResult.Yes)
                    {
                        EscolhaCerta();
                        break;
                    }
                    else if (RespostaUsuario == DialogResult.No && count == 0)
                    {
                        // Caso não tenha acertado, desistir e pedir para o usuário inserir o prato
                        InserirPrato(pratos, count);
                        break;
                    }
                }
            }

            // Caso tenha percorrido todas as opções e não encontrou, matenho as opções pré-cadastradas(Lasanha e Bolo de Chocolate)
            // Se o usuário confirmar os pré-cadastro mostrar a mensagem que a aplicação acertou
            if (count == 0)
            {
                RespostaUsuario = VerificarPrato(pratos, count, false);

                if (RespostaUsuario == DialogResult.Yes)
                {
                    EscolhaCerta();
                    return;
                }

                InserirPrato(pratos, count);

                // Informar que o usuário ainda está tentando ver se a aplicação adivinha seu prato
                RespostaUsuario = DialogResult.OK;
            }
        }

        private void InserirPrato(IList<IPratosGourmet> pratos, int ordemPrato)
        {
            pratos.Add(ConstruirPratoNovo(pratos, ordemPrato));
        }

        /// <summary>
        /// caso a aplicação não consiga adivinhar o prato do usuário pedir pra que ele insira
        /// </summary>
        /// <param name="pratos">Lista dos pratos</param>
        /// <param name="ordemPrato">Ordem dos pratos</param>
        /// <returns>Retorna o prato inserido pelo usuário</returns>
        private IPratosGourmet ConstruirPratoNovo(IList<IPratosGourmet> pratos, int ordemPrato)
        {
            string nomePrato = Interaction.InputBox("Qual prato você pensou?", $"{TITULO_MENSAGEM} - Desisto", string.Empty);
            string tipoPrato = Interaction.InputBox($"{nomePrato} é __________ mas {pratos[ordemPrato].NomePrato} não.", $"{TITULO_MENSAGEM} - Complete", string.Empty);

            IPratosGourmet pratoGourmet = new PratosGourmet
            {
                NomePrato = nomePrato,
                TipoPrato = tipoPrato
            };

            return pratoGourmet;
        }

        /// <summary>
        /// Verificar o prato que o usuário escolheu
        /// </summary>
        /// <param name="pratos">Lista com os pratos</param>
        /// <param name="posicao">posicao do prato que o usuário está solicitando a advinhação</param>
        /// <param name="definicoes">Verficiar se que exibir as definições do prato ou o nome do prato</param>
        /// <returns>Retorna sim ou não se a aplicação acertou o prato</returns>
        private DialogResult VerificarPrato(IList<IPratosGourmet> pratos, int posicao, bool definicoes)
        {
            if (definicoes)
                return MessageBox.Show($"O prato que pensou é {pratos[posicao].TipoPrato}?", $"{TITULO_MENSAGEM} - Confirma", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            return MessageBox.Show($"O prato que pensou é {pratos[posicao].NomePrato}?", $"{TITULO_MENSAGEM} - Confirma", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }        

        private void EscolhaCerta()
        {
            RespostaUsuario = MessageBox.Show("Acertei de novo!", TITULO_MENSAGEM, MessageBoxButtons.OK, MessageBoxIcon.Information);            
        }
    }
}
