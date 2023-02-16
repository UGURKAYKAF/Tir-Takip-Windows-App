using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace LojistikTakipSistemi
{
    public partial class takip : Form
    {
        public takip()
        {
            InitializeComponent();
                 
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
        }
        int Move;
        int Mouse_X;
        int Mouse_Y;
        public int f;
        

        //private void Save_Data()
        //{
        //    if (checkBox3.Checked==false)
        //    {
        //        Properties.Settings.Default.Username = "";
        //        Properties.Settings.Default.Password = "";
        //        Properties.Settings.Default.Remember = false;
        //        Properties.Settings.Default.Save();
        //    }
        //    if (remember==false)
        //    {
        //        checkBox3.Visible = true;
        //    }
        //}
        private void takip_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }
        
        private void takip_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
            takip tkp = new takip();
        }

        private void takip_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }
       
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                groupBox2.Visible = false;
                dataGridView1.Width = 1408;
                dataGridView1.Height = 758;
                dataGridView1.Location = new Point(12, 159);
            }
            else
            {
                groupBox2.Visible = true;
                dataGridView1.Width = 1071;
                dataGridView1.Height =758;
                dataGridView1.Location = new Point(349, 159);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked==true)
            {
                groupBox7.Visible = false;
            }
            else
            {
                groupBox7.Visible = true;
            }
        }

        private void maskedTextBox2_Click(object sender, EventArgs e)
        {
            maskedTextBox2.Text = "";
        }

        private void maskedTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
                
        }

        private void takip_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void maskedTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Arama Yapılıyor", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (e.KeyCode==Keys.Escape)
            {
                maskedTextBox2.Text = "Ara";
            }
        }
       
        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btn_mouseleave(object sender, EventArgs e)
        {
          /*  Button btn = sender as Button;
            btn.BackColor = button1.BackColor;
            btn.ForeColor = Color.White;*/
        }

        private void btn_Hover(object sender, EventArgs e)
        {
            /*takip tkp = new takip();
            Button btn = sender as Button;
            btn.BackColor = Color.LightGray;
            btn.ForeColor = Color.Black;*/

        }

        private void btn_MouseMove(object sender, EventArgs e)
        {
           /* takip tkp = new takip();
            Button btn = sender as Button;
            btn.BackColor = Color.Red;*/
        }
        public int durum;
        private void button1_Click(object sender, EventArgs e)
        {
            
                Application.Exit();
            
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Save_Data();
            Form1 frm = new Form1();
            this.Hide();
            frm.Show();
        }
        public int id;
        public int a;
        SqlCommand cmd;
        SqlDataReader dr;
        public bool remember;
        private void takip_Load(object sender, EventArgs e)
        {
            timer1.Start();
            SqlConnection baglanti = new SqlConnection(@"Data Source=.; Initial Catalog=takipS;Integrated Security=True");
            cmd = new SqlCommand();
            baglanti.Open();
            cmd.Connection = baglanti;
            cmd.CommandText = "SELECT * FROM Kullanici where Id='" + id +"'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string CompanyName =dr["FirmaAdi"].ToString();
                string DosyaYolu = dr["ProfilResim"].ToString();
                label1.Text = CompanyName;
               checkBox2.BackgroundImage = Image.FromFile(DosyaYolu);
                
            }
            if (maskedTextBox1.Text=="1")
            {
                button12.Visible = false;
                button13.Visible = true;
            }
            
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        int a = Convert.ToInt32(maskedTextBox1.Text);
        //        int b = Convert.ToInt32(label3.Text);
        //        if (a>b)
        //        {
        //            MessageBox.Show("Sayfa Bulunamadı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Sayfaya Gidiliyor", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            
        }
        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
           
            //if (maskedTextBox1.Text=="")
            //{
            //    maskedTextBox1.Text = "1";
            //}
            //else
            //{
            //    int a = Convert.ToInt32(maskedTextBox1.Text);
            //    int b = Convert.ToInt32(label3.Text);
            //    if (a > b)
            //    {
            //        MessageBox.Show("Sayfa Bulunamadı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Sayfaya Gidiliyor", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addCar car = new addCar();
            car.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Düzenle düzenle = new Düzenle();
            düzenle.id = id;
            düzenle.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=.; Initial Catalog=takipS;Integrated Security=True");
            cmd = new SqlCommand();
            baglanti.Open();
            cmd.Connection = baglanti;
            cmd.CommandText = "SELECT * FROM Kullanici where Id='" + id + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string CompanyName = dr["FirmaAdi"].ToString();
                string DosyaYolu = dr["ProfilResim"].ToString();
                label1.Text = CompanyName;
                checkBox2.BackgroundImage = Image.FromFile(DosyaYolu);

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
