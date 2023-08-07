using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Helpers
{
    internal interface IDBHelper
    {
        DataSet ExecuteProc(string procedure, params SqlParameter[] sqlParameters);
        int ExecuteProcNonQuery(string procedure, params SqlParameter[] sqlParameters);
    }
}
