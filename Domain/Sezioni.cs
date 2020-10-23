using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Domain
{
    public class Sezioni
    {
        String ClassID;

        public Sezioni()
        {
        }
        public Sezioni(string classID)
        {
            ClassID1 = classID;
        }

        public string ClassID1 { get => ClassID; set => ClassID = value; }

        public override string ToString()
        {
            return ClassID;
        }

        public List<Sezioni> FillArray()
        {
            List<Sezioni> ClassList = new List<Sezioni>();
            SqlConnection con = new SqlConnection("Data Source = localhost; Initial Catalog = Esercizi; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("SELECT ClassID FROM Class", con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Sezioni s = new Sezioni();
                    s.ClassID1 = reader["ClassID"].ToString();
                    ClassList.Add(s);
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
            return ClassList;
        }
    }
}
