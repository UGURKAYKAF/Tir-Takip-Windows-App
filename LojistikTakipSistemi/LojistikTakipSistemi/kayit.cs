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
    public partial class kayit : Form
    {
       
        public kayit()
        {
            InitializeComponent();
        }
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void kayit_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }

        private void kayit_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void kayit_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=.; Initial Catalog=takipS;Integrated Security=True");
        string sorgu;
        public string DosyaYolu;
        public string mail;
        SqlCommand cmd;
        SqlDataReader dr;
        public bool Kontrol()
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=.; Initial Catalog=takipS;Integrated Security=True");
                 baglanti.Open();
            cmd = new SqlCommand();
           
            cmd.Connection = baglanti;
            cmd.CommandText = "SELECT * FROM Kullanici where MailAdresi='" + maskedTextBox3.Text + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {


            if (Kontrol()==true)
            {
                MessageBox.Show("Mail Adresi Kullanılmakta", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (maskedTextBox1.Text == "" || maskedTextBox2.Text == "" || maskedTextBox3.Text == "" || maskedTextBox4.Text == "" || maskedTextBox5.Text == "")
                {
                    MessageBox.Show("Kutucuklar Boş Geçilemez", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (maskedTextBox1.Text != maskedTextBox2.Text)
                {
                    MessageBox.Show("Şifreler Uyuşmuyor", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (maskedTextBox3.Text == mail)
                {
                    MessageBox.Show("Girdiğiniz Mail Adresi Kullanılmakta", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (baglanti.State == ConnectionState.Closed)
                        baglanti.Open();
                    sorgu = "insert into Kullanici (FirmaAdi,MailAdresi,Sifre,Telefon,Adres,ProfilResim,Durum)VALUES(@FirmaAdi,@MailAdresi,@Sifre,@Telefon,@Adres,@ProfilResim,1)";
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@FirmaAdi", maskedTextBox5.Text);
                    komut.Parameters.AddWithValue("@MailAdresi", maskedTextBox3.Text);
                    komut.Parameters.AddWithValue("@Sifre", maskedTextBox1.Text);
                    komut.Parameters.AddWithValue("@Telefon", maskedTextBox4.Text);
                    komut.Parameters.AddWithValue("@Adres", richTextBox1.Text);
                    komut.Parameters.AddWithValue("@ProfilResim", DosyaYolu);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kayıt Başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1 frm = new Form1();
                    this.Hide();
                    frm.Show();
                }
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                maskedTextBox1.PasswordChar = '\0';
                checkBox1.ImageIndex = 1;
            }
            else
            {
                maskedTextBox1.PasswordChar = '*';
                checkBox1.ImageIndex = 2;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                maskedTextBox2.PasswordChar = '\0';
                checkBox2.ImageIndex = 1;
            }
            else
            {
                maskedTextBox2.PasswordChar = '*';
                checkBox2.ImageIndex = 2;
            }
        }

        private void kayit_Load(object sender, EventArgs e)
        {
            
        }
      
        
        private void button9_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())

            {

                dlg.Title = "Open Image";

                dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";



                if (dlg.ShowDialog() == DialogResult.OK)

                {
                    if (baglanti.State == ConnectionState.Closed)
                        baglanti.Open();
                    DosyaYolu = dlg.FileName;
                    button9.Text = dlg.SafeFileName;
                }

            }
        }
    }
}
