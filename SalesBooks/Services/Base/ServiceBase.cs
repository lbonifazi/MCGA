using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceBase
    {
        #region Consts
        private const string DBContextName = "DBContext";
        #endregion

        #region Properties
        protected static DB DB
        {
            get
            {
                // Find DB on this thread's bag
                DB db = CallContext.GetData(DBContextName) as DB;

                // If it doesn't exists, create it
                if (db == null)
                {
                    db = new DB();

                    // Save the context on the thread's bag
                    CallContext.SetData(DBContextName, db);
                }

                return db;
            }
        }
        #endregion
    }
}
