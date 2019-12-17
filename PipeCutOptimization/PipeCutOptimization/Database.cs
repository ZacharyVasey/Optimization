using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeCutOptimization
{
    class Database
    {
        protected DbProviderFactory factory;
        protected string provider;
        protected string connectionString;
        public Database(string provider, string connectionString)
        {
            //initialize the database connection
            //string provider = ConfigurationManager.AppSettings["provider"];
            //string connectionString = ConfigurationManager.AppSettings["connectionString"];

            factory = DbProviderFactories.GetFactory(provider);
            this.provider = provider;
            this.connectionString = connectionString;
            
        }

        public List<Pipe> GetRows(string query)
        {
            List<Pipe> returnVal = new List<Pipe>();
            using (DbConnection connection = factory.CreateConnection())
            {
                //if connection fails, return null
                if (connection == null)
                {
                    Console.WriteLine("Database Connection Error");
                    Console.ReadLine();
                    return null;
                }
                connection.ConnectionString = connectionString;
                connection.Open();
                DbCommand command = factory.CreateCommand();
                //if command not created, return null
                if (command == null)
                {
                    Console.WriteLine("Database Command Error");
                    Console.ReadLine();
                    return null;
                }
                command.Connection = connection;
                
                //Enter command
                command.CommandText = query;

                Pipe tempPipe;

                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        //Console.WriteLine($"{dataReader["LENGTH"]}");
                        //Console.WriteLine((double)dataReader["LENGTH"]);
                        tempPipe = new Pipe((double)dataReader["LENGTH"]);
                        tempPipe.ItemNo = (int)dataReader["DB_CODE"];
                        returnVal.Add(tempPipe);
                    }
                }
            }
            return returnVal;  
        }
    }
}
