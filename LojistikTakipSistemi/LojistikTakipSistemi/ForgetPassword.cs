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
using System.Diagnostics;
using System.Net.Mail;

namespace LojistikTakipSistemi
{
    public partial class ForgetPassword : Form
    {
        public ForgetPassword()
        {
            InitializeComponent();
        }

        private void ForgetPassword_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            maskedTextBox2.Visible = false;
            button3.Visible = false;
        }
        SqlCommand cmd;
        SqlDataReader dr;
        public int rcode;
        public int id;

       // MailMessage eposta = new MailMessage();

      
       
        private void button2_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text=="")
            {
                MessageBox.Show("Lütfen Mail Adresinizi Girin","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                
                SqlConnection baglanti = new SqlConnection(@"Data Source=.; Initial Catalog=takipS;Integrated Security=True");
                cmd = new SqlCommand();
                baglanti.Open();
                cmd.Connection = baglanti;
                cmd.CommandText = "SELECT * FROM Kullanici where MailAdresi='" + maskedTextBox1.Text + "'";
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    id = Convert.ToInt32(dr["Id"]);
                    string mail =dr["MailAdresi"].ToString();
                    Random code = new Random();
                    rcode = code.Next(100000, 999999);
                    MessageBox.Show(rcode.ToString());

                    label1.Visible = true;
                    maskedTextBox2.Visible = true;
                    button3.Visible = true;
                  //  eposta.From = new MailAddress("sgtakipsistemi@outlook.com");
                  //  eposta.To.Add(mail);
                  //  eposta.Subject = "Onay Kodu";
                  //  eposta.Body = "Onay Kodu : "+rcode;

                  //  MailMessage ePosta = new MailMessage();
                  //  ePosta.From = new MailAddress("sgtakipsistemi@outlook.com");

                  //ePosta.To.Add(mail);


                  

                  //  ePosta.Subject = "Yeni Şifre Onay Kodu";


                  //  ePosta.Body = rcode.ToString();

                  //  SmtpClient smtp = new SmtpClient();

                
                  //  smtp.Credentials = new System.Net.NetworkCredential("sgtakipsistemi@outlook.com", "MuhammeD950");

                  //  smtp.Port = 25;
                  //  smtp.Host = "smtp.live.com";
                  //  smtp.EnableSsl = true;
                  //  smtp.SendAsync(ePosta, (object)ePosta);
                  //  smtp.Send(ePosta);

                }
                else
                {
                    MessageBox.Show("Mail Adresi Bulunamadı", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                baglanti.Close();
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }
        int Move;
        int Mouse_X;
        int Mouse_Y;
       
       
        private void ForgetPassword_MouseUp_1(object sender, MouseEventArgs e)
        {
            Move = 0;
        }

        private void ForgetPassword_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void ForgetPassword_MouseDown_1(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (maskedTextBox2.Text == "")
            {
                MessageBox.Show("Lütfen Kod Giriniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int pass = Convert.ToInt32(maskedTextBox2.Text);
                if (pass == rcode)
                {
                    newpassword nps = new newpassword();
                    nps.Id = id;
                    nps.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Hatalı Kod", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
    }
}
