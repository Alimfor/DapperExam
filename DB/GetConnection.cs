using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DB
{
    public class GetConnection
    {
        public static IDbConnection getConnection { get => new SqlConnection("Data Source=223-2;Initial Catalog=forExam;Trusted_Connection=true;"); } 
    }
}
