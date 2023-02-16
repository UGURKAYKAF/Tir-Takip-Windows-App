using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LojistikTakipSistemi
{
    public partial class addCar : Form
    {
        public addCar()
        {
            InitializeComponent();
        }

        private void addCar_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void addCar_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }

        private void addCar_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void addCar_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
