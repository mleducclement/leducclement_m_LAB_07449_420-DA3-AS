using System.Data.SqlClient;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace leducclement_m_LAB_07449_420_DA3_AS.Utils
{
    public class DbUtils<TConnection> where TConnection : DbConnection, new()
    {
        private static readonly string DEFAULT_SERVER = ".\\SQL2019Express";
        private static readonly string DEFAULT_SERVER_DB_NAME = "db_lab";

        private static readonly string DEFAULT_SERVER_USER = "<appDbUser>";
        private static readonly string DEFAULT_SERVER_PASSWD = "<appDbPassword>";
        private static readonly string DEFAULT_DBFILE_NAME = "lab.mdf";

        public static readonly string DB_DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss.FFF";

        private Type _type;

        public Type ConnectionType
        {
            get { return _type; }
            private set { this._type = value; }
        }

        public DbUtils()
        {
            this.ConnectionType = new TConnection().GetType();
        }
    }
}
