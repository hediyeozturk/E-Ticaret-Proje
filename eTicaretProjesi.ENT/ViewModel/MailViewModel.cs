using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTicaretProjesi.ENT.ViewModel
{
    public class MailViewModel
    {
        public string Kime { get; set; }
        public List<String> Kimlere { get; set; } = new List<string>();
        public string Konu { get; set; }
        public string Mesaj { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
    }
}
