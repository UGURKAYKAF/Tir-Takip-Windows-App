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
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
        }
        SqlCommand cmd;
        SqlDataReader dr;
        public string eposta, sifre, adres;
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void AdminPanel_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }

        private void AdminPanel_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void AdminPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public int id;
        SqlConnection baglanti = new SqlConnection(@"Data Source=.; Initial Catalog=takipS;Integrated Security=True");
        
        private void button2_Click(object sender, EventArgs e)
        {
            //baglanti.Open();
            //cmd = new SqlCommand();
            //cmd.Connection = baglanti;
            //dr = cmd.ExecuteReader();
            if (radioButton2.Checked)
            {
                dataGridView1.Visible = true;
                if (checkBox3.Checked && checkBox4.Checked)
                {
                    SqlDataAdapter dAdapter = new SqlDataAdapter("Select * From Kullanici", baglanti);
                    DataTable dtable = new DataTable();
                    dAdapter.Fill(dtable);
                    dataGridView1.DataSource = dtable;
                    baglanti.Close();
                }
                else if (checkBox3.Checked)
                {
                    baglanti.Open();
                    SqlDataAdapter dAdapter = new SqlDataAdapter("Select * From Kullanici Where Durum = 1", baglanti);
                    DataTable dtable = new DataTable();
                    dAdapter.Fill(dtable);
                    dataGridView1.DataSource = dtable;
                    baglanti.Close();
                }
                else if (checkBox4.Checked)
                {
                    baglanti.Open();
                    SqlDataAdapter dAdapter = new SqlDataAdapter("Select * From Kullanici Where Durum = 0", baglanti);
                    DataTable dtable = new DataTable();
                    dAdapter.Fill(dtable);
                    dataGridView1.DataSource = dtable;
                    baglanti.Close();
                }
                else
                {
                    MessageBox.Show("Filtreleme Seçilmedi", "Hata", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
            else if(radioButton1.Checked)
            {
                dataGridView1.Visible = false;
                while (dr.Read())
                {

                    id = Convert.ToInt32(dr["Id"]);
                    eposta += dr["MailAdresi"].ToString();
                    sifre += dr["Sifre"].ToString();
                    adres += dr["Adres"].ToString();

                }
                baglanti.Close();
                for (int i = 0; i < 10; i++)
                {
                    foreach (Button btn in this.Controls.OfType<Button>())
                    {
                        this.Controls.Remove(btn);
                    }
                }
                int satir = 0;
                int sutun = 0;

                for (int i = 0; i < id; i++)
                {

                    Button btn = new Button();

                    btn.Text = "Kullanıcı İd: " + id.ToString() + "\n Mail Adresi : " + eposta + "\n Şifresi : " + sifre + "\n Adresi : " + adres + "\n Kullanıcıyı Silmek İçin Tıklayınız";
                    btn.Size = new Size(350, 180);
                    //btn.Location = new Point(400 * sutun, 230 * satir);
                    btn.Location = new Point(359 * (sutun + 1), 230 * (satir + 1));
                    btn.Name = (i + 1).ToString();
                    id = Convert.ToInt32(btn.Name);
                    btn.ForeColor = Color.Black;
                    btn.BackColor = Color.Beige;
                    this.Controls.Add(btn);
                    sutun++;
                    if (sutun == 3) { satir++; sutun = 0; }
                    //btn.Text = "";
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                groupBox7.Visible = false;
            }
            else
            {
                groupBox7.Visible = true;
            }
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter dAdapter = new SqlDataAdapter("Select * From Kullanici Where Id Like '"+maskedTextBox2.Text+"'", baglanti);
            DataTable dtable = new DataTable();
            dAdapter.Fill(dtable);
            dataGridView1.DataSource = dtable;
            baglanti.Close();
            if (maskedTextBox2.Text=="")
            {
                baglanti.Open();
                SqlDataAdapter dAdapter2 = new SqlDataAdapter("Select * From Kullanici", baglanti);
                DataTable dtable2 = new DataTable();
                dAdapter.Fill(dtable2);
                dataGridView1.DataSource = dtable2;
                baglanti.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminEkle Admin = new AdminEkle();
            Admin.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }
        string sorgu;
        public int Kid;
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {


                Kid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                DialogResult result2 = MessageBox.Show(Kid + " İd'li Kullanıcıyı Silmek İstiyormusunuz", "Bilgilendirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result2 == DialogResult.Yes)
                {
                    if (baglanti.State == ConnectionState.Closed)
                        baglanti.Open();
                    sorgu = "Delete From Kullanici Where Id = '" + Kid + "'";
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.ExecuteNonQuery();

                    baglanti.Close();

                    baglanti.Open();
                    SqlDataAdapter dAdapter = new SqlDataAdapter("Select * From Kullanici", baglanti);
                    DataTable dtable = new DataTable();
                    dAdapter.Fill(dtable);
                    dataGridView1.DataSource = dtable;
                    baglanti.Close();
                }

            }
            catch (Exception Hata)
            {
                MessageBox.Show("Kullanıcı Bulunamadı", "Hata", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Düzenle dz = new Düzenle();
            dz.id = id;
            dz.ShowDialog();
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
                string mail = dr["MailAdresi"].ToString();
                string CompanyName = dr["FirmaAdi"].ToString();
                string DosyaYolu = dr["ProfilResim"].ToString();
                string durum = dr["Durum"].ToString();
                checkBox2.BackgroundImage = Image.FromFile(DosyaYolu);
                label1.Text = CompanyName;
               
            } 
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int durum;
            //DialogResult result= MessageBox.Show("Hangi İşlem Yapılsın ?", "Bilgi", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            //if (result==DialogResult.Cancel)
            //{
            //    try
            //    {
            //        durum = Convert.ToInt32(dataGridView1.CurrentRow.Cells[7].Value);
            //        if (durum == 1)
            //        {
            //            takip takip = new takip();
            //            takip.durum = 1;
            //            takip.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            //            takip.ShowDialog();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Admin Profiline Girilemez", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //    catch (Exception Hata2)
            //    {
            //        MessageBox.Show("Kullanıcı Bulunamadı", "Hata", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            //    }
               
            //}
            //else
            //{
                
            //}
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            MessageBoxManager.OK = "Sil";
            MessageBoxManager.Cancel = "Profil";
            MessageBoxManager.Yes = "Evet";
            MessageBoxManager.No = "Hayır";
            MessageBoxManager.Retry = "Tamam";
            MessageBoxManager.Register();
            timer1.Start();
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SqlConnection baglanti = new SqlConnection(@"Data Source=.; Initial Catalog=takipS;Integrated Security=True");
            cmd = new SqlCommand();
            baglanti.Open();
            cmd.Connection = baglanti;
            cmd.CommandText = "SELECT * FROM Kullanici where Id='" + id + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string mail = dr["MailAdresi"].ToString();
                string CompanyName = dr["FirmaAdi"].ToString();
                string DosyaYolu = dr["ProfilResim"].ToString();
                checkBox2.BackgroundImage = Image.FromFile(DosyaYolu);
                label1.Text = CompanyName;
            }


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                groupBox1.Visible = false;
                dataGridView1.Width = 1408;
                dataGridView1.Height = 884;
                dataGridView1.Location = new Point(12, 204);
            }
            else
            {
                dataGridView1.Width = 1148;
                dataGridView1.Height = 884;
                dataGridView1.Location = new Point(272,204);
                groupBox1.Visible = true;
            }
        }
    }
}

//while (dr.Read())
//{

//    id = Convert.ToInt32(dr["Id"]);
//    eposta += dr["MailAdresi"].ToString();
//    sifre += dr["Sifre"].ToString();
//    adres += dr["Adres"].ToString();

//}
//baglanti.Close();
//for (int i = 0; i < 3; i++)
//{
//    for (int j = 0; j < 3; j++)
//    {
//        //k++;
//        //GroupBox btn = new GroupBox();
//        //btn.Location = new Point(359 * (j + 1), 230 * (i + 1));
//        //btn.Size = new Size(350, 180);
//        //btn.Text += k.ToString();
//        //Name = id.ToString();
//        //this.Controls.Add(btn);
//        //btn.ForeColor = Color.Black;
//        //btn.BackColor = Color.Beige;

//        //Button delete = new Button();
//        //delete.Location = new Point(359 * (j + 1), 270 * (i + 1));
//        //delete.Size = new Size(350, 80);
//        //delete.Text += "Sil";
//        //Name = "Delete"+k;
//        //this.Controls.Add(delete);
//        //delete.ForeColor = Color.Black;
//        //delete.BackColor = Color.Red;



//    }
//}
//for (int i = 0; i < 10; i++)
//{
//    foreach (Button btn in this.Controls.OfType<Button>())
//    {
//        this.Controls.Remove(btn);
//    }
//}
//int satir = 0;
//int sutun = 0;

//for (int i = 0; i < id; i++)
//{

//    Button btn = new Button();

//    btn.Text = "Kullanıcı İd: " + id.ToString()+"\n Mail Adresi : "+eposta+"\n Şifresi : "+sifre+"\n Adresi : "+adres+"\n Kullanıcıyı Silmek İçin Tıklayınız";
//    btn.Size = new Size(350, 180);
//    //btn.Location = new Point(400 * sutun, 230 * satir);
//    btn.Location = new Point(359 * (sutun + 1), 230 * (satir + 1));
//    btn.Name = (i+1).ToString();
//    kullaniciId = Convert.ToInt32(btn.Name);
//    btn.ForeColor = Color.Black;
//    btn.BackColor = Color.Beige;
//    this.Controls.Add(btn);
//    sutun++;
//    btn.Click += btn_Click;
//    if (sutun == 3) { satir++; sutun = 0; }
//    //btn.Text = "";
//}