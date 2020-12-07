using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace DBModels
{
    class StoredProcedure
    {
        SqlConnection connect;
        public StoredProcedure()
        {
            var configuration = GetConfiguration();
            connect = new SqlConnection(configuration.GetSection("Data").GetSection("ConnectionString").Value);
        }
        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        public DataSet Getrecord()
        {
            SqlCommand command = new SqlCommand("Sp_Person", connect);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            return dataSet;
        }
    }
}
