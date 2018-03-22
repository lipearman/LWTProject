using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MotorClaimRESTService.Context
{
    public class DataClasses_MotorClaimExt : DataClasses_MotorClaimDataContext
    {
        private static string ConnectionString
        {
            get
            {
                string Conn = ConfigurationManager.ConnectionStrings["MotorClaimConnectionString"].ConnectionString;
                return Conn;
            }
        }


        public DataClasses_MotorClaimExt() 
        {
            // Uses constructor initializer.
            this.Connection.ConnectionString = ConnectionString;
        }
        public DataClasses_MotorClaimExt(string ConnectionString): base()
        {
            this.Connection.ConnectionString = ConnectionString;
        }
    }


    public class DataClasses_PortalExt : DataClasses_PortalDataContext
    {
        private static string ConnectionString
        {
            get
            {
                string Conn = ConfigurationManager.ConnectionStrings["MobilePortalConnectionString"].ConnectionString;
                return Conn;
            }
        }


        public DataClasses_PortalExt()
        {
            // Uses constructor initializer.
            this.Connection.ConnectionString = ConnectionString;
        }
        public DataClasses_PortalExt(string ConnectionString)
            : base()
        {
            this.Connection.ConnectionString = ConnectionString;
        }
    }
}