using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client.Zeus.App.Models;
using Client.Zeus.App.Models.Base;
using Client.Zeus.App.Custom;
using Client.Zeus.Domain;
using Client.Zeus.Domain.Base;
using Client.Zeus.Business.Manager;

namespace Client.Zeus.App.Controllers
{
    [CustomAuthorization]
    public class UserController : CrudControllerBase<TB_USER, BaseSearchCodeDescription<TB_USER>>
    {
        public UserController()
        {
            var fields = new[] {
                new Field("NOME"),
                new Field("LOGIN"),
                new Field("EMAIL"),
                new Field("CPF")
            }.ToList<Field>();

            var fieldsSearch = new[] {
                "NAME", "LOGIN", "EMAIL"
            }.Select(o => new Field(o)).ToList();

            var fielsGrid = new[] {
                "ID", "NAME", "LOGIN", "EMAIL"
            };

            AutomaticModel.Title = "User";
            AutomaticModel.SearchFields = fieldsSearch;
            AutomaticModel.GridField = fielsGrid;
            AutomaticModel.Fields = fields;
        }

        public ActionResult Permissao()
        {
            ViewBag.Functionalities = new FunctionalityManager().List();
            return View();
        }
    }
}
