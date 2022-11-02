using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGM_Student_Information
{
    public partial class frm_View_Student_Details : Form
    {
        public frm_View_Student_Details()
        {
            InitializeComponent();
        }

        private void frm_View_Student_Details_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'student_DetailsDataSet.Student_Details' table. You can move, or remove it, as needed.
            this.student_DetailsTableAdapter.Fill(this.student_DetailsDataSet.Student_Details);

        }

        private void lbl_Add_New_Student_Click(object sender, EventArgs e)
        {
            frm_Add_New_Student_Details obj = new frm_Add_New_Student_Details();
            obj.Show();
            this.Hide();
        }

        private void btn_Log_Out_Click(object sender, EventArgs e)
        {
            frm_Login lg = new frm_Login();
            lg.Show();
            this.Hide();

        }
    }       
}

