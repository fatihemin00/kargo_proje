using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kargo_takip1.Models
{
    public class KargoViewModel
    {
        public List<GelenKargoTakip> GelenKargoList { get; set; }
        public List<GidenKargoTakip> GidenKargoList { get; set; }

        // Constructor
        public KargoViewModel()
        {
            GelenKargoList = new List<GelenKargoTakip>();
            GidenKargoList = new List<GidenKargoTakip>();
        }
    }
}