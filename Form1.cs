using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProMaxDrawer
{
    public partial class Form1 : Form
    {
        private Button btn1;
        private Button btn2;
        private Button btn3;
        private Button btn4;
        private Button btn5;
        private Button btn6;
        private Button btn7;

        private Button btnAbout;
        private Form formAbout;

        public Form1()
        {
            InitUI();
        }

        private void InitUI()
        {
            this.Text = "ProMaxDrawer - v1.0";
            this.Size = new Size(640, 480);
            this.BackColor = Color.RoyalBlue;

            btn1 = CreateFuckingButton(30);
            btn2 = CreateFuckingButton(80);
            btn3 = CreateFuckingButton(130);
            btn4 = CreateFuckingButton(180);
            btn5 = CreateFuckingButton(230);
            btn6 = CreateFuckingButton(280);
            btn7 = CreateFuckingButton(330);

            btnAbout = CreateFuckingButton2(30);

            btn1.Text = "Drawer1";
            btn2.Text = "Drawer2";
            btn3.Text = "Drawer3";
            btn4.Text = "Drawer4";
            btn5.Text = "Drawer5";
            btn6.Text = "Drawer6";
            btn7.Text = "Drawer7";

            btn1.Click += (s, e) => Drawers.Texts();
            btn2.Click += (s, e) => Drawers.BarakoEffect();
            btn3.Click += (s, e) => Drawers.DarkR();
            btn4.Click += (s, e) => Drawers.BrightR();
            btn5.Click += (s, e) => Drawers.Shake();
            btn6.Click += (s, e) => Drawers.Invert();
            btn7.Click += (s, e) => Drawers.Hatch();

            btnAbout.Text = "About";

            btnAbout.Click += (s, e) => CreateAboutForm();

            this.Controls.Add(btn1);
            this.Controls.Add(btn2);
            this.Controls.Add(btn3);
            this.Controls.Add(btn4);
            this.Controls.Add(btn5);
            this.Controls.Add(btn6);
            this.Controls.Add(btn7);

            this.Controls.Add(btnAbout);
        }

        private Button CreateFuckingButton(int y) => new Button
        {
            BackColor = Color.LightBlue,
            ForeColor = Color.White,
            Size = new Size(200, 50),
            Location = new Point(20, y),
            Font = new Font("Comic Sans MS", 12),
        };

        private Button CreateFuckingButton2(int y)
        {
            return new Button
            {
                BackColor = Color.LightCyan,
                ForeColor = Color.Black,
                Size = new Size(100, 50),
                Location = new Point(520, y),
                Font = new Font("Arial", 12)
            };
        }

        private void CreateAboutForm()
        {
            formAbout = new Form
            {
                Text = "ProMaxDrawer - About",
                Size = new Size(480, 320),
                BackColor = Color.GreenYellow,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label Aboutlbl = new Label
            {
                Text = "ProMaxDrawer\n\nVersion 1.0\n\nDeveloped by Danoni631",
                Font = new Font("Impact", 19),
                ForeColor = Color.Green,
                Location = new Point(20, 20),
                Size = new Size(320, 200)
            };

            formAbout.Controls.Add(Aboutlbl);
            formAbout.ShowDialog(this);
        }
    }
}
