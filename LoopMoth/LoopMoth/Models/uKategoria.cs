using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoopMoth.Models
{
    public class uKategoria
    {
        public string nazwa { get; set; }
        public int id_kategorii { get; set; }
        public uKategoria[] podkategorie { get; set; }
    }
}