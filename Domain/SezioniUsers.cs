using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Domain
{
    public class SezioniUsers
    {
        String ClassID;
        int UserID;

        public SezioniUsers()
        {
        }

        public SezioniUsers(string classID, int userID)
        {
            ClassID1 = classID;
            UserID1 = userID;
        }

        public string ClassID1 { get => ClassID; set => ClassID = value; }
        public int UserID1 { get => UserID; set => UserID = value; }

        public List<SezioniUsers> FillArray()
        {
            List<SezioniUsers> SezioniUserList = new List<SezioniUsers>();
            SqlConnection con = new SqlConnection("Data Source = localhost; Initial Catalog = Esercizi; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("SELECT ClassID,UserID FROM ClassUser WHERE UserID >= 2000", con);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SezioniUsers su = new SezioniUsers();
                    su.ClassID = reader["CLassID"].ToString();
                    su.UserID1 = Convert.ToInt32(reader["UserID"].ToString());
                    SezioniUserList.Add(su);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return SezioniUserList;
        }
    }
}
