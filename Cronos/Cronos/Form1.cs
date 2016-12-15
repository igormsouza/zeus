using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cronos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var link = new LinkLabel.Link();
            link.LinkData = "http://intranet.bhs.com.br/depto/pmo/conhecimento/blog/default.aspx";
            linkAjuda.Links.Add(link);
        }

        private void btnBuscarCaminhoOrigem_Click(object sender, EventArgs e)
        {
            try
            {
                var fbd = new FolderBrowserDialog();
                fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;

                var result = fbd.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtCaminhoOrigem.Text = fbd.SelectedPath;
                    var namespaceAtual = Util.DescobreNamespace(txtCaminhoOrigem.Text);
                    txtNamespaceOrigem.Text = string.Format("{0}.{1}", namespaceAtual.Item1, namespaceAtual.Item2);
                }

                AtualizaStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarCaminhoDestino_Click(object sender, EventArgs e)
        {
            try
            {
                var fbd = new FolderBrowserDialog();
                fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;

                var result = fbd.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtCaminhoDestino.Text = fbd.SelectedPath;
                }

                AtualizaStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AtualizaStatus()
        {
            bool gerar = false;

            if (string.IsNullOrWhiteSpace(txtCaminhoOrigem.Text))
            {
                lblStatusOperacao.Text = "Aguardando a seleção da pasta origem";
            }
            else if (string.IsNullOrWhiteSpace(txtCaminhoDestino.Text))
            {
                lblStatusOperacao.Text = "Aguardando a seleção da pasta destino";
            }
            else if (string.IsNullOrWhiteSpace(txtCliente.Text))
            {
                lblStatusOperacao.Text = "Aguardando o preenchimento do Cliente";
            }
            else if (string.IsNullOrWhiteSpace(txtProduto.Text))
            {
                lblStatusOperacao.Text = "Aguardando o preenchimento do Produto";
            }
            else
            {
                lblStatusOperacao.Text = "Pronto para geração";
                gerar = true;
            }

            return gerar;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (AtualizaStatus())
            {
                Iniciar();
            }
            else
            {
                MessageBox.Show("Existe(m) informação(ões) incompleta(s) para geração.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Iniciar()
        {
            try
            {
                NovaLinha("Início Operação");
                NovaLinha(string.Format("Caminho Origem = {0}", txtCaminhoOrigem.Text));
                NovaLinha(string.Format("Caminho Destino = {0}", txtCaminhoDestino.Text));

                NovaLinha(string.Format(" Preparação dos dados {0}", Environment.NewLine));

                NovaLinha("1 - Alterar e verificar permissão de leitura e escrita na pasta origem.");
                Util.AlteraPermissaoDeEscritaPasta(txtCaminhoOrigem.Text, true);

                NovaLinha("2 - Alterar e verificar permissão de leitura e escrita na pasta destino.");
                Util.AlteraPermissaoDeEscritaPasta(txtCaminhoOrigem.Text, true);

                NovaLinha("3 - Limpa pasta destino.");
                Util.LimpaDestino(txtCaminhoDestino.Text);

                NovaLinha("4 - Copia pasta destino para origem.");
                Util.CopiaArquivosPastaOrigemParaDestino(txtCaminhoOrigem.Text, txtCaminhoDestino.Text);

                NovaLinha("5 - Modificando nome de pastas e arquivos na pasta");
                Util.pastasZeus = Util.pastasZeus.Select(o => o.Replace("{namespaceAtual}", txtNamespaceOrigem.Text)).ToArray();
                int cont = 1;
                foreach (var item in Util.pastasZeus)
                {
                    Util.RenomeiaDestino(txtCaminhoDestino.Text, txtNamespaceOrigem.Text, string.Format("{0}.{1}", txtCliente.Text, txtProduto.Text), item);
                    NovaLinha(string.Format("5.{0} - Pasta {1} processada com sucesso", cont, item.Split('.')[2]));
                    cont++;
                }

                NovaLinha("6 - Geração realizada com sucesso!");
                MessageBox.Show("Geração realizada com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NovaLinha(string linha, bool primeira = false)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string, bool>(NovaLinhaAsync), new object[] { linha, primeira });
                return;
            }

            if (primeira)
                txtLog.Text = "";

            txtLog.AppendText(string.Format("[{0}] {1}  {2}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), linha, Environment.NewLine));
        }

        public void NovaLinhaAsync(string linha, bool primeira = false)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string, bool>(NovaLinhaAsync), new object[] { linha, primeira });
                return;
            }

            if (primeira)
                txtLog.Text = linha + Environment.NewLine + Environment.NewLine;
            else
                txtLog.Text += linha + Environment.NewLine + Environment.NewLine;
        }

        private void linkAjuda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
    }
}
