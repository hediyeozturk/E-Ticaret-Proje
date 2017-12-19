using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTicaretProjesi.ENT.ViewModel
{
   public class UsersViewModel
    {
        public string KullaniciID { get; set; }
        public string KullaniciAd { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string RolAd { get; set; }
        public string RolID { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
