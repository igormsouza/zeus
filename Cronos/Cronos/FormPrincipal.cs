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
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();

            var link = new LinkLabel.Link();
            link.LinkData = "https://igormsouza.wordpress.com";
            linkHelp.Links.Add(link);
        }

        private bool UpdateStatus()
        {
            bool generate = false;

            if (string.IsNullOrWhiteSpace(txtOriginPath.Text))
            {
                lblStatus.Text = "You have to select the origin path";
            }
            else if (string.IsNullOrWhiteSpace(txtDestinyPath.Text))
            {
                lblStatus.Text = "You have to select the destiny path";
            }
            else if (string.IsNullOrWhiteSpace(txtClient.Text))
            {
                lblStatus.Text = "You have to enter the Client's name";
            }
            else if (string.IsNullOrWhiteSpace(txtProduct.Text))
            {
                lblStatus.Text = "You have to enter the Product's name";
            }
            else
            {
                lblStatus.Text = "Ready to generate!";
                generate = true;
            }

            return generate;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (UpdateStatus())
            {
                Start();
            }
            else
            {
                MessageBox.Show("You have to complete all fields to generate a new structure.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Start()
        {
            try
            {
                NewLine("Start");
                NewLine(string.Format("Path Origin = {0}", txtOriginPath.Text));
                NewLine(string.Format("Path Destiny = {0}", txtDestinyPath.Text));

                NewLine(string.Format(" Preparing data {0}", Environment.NewLine));

                NewLine("1 - Updating and verifing permission of read and write in a origin path.");
                Util.EditWriteFolder(txtOriginPath.Text, true);

                NewLine("2 - Updating and verifing permission of read and write in a destiny path.");
                Util.EditWriteFolder(txtOriginPath.Text, true);

                NewLine("3 - Cleaning destiny folder.");
                Util.CleanDestiny(txtDestinyPath.Text);

                NewLine("4 - Copy itens from destiny to origin.");
                Util.CopyFolders(txtOriginPath.Text, txtDestinyPath.Text);

                NewLine("5 - Modifying names of folders and files.");
                Util.ZEUS_FOLDER = Util.ZEUS_FOLDER.Select(o => o.Replace("{namespaceAtual}", txtNamespaceOrigin.Text)).ToArray();
                int cont = 1;
                foreach (var item in Util.ZEUS_FOLDER)
                {
                    Util.RenameDestiny(txtDestinyPath.Text, txtNamespaceOrigin.Text, string.Format("{0}.{1}", txtClient.Text, txtProduct.Text), item);
                    NewLine(string.Format("5.{0} - Folder {1} processed with success.", cont, item.Split('.')[2]));
                    cont++;
                }

                NewLine("6 - Success!");
                MessageBox.Show("New struct was generated with success!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NewLine(string line, bool first = false)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string, bool>(NewLineAsync), new object[] { line, first });
                return;
            }

            if (first)
                txtLog.Text = "";

            txtLog.AppendText(string.Format("[{0}] {1}  {2}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), line, Environment.NewLine));
        }

        private void NewLineAsync(string line, bool first = false)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string, bool>(NewLineAsync), new object[] { line, first });
                return;
            }

            if (first)
                txtLog.Text = line + Environment.NewLine + Environment.NewLine;
            else
                txtLog.Text += line + Environment.NewLine + Environment.NewLine;
        }

        private void btnSearchOriginPath_Click(object sender, EventArgs e)
        {
            try
            {
                var fbd = new FolderBrowserDialog();
                fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;

                var result = fbd.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtOriginPath.Text = fbd.SelectedPath;
                    var namespaceAtual = Util.DiscoverNamespace(txtOriginPath.Text);
                    txtNamespaceOrigin.Text = string.Format("{0}.{1}", namespaceAtual.Item1, namespaceAtual.Item2);
                }

                UpdateStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchDestinyPath_Click(object sender, EventArgs e)
        {
            try
            {
                var fbd = new FolderBrowserDialog();
                fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;

                var result = fbd.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtDestinyPath.Text = fbd.SelectedPath;
                }

                UpdateStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
    }
}
