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
		private int dvX;
		private int dvY;
        private int loc_x;
        private int loc_y;
        private PictureBox fruit;
		private PictureBox[] body = new PictureBox[10000]; //размер змейки
        private Label Score;
        private int map_width = 200;
        private int map_height = 200;
		private int y;
        private int size = 20;
        public Form1()
        {
            InitializeComponent();
            body[0] = new PictureBox();
            body[0].Size = new Size(size, size);
            body[0].BackColor = Color.Red;
            body[0].Location = new Point(0, 0);
            this.Controls.Add(body[0]);
            y = 0;
			dvX = 1;
			dvY = 0;
            Score = new Label();
            Score.Location = new Point(220, 10);
            Score.Size = new Size(100, 20);
            Score.ForeColor = Color.Black;
            Score.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            Score.Text = "Score: 0";
            this.Controls.Add(Score);
            this.Controls.Add(fruit);
            Map();
            fruit = new PictureBox();
            fruit.BackColor = Color.Yellow;
			fruit.Size = new Size(size, size);
            this.Controls.Add(fruit);
            Loc_fruit();

			timer1.Tick += new EventHandler(upp);
			timer1.Interval = 200;
			timer1.Start();
        }
        private void upp(Object myObject, EventArgs e)
        {
            eat_fruit();

            for (int i = y; i >= 1; --i)
            {
                body[i].Location = body[i - 1].Location;
            }
            body[0].Location = new Point(body[0].Location.X + dvX * (size), body[0].Location.Y + dvY * (size));

			#region Появление змейки с другой стороны поля
			if (body[0].Location.X >= (map_width))
            {
                body[0].Location = new Point(0, body[0].Location.Y + dvY * (size));
            }
           if (body[0].Location.X < 0)
           {
               body[0].Location = new Point(180, body[0].Location.Y + dvY * (size));
           }
            if (body[0].Location.Y >= (map_height))
            {
                body[0].Location = new Point(body[0].Location.X + dvX * (size), 0);
            }
            if (body[0].Location.Y < 0)
            {
                body[0].Location = new Point(body[0].Location.X + dvX * (size), 180);
			}
			#endregion

			#region Если мы врезаемся в тело змейки
			for (int i = 1; i <= y; ++i)
            {
               
                if (body[0].Location == body[i].Location)
                {
                    Controls.Clear();
                    Label died = new Label();
                    died.Font = new Font("Times New Roman", 20, FontStyle.Bold);
                    died.ForeColor = Color.Black;
                    died.Size = new Size(150, 40);
                    died.Location = new Point(100, 50);
                    died.Text = "You Died!";
                    this.Controls.Add(died);

                    Label record = new Label();
                    record.Font = new Font("Times New Roman", 20, FontStyle.Bold);
                    record.ForeColor = Color.Black;
                    record.Size = new Size(200, 40);
                    record.Location = new Point(110, 100);
                    record.Text = "Score: " + y;
                    this.Controls.Add(record);

                    Button start = new Button();
                    start.Size = new Size(150,40);
                    start.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                    start.ForeColor = Color.Black;
                    start.Text = "Начать заново";
                    start.Location = new Point(90, 170);
					start.Click += new EventHandler(Start);
                    this.Controls.Add(start);


                    Button stop = new Button();
                    stop.Size = new Size(150, 40);
                    stop.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                    stop.ForeColor = Color.Black;
                    stop.Text = "Вернуться в меню";
                    stop.Location = new Point(90, 220);
                    this.Controls.Add(stop);
                }

			}
			#endregion

		}
		public void Start(object sender, EventArgs e)
		{
			Application.Restart();
		}
        static void start_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        //Если съели фрукт
   private void eat_fruit()
        {
             if(body[0].Location == fruit.Location)     //сравниваем координаты фрукта и головы змеи
            {
                ++y;                                    //если равн, то увеличиваем наши очки - y;
                Score.Text = "Score: " + y;             //выводим очки

                body[y] = new PictureBox();
                body[y].Size = new Size(size, size);
                body[y].BackColor = Color.Green;
                body[y].Location = new Point(body[y - 1].Location.X + dvX * size, body[y - 1].Location.Y + dvY * size);
                this.Controls.Add(body[y]);
               
                Loc_fruit(); //генерируем новое положение фрукта
            }
        }

        //Генерация фрукта
        private void Loc_fruit()
        {
            m_start:
            Random rnd = new Random();
            loc_x = rnd.Next(0, map_width);
            int X = loc_x % size;
            loc_x = loc_x - X;

            loc_y = rnd.Next(0, map_height);
            int Y = loc_y % size;
            loc_y = loc_y - Y;
           
            fruit.Location = new Point(loc_x, loc_y);

            #region Чтобы фрукт не генерировался в змее
            for (int i = 0; i <= y; ++i )
            {
                if (fruit.Location == body[i].Location || fruit.Location == body[0].Location)
                {
                    goto m_start;
                }
            }
            #endregion
        } 

        //Генераци карты
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
            for (int i = 1; i <= map_width / size; ++i)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(size * i, 0);
                pic.Size = new Size(1, map_height);
                this.Controls.Add(pic);
            }

        }
    
        # region Управление
		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
			{
				if(dvY == 1)
				{
				}
				else
				{
					dvY = -1;
					dvX = 0;
				}
			}
			if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
			{
				if(dvY == -1)
				{
				}
				else
				{
					dvY = 1;
					dvX = 0;	
				}
			}
			if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
			{
				if(dvX == -1)
				{
				}
				else
				{
					dvX = 1;
					dvY = 0;
				}
			}
			if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
			{
				if (dvX == 1)
				{
				}
				else
				{
					dvX = -1;
					dvY = 0;
				}
			}
		}
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }
	
	}
}
