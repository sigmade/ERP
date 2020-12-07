using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebERP.Models
{
    public class ExcelExport
    {
        SqlConnection connect;
        public ExcelExport()
        {
            var configuration = GetConfiguration();
            connect = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        }
        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        public DataSet GetSP(string sqlstring)
        {
            SqlCommand command = new SqlCommand(sqlstring, connect);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            return dataSet;
        }

        public DataSet Getquery(string sqlstring)
        {
            SqlCommand command = new SqlCommand(sqlstring, connect);
            command.CommandType = CommandType.Text;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            return dataSet;
        }
    }
}
