using Client.Zeus.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Domain.Base
{
    public class AutomaticModel
    {
        public AutomaticModel(bool notLoad = false)
        {
            NotLoad = notLoad;
            ShowNew = true;
            ShowDelete = true;
            ShowEdit = true;
        }

        public bool NotLoad { get; set; }

        public string Title { get; set; }

        public Redirect Redirect { get; set; }

        private IList<Field> searchFields;
        public IList<Field> SearchFields
        {
            get
            {
                if (searchFields == null)
                {
                    searchFields = new List<Field>();
                }
                return searchFields;
            }
            set
            {
                searchFields = value;
            }
        }

        private IList<string> gridField;
        public IList<string> GridField
        {
            get
            {
                if (gridField == null)
                {
                    gridField = new List<string>();
                }
                return gridField;
            }
            set
            {
                gridField = value;
            }
        }

        private IList<string> GridColumns;
        public IList<string> gridColumns
        {
            get
            {
                if (GridColumns == null)
                {
                    GridColumns = new List<string>();
                }
                return GridColumns;
            }
            set
            {
                GridColumns = value;
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

        private List<LinkField> actions;
        public List<LinkField> Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new List<LinkField>();
                }
                return actions;
            }
            set
            {
                actions = value;
            }
        }

        private List<Field> fields;
        public List<Field> Fields
        {
            get
            {
                if (fields == null)
                {
                    fields = new List<Field>();
                }
                return fields;
            }
            set
            {
                fields = value;
            }
        }

        private List<Field> hiddenField;
        public List<Field> HiddenField
        {
            get
            {
                if (hiddenField == null)
                {
                    hiddenField = new List<Field>();
                }
                return hiddenField;
            }
            set
            {
                hiddenField = value;
            }
        }

        public bool ShowNew { get; set; }

        public bool ShowDelete { get; set; }

        public bool ShowEdit { get; set; }

        public bool ShowClean { get; set; }

        public string IncludedTables { get; set; }

        private IList<DynamicContent> dynamicContent;
        public IList<DynamicContent> DynamicContent
        {
            get
            {
                if (dynamicContent == null)
                {
                    dynamicContent = new List<DynamicContent>();
                }
                return dynamicContent;
            }
            set
            {
                dynamicContent = value;
            }
        }

        private IList<DynamicContent> conteudoDinamicoInserir;
        public IList<DynamicContent> ConteudoDinamicoInserir
        {
            get
            {
                if (conteudoDinamicoInserir == null)
                {
                    conteudoDinamicoInserir = new List<DynamicContent>();
                }
                return conteudoDinamicoInserir;
            }
            set
            {
                conteudoDinamicoInserir = value;
            }
        }

        private IList<DynamicContent> conteudoDinamicoEditar;
        public IList<DynamicContent> ConteudoDinamicoEditar
        {
            get
            {
                if (conteudoDinamicoEditar == null)
                {
                    conteudoDinamicoEditar = new List<DynamicContent>();
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

public enum enumFieldType : int
{
    Default = 0,
    TextArea = 1,
    CustomizedParcial = 2,
    CustomizedHtml = 3,
    Label = 4
}
public enum enumDynamicContentType : int
{
    CustomizedParcial = 0,
    CustomizedHtml = 1
}
public enum enumIncludedReference : int
{
    After = 0,
    Before = 1,
    Son = 2
}

#endregion