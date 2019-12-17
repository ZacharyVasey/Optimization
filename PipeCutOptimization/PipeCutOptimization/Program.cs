using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Configuration;
namespace PipeCutOptimization
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            // Database connection
            //
            string provider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.AppSettings["connectionString"];

            Database db = new Database(provider, connectionString);

            List<Pipe> pipeList = db.GetRows("SELECT DB_CODE, LENGTH FROM MATL23DPipeModel WHERE ALPHA_SIZE = '4\"'");

            Console.WriteLine(ListToString(pipeList));
            Console.ReadKey(true);
        }

        public static string ListToString(List<Pipe> l)
        {
            string temp = "{\n";
            foreach (var pipe in l)
            {
                temp = temp + pipe.ToString() + "\n";
            }
            temp = temp + "}";
            return temp;
        }
    }
}
