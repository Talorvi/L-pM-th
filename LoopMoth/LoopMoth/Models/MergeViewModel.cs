using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace LoopMoth.Models
{
    public class MergeViewModel
    {
        [DisplayName("Tytuł")]
        public string tytul { get; set; }
        [DisplayName("Język")]
        public string jezyk { get; set; }
        [DisplayName("Rodzaj")]
        public string rodzaj { get; set; }
        [DisplayName("Rok publikacji")]
        public Nullable<int> rok_publikacji { get; set; }
        [DisplayName("Słowa kluczowe")]
        public string slowa_kluczowe { get; set; }
        [DisplayName("Wydawca")]
        public string wydawca { get; set; }
        [DisplayName("Autorzy")]
        public ICollection<string> autorzy { get; set; }
        [DisplayName("Kategorie")]
        public ICollection<string> kategorie { get; set; }
    }
}