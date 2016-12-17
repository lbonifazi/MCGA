using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace ViewModels
{
    public class HomeModel : BaseModel
    {
        public bool IsAdmin;

        public IList<Books> BookList;

        public IList<Subject> Subjects;
    }
}
