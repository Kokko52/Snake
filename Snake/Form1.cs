using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private int loc_x;
        private int loc_y;
        private PictureBox fruit;
        private int map_width = 200;
        private int map_height = 200;
        private int size = 20;
        public Form1()
        {
            InitializeComponent();
            //PictureBox snake = new PictureBox();
            //snake.Size = new Size(20,20);
            //snake.Location = new Point(20,20);
            //snake.BackColor = Color.Green;
            //this.Controls.Add(snake);
			//MessageBox.Show(e.KeyCode.ToString());
			//MessageBox.Show(e.KeyChar.ToString());

            this.Controls.Add(fruit);
           // this.map_width = map_width;
           // this.map_height = map_height;
            Map();
            fruit = new PictureBox();
            fruit.BackColor = Color.Yellow;
            fruit.Size = new Size(20, 20);
            this.Controls.Add(fruit);
            Loc_fruit();
            //if(pictureBox1.Location == fruit.Location)
            //{
            //    MessageBox.Show("Ну здарова");
            //}
        }
        private void Loc_fruit()
        {

            Random rnd = new Random();
            loc_x = rnd.Next(0, map_width);
            int X = loc_x % size;
            loc_x = loc_x - X;

            loc_y = rnd.Next(0, map_height);
            int Y = loc_y % size;
            loc_y = loc_y - Y;
           
            fruit.Location = new Point(loc_x, loc_y);
        }
        private void Map()
        {
            for (int i = 1; i <= map_width / size; ++i)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(0, size * i);
                pic.Size = new Size(map_width, 1);
                this.Controls.Add(pic);
            }
            for(int i =1; i <= map_width / size ; ++i)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(size * i, 0);
                pic.Size = new Size(1, map_height);
                this.Controls.Add(pic);
            }

        }
     
       


        # region Управление
        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(pictureBox1.Location.X + 20, pictureBox1.Location.Y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(pictureBox1.Location.X - 20, pictureBox1.Location.Y);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - 20);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + 20);
        }
        #endregion


		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
			{
				pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - 20);
			}
			if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
			{
				pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + 20);
			}
			if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
			{
				pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - 20);
			}
			if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
			{
				pictureBox1.Location = new Point(pictureBox1.Location.X - 20, pictureBox1.Location.Y);
			}
		}


		
	}
}
