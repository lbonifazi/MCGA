using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class BaseModel
    {
        public string UserName;

        public bool IsLogged;

        public NavbarModel NavbarData { get; set; }
        public FooterModel FooterData { get; set; }
    }
}
