using System;
using System.Data.SqlClient;
using DrugHouse.Shared.Helpers;

namespace DbUpdateDelivery
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var conn = new SqlConnection(Module.ConnectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand(Module.SqlText, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }

            }
            catch (Exception ex)
            {      
                Console.WriteLine("There are some error(s):");                                       
                Console.WriteLine(Helper.GetInnerMostException(ex).Message);
                Console.ReadKey(true);
            } 
        }
    }
}
