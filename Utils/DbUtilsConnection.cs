using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leducclement_m_LAB_07449_420_DA3_AS.Utils
{
    internal class DbUtilsConnection
    {
        public static readonly string EXECUTION_DIRECTORY = Path.GetFullPath(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));

        public static readonly string DB_FILE_PATH = Path.GetFullPath(EXECUTION_DIRECTORY + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "lab.mdf");

        public static readonly string DB_DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss.FFF";

        public static SqlConnection GetDefaultConnection()
        {
            string connString = "Server=.\\SQL2019EXPRESS; Integrated_security=true; " + $"AttachDbFilename={DB_FILE_PATH}; User Instance=true;";

            Debug.WriteLine($"Connection string: [{connString}].");

            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
    }
}
