using Client.Zeus.App.Custom;
using Client.Zeus.Domain;
using Client.Zeus.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Zeus.App.Controllers
{

    public class PerfilController : CrudControllerBase<TB_PERFIL, BaseSearchCodeDescription<TB_PERFIL>>
    {
        public PerfilController()
        {
            var fields = new[] { new Field("NAME", width: "10") }.ToList<Field>();

            AutomaticModel.Title = "Perfil";
            AutomaticModel.SearchFields = fields;
            AutomaticModel.GridField = fields.Select(o => o.Name).ToList();
            AutomaticModel.Fields = fields;
        }
    }
}