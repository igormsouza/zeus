using BHS.ProjetoBaseMvc.Dominio.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Dominio.Base
{
    public class ModelAutomaticoTela
    {
        public ModelAutomaticoTela(bool naoCarregado = false)
        {
            NaoCarregado = naoCarregado;
            ExibirNovo = true;
            ExibirExcluir = true;
            ExibirEditar = true;
        }

        public bool NaoCarregado { get; set; }

        public string Titulo { get; set; }

        public Redirecionar Redirecionar { get; set; }

        private List<Campo> camposPesquisa;
        public List<Campo> CamposPesquisa
        {
            get
            {
                if (camposPesquisa == null)
                {
                    camposPesquisa = new List<Campo>();
                }
                return camposPesquisa;
            }
            set
            {
                camposPesquisa = value;
            }
        }

        private IList<string> camposGrid;
        public IList<string> CamposGrid
        {
            get
            {
                if (camposGrid == null)
                {
                    camposGrid = new List<string>();
                }
                return camposGrid;
            }
            set
            {
                camposGrid = value;
            }
        }

        private IList<string> camposGridNome;
        public IList<string> CamposGridNome
        {
            get
            {
                if (camposGridNome == null)
                {
                    camposGridNome = new List<string>();
                }
                return camposGridNome;
            }
            set
            {
                camposGridNome = value;
            }
        }

        private IList<string> linksGrid;
        public IList<string> LinksGrid
        {
            get
            {
                if (linksGrid == null)
                {
                    linksGrid = new List<string>();
                }
                return linksGrid;
            }
            set
            {
                linksGrid = value;
            }
        }

        private List<CampoLink> acoes;
        public List<CampoLink> Acoes
        {
            get
            {
                if (acoes == null)
                {
                    acoes = new List<CampoLink>();
                }
                return acoes;
            }
            set
            {
                acoes = value;
            }
        }

        private List<Campo> campos;
        public List<Campo> Campos
        {
            get
            {
                if (campos == null)
                {
                    campos = new List<Campo>();
                }
                return campos;
            }
            set
            {
                campos = value;
            }
        }

        private List<Campo> camposOcultos;
        public List<Campo> CamposOcultos
        {
            get
            {
                if (camposOcultos == null)
                {
                    camposOcultos = new List<Campo>();
                }
                return camposOcultos;
            }
            set
            {
                camposOcultos = value;
            }
        }

        public bool ExibirNovo { get; set; }

        public bool ExibirExcluir { get; set; }

        public bool ExibirEditar { get; set; }

        public bool ExibirLimpar { get; set; }

        public string IncluirTabelas { get; set; }

        private IList<ConteudoDinamico> conteudoDinamico;
        public IList<ConteudoDinamico> ConteudoDinamico
        {
            get
            {
                if (conteudoDinamico == null)
                {
                    conteudoDinamico = new List<ConteudoDinamico>();
                }
                return conteudoDinamico;
            }
            set
            {
                conteudoDinamico = value;
            }
        }

        private IList<ConteudoDinamico> conteudoDinamicoInserir;
        public IList<ConteudoDinamico> ConteudoDinamicoInserir
        {
            get
            {
                if (conteudoDinamicoInserir == null)
                {
                    conteudoDinamicoInserir = new List<ConteudoDinamico>();
                }
                return conteudoDinamicoInserir;
            }
            set
            {
                conteudoDinamicoInserir = value;
            }
        }

        private IList<ConteudoDinamico> conteudoDinamicoEditar;
        public IList<ConteudoDinamico> ConteudoDinamicoEditar
        {
            get
            {
                if (conteudoDinamicoEditar == null)
                {
                    conteudoDinamicoEditar = new List<ConteudoDinamico>();
                }
                return conteudoDinamicoEditar;
            }
            set
            {
                conteudoDinamicoEditar = value;
            }
        }
    }
}

#region Enuns

public enum enumTipoCampo : int
{
    Padrao = 0,
    TextArea = 1,
    CustomizadoParcial = 2,
    CustomizadoHtml = 3,
    Label = 4
}
public enum enumTipoConteudoDinamico : int
{
    CustomizadoParcial = 0,
    CustomizadoHtml = 1
}
public enum enumInclusaoReferencia : int
{
    Depois = 0,
    Antes = 1,
    Filho = 2
}

#endregion