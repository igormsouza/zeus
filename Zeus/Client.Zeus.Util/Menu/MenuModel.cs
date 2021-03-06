﻿using Client.Zeus.Util.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Util.Menu
{
    public class MenuModel
    {
        public int ID { get; set; }

        [Map("TITLE")]
        public string Title { get; set; }

        [Map("CONTROLLER")]
        public string Controller { get; set; }

        [Map("ACTION")]
        public string Action { get; set; }

        [Map("IMAGE")]
        public string Image { get; set; }

        [Map("ID_PARENT")]
        public Nullable<int> IdFather { get; set; }

        [Map("URL")]
        public string Url { get; set; }

        [Map("FLAG_NEW_PAGE")]
        public Nullable<bool> NewPage { get; set; }

        public List<MenuModel> Childreen { get; set; }
    }
}
