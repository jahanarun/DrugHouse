using System.Data.SqlClient;

namespace DbUpdateDelivery
{
    class Program
    {
        static void Main(string[] args)
        {
            
            using (var conn = new SqlConnection(Module.ConnectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(Module.SqlText, conn);
                cmd.ExecuteNonQuery();  
                conn.Close();
                
            }
        }
    }
}
