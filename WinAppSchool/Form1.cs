using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Domain;

namespace WinAppSchool
{
    public partial class Form1 : Form
    {
        List<User> UserList = new List<User>();
        List<Sezioni> ClassList = new List<Sezioni>();
        List<SezioniUsers> SezioniUserList = new List<SezioniUsers>();
        List<Role> RoleList = new List<Role>();
        SezioniUsers su = new SezioniUsers();
        User u = new User();
        Sezioni s = new Sezioni();
        Role r = new Role();
        public Form1()
        {
            InitializeComponent();

        }

        private void CaricaSchool_Click(object sender, EventArgs e) //Carica Studenti
        {
            UserList = u.FillArray();
            comboBox1.DataSource = UserList;
        }

        private void button2_Click(object sender, EventArgs e) //Carica Classi box 1
        {
            ClassList = s.FillArray();
            comboBox2.DataSource = ClassList;
        }

        private void button1_Click(object sender, EventArgs e)   //UPDATE CLASSE
        {
            SqlConnection con = new SqlConnection("Data Source = localhost; Initial Catalog = Esercizi; Integrated Security = True");
            try
            {
                string cmdText = "UPDATE ClassUser SET ClassID = @ClassID WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(cmdText, con);
                cmd.Parameters.AddWithValue("@ClassID",comboBox2.SelectedValue.ToString());
                string app = comboBox2.SelectedValue.ToString();
                cmd.Parameters.AddWithValue("UserID", UserList[comboBox1.SelectedIndex].UserID1);
                con.Open();
                cmd.ExecuteNonQuery();
                textBox1.Text = "Cambiata la sezione dell'utente " + UserList[comboBox1.SelectedIndex].UserID1 + " a " + app;
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
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)  
        {
            SezioniUserList = su.FillArray();
            SezioniUsers VarSu = new SezioniUsers();
            VarSu = SezioniUserList.FirstOrDefault(y => y.UserID1 == UserList[comboBox1.SelectedIndex].UserID1);
            comboBox2.SelectedItem = ClassList.FirstOrDefault(x => x.ClassID1 == VarSu.ClassID1);
        }

        private void button3_Click(object sender, EventArgs e)  //CaricaRuolo
        {
            RoleList = r.FillArray();
            CBRuolo.DataSource = RoleList;
        }

        private void BTInsert_Click(object sender, EventArgs e) //INSERT
        {
            SqlConnection con = new SqlConnection("Data Source = localhost; Initial Catalog = Esercizi; Integrated Security = True");
            try
            {
                string cmdText = "INSERT INTO School (UserID,UserTypeID,LastName,FirstName) VALUES(@UserID,@UserTypeID,@LastName,@FirstName)";
                SqlCommand cmd = new SqlCommand(cmdText, con);
                cmd.Parameters.AddWithValue("@UserID", u.TrovaIndice(CBRuolo.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@UserTypeID", RoleList[CBRuolo.SelectedIndex].UserTypeID1);
                cmd.Parameters.AddWithValue("@LastName", TBCognome.Text);
                cmd.Parameters.AddWithValue("@FirstName", TBNome.Text);
                con.Open();
                cmd.ExecuteNonQuery();
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

            try
            {
                string cmdText = "INSERT INTO ClassUser (ClassID,UserID) VALUES(@ClassID,@UserID)";
                SqlCommand cmd = new SqlCommand(cmdText, con);
                cmd.Parameters.AddWithValue("@ClassID", CBCLass2.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@UserID", u.TrovaIndice(CBRuolo.SelectedValue.ToString())-1);
                con.Open();
                cmd.ExecuteNonQuery();
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
        }

        private void button4_Click(object sender, EventArgs e) //Carica Classi box 2
        {
            ClassList = s.FillArray();
            CBCLass2.DataSource = ClassList;
        }
    }
}
