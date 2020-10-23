using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Domain
{
    public class User
    {
        int UserID;
        int UserTypeID;
        String LastName;
        String FirstName;

        public User()
        {

        }

        public User(int userID, int userTypeID, string lastName, string firstName)
        {
            UserID = userID;
            UserTypeID = userTypeID;
            LastName = lastName;
            FirstName = firstName;
        }

        public int UserID1 { get => UserID; set => UserID = value; }
        public int UserTypeID1 { get => UserTypeID; set => UserTypeID = value; }
        public string LastName1 { get => LastName; set => LastName = value; }
        public string FirstName1 { get => FirstName; set => FirstName = value; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }

        public List<User> FillArray()
        {
            List<User> UserList = new List<User>();
            SqlConnection con = new SqlConnection("Data Source = localhost; Initial Catalog = Esercizi; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("SELECT UserID,UserTypeID,FirstName,LastName FROM School WHERE UserTypeID=1", con);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User u = new User();
                    u.UserID1 = Convert.ToInt32(reader["UserID"].ToString());
                    u.UserTypeID1 = Convert.ToInt32(reader["UserTypeID"].ToString());
                    u.FirstName1 = reader["FirstName"].ToString();
                    u.LastName1 = reader["LastName"].ToString();
                    UserList.Add(u);
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
            return UserList;
        }

        public List<User> FillArrayTutti()
        {
            List<User> UserList = new List<User>();
            SqlConnection con = new SqlConnection("Data Source = localhost; Initial Catalog = Esercizi; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("SELECT UserID,UserTypeID,FirstName,LastName FROM School ORDER BY 1 ASC", con);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User u = new User();
                    u.UserID1 = Convert.ToInt32(reader["UserID"].ToString());
                    u.UserTypeID1 = Convert.ToInt32(reader["UserTypeID"].ToString());
                    u.FirstName1 = reader["FirstName"].ToString();
                    u.LastName1 = reader["LastName"].ToString();
                    UserList.Add(u);
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
            return UserList;
        }

        public int TrovaIndice(String s)
        {
            List<User> UserList = new List<User>();
            User u = new User();
            UserList = u.FillArrayTutti();
            int app = 0;
            if (s=="Studente")
            {
                app = UserList[UserList.Count - 1].UserID + 1;
            }
            else if (s=="Professore")
            {
                List<User> AppList = new List<User>();
                AppList = UserList.Where(x => x.UserTypeID == 2 ).ToList();
                app = AppList[AppList.Count - 1].UserID + 1;
            }
            return app;
        }
    }
}
