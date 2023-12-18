using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	/*
	Name: Merushka Munsamy
	Assessment: 2
	*/

	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		int numberOfDisks, x = 0, y = 0, a, b, c;
		Button button1;
		PictureBox pictureBox1;
		Timer timer1;
		TextBox textBox1;
		Graphics graphics;
		Bitmap bitmap;
		Pen pen;
		int[,] arrayPosition;
		char[,] arrayMovements;
		SolidBrush brush;

		private void button1_Click(object sender, EventArgs e)
		{

			timer1.Enabled = true;
			numberOfDisks = int.Parse(textBox1.Text);
			arrayPosition = new int[3, numberOfDisks];
			arrayMovements = new char[2, 3000]; //account for movements of up to 10 disks
			for (int i = 0; i < numberOfDisks; i++)
				arrayPosition[0, i] = numberOfDisks - i;
			TowersOfHanoi(numberOfDisks, 'A', 'B', 'C'); //recursive function
			Disks();
			timer1.Enabled = true;
		}

		private void TowersOfHanoi(int diskNumber, char sourceTower, char temporaryTower, char destinationTower) //recursive function for towers and where the disks must move
		{
			if (diskNumber == 1)
			{
				arrayMovements[0, x] = sourceTower;
				arrayMovements[1, x] = destinationTower;
				x++;
			}
			else
			{
				TowersOfHanoi(diskNumber - 1, sourceTower, destinationTower, temporaryTower);
				TowersOfHanoi(1, sourceTower, temporaryTower, destinationTower);
				TowersOfHanoi(diskNumber - 1, temporaryTower, sourceTower, destinationTower);
			}
		}

		private void Picture()
		{
			pictureBox1.Image = bitmap;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			button1 = new Button();
			button1.Parent = this;
			button1.Location = new Point(750, 100);
			button1.Click += new EventHandler(button1_Click);
			button1.Text = "Play";

			textBox1 = new TextBox();
			textBox1.Parent = this;
			textBox1.Location = new Point(750, 50);
			textBox1.Text = "4";

			pictureBox1 = new PictureBox();
			pictureBox1.Parent = this;
			pictureBox1.Location = new Point(10, 50);
			pictureBox1.Size = new Size(700, 400);
			pictureBox1.BackColor = Color.White;

			timer1 = new Timer();
			timer1.Interval = 300; //time in between movements
			timer1.Tick += new EventHandler(timer1_Tick);

			bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

			graphics = Graphics.FromImage(bitmap);

			pen = new Pen(Color.Black, 3);
		}

		private void Disks() //create disks as needed
		{
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < numberOfDisks; j++)
				{
					if (arrayPosition[i, j] != 0)
					{
						switch (arrayPosition[i, j])
						{
							case 1:
								brush = new SolidBrush(Color.Blue); //disk colours
								break;
							case 2:
								brush = new SolidBrush(Color.Green);
								break;
							case 3:
								brush = new SolidBrush(Color.Pink);
								break;
							case 4:
								brush = new SolidBrush(Color.Yellow);
								break;
							case 5:
								brush = new SolidBrush(Color.Red);
								break;
							case 6:
								brush = new SolidBrush(Color.Purple);
								break;
							case 7:
								brush = new SolidBrush(Color.Orange);
								break;
							case 8:
								brush = new SolidBrush(Color.SlateGray);
								break;
							case 9:
								brush = new SolidBrush(Color.Silver);
								break;
							case 10:
								brush = new SolidBrush(Color.Gold);
								break;
						}
						graphics.FillRectangle(brush, (i + 1) * 190 - 60 - (arrayPosition[i, j]) * 10, 320 - j * 30, arrayPosition[i, j] * 20, 30); //create bigger rectangles as needed
					}
				}
		}

		void timer1_Tick(object sender, EventArgs e) //animate movements
		{
			graphics.Clear(Color.White); //makes disks colours follow what was set in Disks()

			a = -1; //keep in bounds
			b = -1;
			c = -1;

			graphics.DrawLine(pen, 60, 350, 200, 350); //heights and widths of towers
			graphics.DrawLine(pen, 250, 350, 390, 350);
			graphics.DrawLine(pen, 440, 350, 580, 350);
			graphics.DrawLine(pen, 130, 350, 130, 100);
			graphics.DrawLine(pen, 320, 350, 320, 100);
			graphics.DrawLine(pen, 510, 350, 510, 100);

			for (int i = 0; i < numberOfDisks; i++)
				if (arrayPosition[0, i] == 0)
				{
					a = i;
					break;
				}

			for (int i = 0; i < numberOfDisks; i++)
				if (arrayPosition[1, i] == 0)
				{
					b = i;
					break;
				}

			for (int i = 0; i < numberOfDisks; i++)
				if (arrayPosition[2, i] == 0)
				{
					c = i;
					break;
				}

			if (a == -1)
			{
				a = numberOfDisks; //number entered
			}
			if (b == -1)
			{
				b = numberOfDisks;
			}
			if (c == -1)
			{
				c = numberOfDisks;
			}

			switch (arrayMovements[0, y]) //move to positions on towers
			{
				case 'A':
					switch (arrayMovements[1, y])
					{
						case 'B':
							arrayPosition[1, b] = arrayPosition[0, a - 1];
							arrayPosition[0, a - 1] = 0;
							break;
						case 'C':
							arrayPosition[2, c] = arrayPosition[0, a - 1];
							arrayPosition[0, a - 1] = 0;
							break;
					}
					break;

				case 'B':
					switch (arrayMovements[1, y])
					{
						case 'A':
							arrayPosition[0, a] = arrayPosition[1, b - 1];
							arrayPosition[1, b - 1] = 0;
							break;
						case 'C':
							arrayPosition[2, c] = arrayPosition[1, b - 1];
							arrayPosition[1, b - 1] = 0;
							break;
					}
					break;

				case 'C':
					switch (arrayMovements[1, y])
					{
						case 'A':
							arrayPosition[0, a] = arrayPosition[2, c - 1];
							arrayPosition[2, c - 1] = 0;
							break;
						case 'B':
							arrayPosition[1, b] = arrayPosition[2, c - 1];
							arrayPosition[2, c - 1] = 0;
							break;
					}
					break;
			}

			Disks(); //call functions
			Picture();
			y++;


		}


	}
}
