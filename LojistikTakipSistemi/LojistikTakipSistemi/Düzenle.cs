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
using System.Data.Sql;

namespace LojistikTakipSistemi
{
    public partial class Düzenle : Form
    {
        public Düzenle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=.; Initial Catalog=takipS;Integrated Security=True");
        public int id;
        public string DosyaYolu;
        SqlCommand cmd;
        SqlDataReader dr;
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void Düzenle_Load(object sender, EventArgs e)
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
                string sifre = dr["Sifre"].ToString();
                string tel = dr["Telefon"].ToString();
                string adres=dr["Adres"].ToString();
                string DosyaYolu = dr["ProfilResim"].ToString();
                string mail = dr["MailAdresi"].ToString();
                maskedTextBox5.Text = CompanyName;
                pictureBox1.BackgroundImage = Image.FromFile(DosyaYolu);
                maskedTextBox1.Text = sifre;
                maskedTextBox2.Text = sifre;
                maskedTextBox4.Text = tel;
                maskedTextBox3.Text = mail;
                richTextBox1.Text = adres;
            }
            

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
                    pictureBox1.BackgroundImage = Image.FromFile(DosyaYolu);
                }

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (maskedTextBox1.Text == maskedTextBox2.Text)
                {
                    SqlConnection baglanti = new SqlConnection(@"Data Source=.; Initial Catalog=takipS;Integrated Security=True");
                    cmd = new SqlCommand();
                    baglanti.Open();
                    cmd.Connection = baglanti;
                    string komut = "Update Kullanici set FirmaAdi=@FirmaAdi,MailAdresi=@MailAdresi,Sifre=@Sifre,Telefon=@Telefon,Adres=@Adres,ProfilResim=@ProfilResim Where Id=" + id;
                    cmd = new SqlCommand(komut, baglanti);
                    cmd.Parameters.AddWithValue("@MailAdresi", maskedTextBox3.Text);
                    cmd.Parameters.AddWithValue("@FirmaAdi", maskedTextBox5.Text);
                    cmd.Parameters.AddWithValue("@Sifre", maskedTextBox1.Text);
                    cmd.Parameters.AddWithValue("@Telefon", maskedTextBox4.Text);
                    cmd.Parameters.AddWithValue("@Adres", richTextBox1.Text);
                    cmd.Parameters.AddWithValue("@ProfilResim", DosyaYolu);
                    cmd.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Düzenleme Başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Şifreler Uyuşmuyor", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void Düzenle_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }

        private void Düzenle_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void Düzenle_MouseDown(object sender, MouseEventArgs e)
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

        private void maskedTextBox4_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
