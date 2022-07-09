using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Bee
{
    class DBConnection
    {
        public SqlConnection getConnection()
        {
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection("data source = DESKTOP-ABD4GBM\\SQLEXPRESS; initial catalog = WA_NET_83_16; user id = sa; password = Ruwandika");
                //sqlCon = new SqlConnection("data source = 10.0.0.4 ; initial catalog = WAD_HND_83_16; Trusted Connection = True);
                sqlCon.Open();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return sqlCon;
        }
    }
}
