using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Student_Management_System
{
    public partial class frm_Add_New_Student_Details : Form
    {
        public frm_Add_New_Student_Details()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Student_Mgt_System_DB;Integrated Security=True");

        void Con_Open()
        {
            if (Con.State != ConnectionState.Open)
            {
                Con.Open();
            }
        }

        void Con_Close()
        {
            if (Con.State != ConnectionState.Closed)
            {
                Con.Close();
            }
        }

        private void Only_Numeric(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (Char)Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void Only_Text(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsLetter(e.KeyChar) || (e.KeyChar == (Char)Keys.Back) || (e.KeyChar == (Char)Keys.Space)))
            {
                e.Handled = true;
            }
        }

        int Auto_Incr()
        {
            int Cnt = 0;

            Con_Open();

            SqlCommand Cmd = new SqlCommand();

            Cmd.Connection = Con;
            Cmd.CommandText = "Select Count(*) from Student_Mgt_System";

            Cnt = Convert.ToInt32(Cmd.ExecuteScalar());

            if (Cnt > 0)
            {
                Cmd.CommandText = "Select Max(Roll_No) from Student_Mgt_System";

                Cnt = Convert.ToInt32(Cmd.ExecuteScalar()) + 1;
            }
            else
            {
                Cnt = 101;
            }

            Con_Close();

            return Cnt;
        }

          void Clear_Controls()
        {
            tb_Roll_No.Text = Convert.ToString(Auto_Incr());
            tb_Name.Clear();
            tb_Mobile_No.Clear();
            cmb_Course.SelectedIndex = -1;
            dtp_DOB.Text = "14/04/2005";
        }

          private void frm_Add_New_Student_Details_Load(object sender, EventArgs e)
          {
              Clear_Controls();
              tb_Roll_No.Focus();
          }

          private void btn_Save_Click(object sender, EventArgs e)
          {
              Con_Open();

            if (tb_Roll_No.Text != "" && tb_Name.Text != "" && tb_Mobile_No.Text != "" && cmb_Course.Text != "")
            {
                SqlCommand Cmd = new SqlCommand();

                Cmd.Connection = Con;
                Cmd.CommandText = "Insert Into Student_Mgt_System (Roll_No,Name,DOB,Mobile_No,Course) values (@RNo, @Name, @DOB, @MNo, @Course)";
                Cmd.Parameters.Add("RNo", SqlDbType.Int).Value = tb_Roll_No.Text;
                Cmd.Parameters.Add("Name", SqlDbType.VarChar).Value = tb_Name.Text;
                Cmd.Parameters.Add("DOB", SqlDbType.Date).Value = dtp_DOB.Value.Date;
                Cmd.Parameters.Add("MNo", SqlDbType.Decimal).Value = tb_Mobile_No.Text;
                Cmd.Parameters.Add("Course", SqlDbType.NVarChar).Value = cmb_Course.Text;
                Cmd.ExecuteNonQuery();
               
                MessageBox.Show("Record saved");
                Clear_Controls();
            }
            else
            {
                MessageBox.Show("Fill all fields");
            }
            Con_Close();
         }

          private void btn_View_Student_List_Click(object sender, EventArgs e)
          {
              frm_View_Student_List obj = new frm_View_Student_List();
              obj.Show();
              this.Hide();
          }

          private void btn_Search_Student_Details_Click(object sender, EventArgs e)
          {
              frm_Search_Student_Details obj = new frm_Search_Student_Details();
              obj.Show();
              this.Hide();
          }

          private void btn_LogOut_Click(object sender, EventArgs e)
          {
              DialogResult Res = MessageBox.Show("Are You Sure Want To Log Out???", "LOGOUT", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

              if (Res == DialogResult.Yes)
              {
                  frm_Login obj = new frm_Login();
                  obj.Show();
                  this.Hide();

              }
          }
    }
}
    

