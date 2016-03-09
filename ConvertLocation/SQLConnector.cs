using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertLocation
{
    class SQLConnector
    {
        public void connectToDB()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=ASUS-PC;Database=CRMAppsDb;Trusted_Connection=true";
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Deals", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // while there is another record present
                    while (reader.Read())
                    {
                        // write the data on to the screen
                        Console.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3}",
                            // call the objects from their index
                        reader[0], reader[1], reader[2], reader[3]));
                    }
                }

                conn.Close();
            }
        }
    }
}
