using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Domain
{
    public class Role
    {
        int UserTypeID;
        string Definition;

        public Role()
        {

        }
        public Role(int userTypeID, string definition)
        {
            UserTypeID1 = userTypeID;
            Definition1 = definition;
        }

        public int UserTypeID1 { get => UserTypeID; set => UserTypeID = value; }
        public string Definition1 { get => Definition; set => Definition = value; }

        public List<Role> FillArray()
        {
            List<Role> RoleList = new List<Role>();
            SqlConnection con = new SqlConnection("Data Source = localhost; Initial Catalog = Esercizi; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM UserType", con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Role r = new Role();
                    r.UserTypeID = Convert.ToInt32(reader["UserTypeID"]);
                    r.Definition = reader["Definition"].ToString();
                    RoleList.Add(r);
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
            return RoleList;
        }

        public override string ToString()
        {
            return Definition;
        }
    }
}
