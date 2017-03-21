
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Reflection;
using System.Data.Entity;
using BHS.ProjetoBaseMvc.Dados;
using BHS.ProjetoBaseMvc.Dados.Repositorio;
using BHS.ProjetoBaseMvc.Dominio.Base;

namespace BHS.ProjetoBaseMvc.Dados
{
    public partial class Adaptador
    {
		private Contexto contexto;

        public Adaptador(Contexto contextoExistente)
        {
            contexto = contextoExistente;
        }

		// Implementa Repositorios
		#region [ Repositorios ]

		private CidadeRepositorio cidaderepositorio;
        public CidadeRepositorio CidadeRepositorio
        {
            get
            {
                if (this.cidaderepositorio == null)
                {
                    this.cidaderepositorio = new CidadeRepositorio(contexto);
                }
                return this.cidaderepositorio;
            }
        }

		private FuncionalidadeRepositorio funcionalidaderepositorio;
        public FuncionalidadeRepositorio FuncionalidadeRepositorio
        {
            get
            {
                if (this.funcionalidaderepositorio == null)
                {
                    this.funcionalidaderepositorio = new FuncionalidadeRepositorio(contexto);
                }
                return this.funcionalidaderepositorio;
            }
        }

		private MenuRepositorio menurepositorio;
        public MenuRepositorio MenuRepositorio
        {
            get
            {
                if (this.menurepositorio == null)
                {
                    this.menurepositorio = new MenuRepositorio(contexto);
                }
                return this.menurepositorio;
            }
        }

		private PerfilRepositorio perfilrepositorio;
        public PerfilRepositorio PerfilRepositorio
        {
            get
            {
                if (this.perfilrepositorio == null)
                {
                    this.perfilrepositorio = new PerfilRepositorio(contexto);
                }
                return this.perfilrepositorio;
            }
        }

		private SugestoesRepositorio sugestoesrepositorio;
        public SugestoesRepositorio SugestoesRepositorio
        {
            get
            {
                if (this.sugestoesrepositorio == null)
                {
                    this.sugestoesrepositorio = new SugestoesRepositorio(contexto);
                }
                return this.sugestoesrepositorio;
            }
        }

		private UFRepositorio ufrepositorio;
        public UFRepositorio UFRepositorio
        {
            get
            {
                if (this.ufrepositorio == null)
                {
                    this.ufrepositorio = new UFRepositorio(contexto);
                }
                return this.ufrepositorio;
            }
        }

		private UsuarioRepositorio usuariorepositorio;
        public UsuarioRepositorio UsuarioRepositorio
        {
            get
            {
                if (this.usuariorepositorio == null)
                {
                    this.usuariorepositorio = new UsuarioRepositorio(contexto);
                }
                return this.usuariorepositorio;
            }
        }
		#endregion			
	}
}


