using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drawer_Pack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
        static void multiply(int[,] mat1,int[,] mat2, int[,] res)
        {
            int N = 3;
            int i, j, k;
            for (i = 0; i < N; i++)
            {
                for (j = 0; j < N; j++)
                {
                    res[i, j] = 0;
                    for (k = 0; k < N; k++)
                        res[i, j] += mat1[i, k]
                                     * mat2[k, j];
                }
            }
        }
        //      Clear Button        //
        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            this.Refresh();
        }

        //      DDA Algorithm       //
        private void ddaLine(int x0, int y0, int xEnd, int yEnd)
        {
            int xInitial = x0, yInitial = y0, xFinal = xEnd, yFinal = yEnd;
            int dx = xFinal - xInitial, dy = yFinal - yInitial, steps, k, xf, yf;
            float xIncrement, yIncrement, x = xInitial, y = yInitial;

            if (Math.Abs(dx) > Math.Abs(dy)) steps = Math.Abs(dx);
            else steps = Math.Abs(dy);

            xIncrement = dx / (float)steps;
            yIncrement = dy / (float)steps;

            for (k = 0; k < steps; k++)
            {
                x += xIncrement;
                xf = (int)x;
                y += yIncrement;
                yf = (int)y;

                try
                {
                    ddaPlotPoints(x, y);

                }
                catch (InvalidOperationException)
                {
                    return;
                }
            }
        }
        private void ddaPlotPoints(float x, float y)
        {
            var aBrush = Brushes.Red;
            var g = panel1.CreateGraphics();
           
            g.FillRectangle(aBrush, x, y, 2, 2);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int x1 = 250 + Convert.ToInt32(textBox1.Text);
            int y1 = 250 - Convert.ToInt32(textBox2.Text);
            int x2 = 250 + Convert.ToInt32(textBox3.Text);
            int y2 = 250 - Convert.ToInt32(textBox4.Text);

            panel1.Controls.Clear();
            this.Refresh();
            ddaLine(x1, y1, x2, y2);
        }

        //      BLA Algorithm       //
        private void bresenhamLine(int x1, int y1, int x2, int y2, int dx, int dy, int decide)
        {
            int pk = 2 * dy - dx;
            for (int i = 0; i <= dx - 1; i++)
            {
                if (x1 < x2) x1++;
                else x1--;

                if (pk < 0)
                {
                    if (decide == 0)
                    {
                        BLAPlotPoints(x1, y1);
                        pk = pk + 2 * dy;
                    }
                    else
                    {
                        BLAPlotPoints(y1, x1);
                        pk = pk + 2 * dy;
                    }
                }
                else
                {
                    if (y1 < y2) y1++;
                    else y1--;
                    if (decide == 0)
                    {

                        BLAPlotPoints(x1, y1);
                    }
                    else
                    {
                        BLAPlotPoints(y1, x1);
                    }
                    pk = pk + 2 * dy - 2 * dx;
                }
            }
        }
        private void BLAPlotPoints(int x, int y)
        {

            var aBrush = Brushes.Blue;
            var g = panel1.CreateGraphics();
            g.FillRectangle(aBrush, 250 + x, 250 - y, 2, 2);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox5.Text);
            int y1 = Convert.ToInt32(textBox6.Text);
            int x2 = Convert.ToInt32(textBox7.Text);
            int y2 = Convert.ToInt32(textBox8.Text);
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            panel1.Controls.Clear();
            this.Refresh();
            if (dx > dy)
            {
                bresenhamLine(x1, y1, x2, y2, dx, dy, 0);
            }
            else
            {
                bresenhamLine(y1, x1, y2, x2, dy, dx, 1);
            }
        }
   
        //      Midpoint Circle Algorithm       //
        private void circleMidpoint(int xCenter, int yCenter, int radius)
        {
            int x = 0;
            int y = radius;
            int p = 1 - radius;
            circlePlotPoints(xCenter, yCenter, x, y);
            while (x < y)
            {
                x++;
                if (p < 0)
                    p += 2 * x + 1;
                else
                {
                    y--;
                    p += 2 * (x - y) + 1;
                }
                circlePlotPoints(xCenter, yCenter, x, y);
            }
        }
        private void circlePlotPoints(int xCenter, int yCenter, int x, int y)
        {
            var aBrush = Brushes.ForestGreen;
            var g = panel1.CreateGraphics();
   
            g.FillRectangle(aBrush, xCenter + x, yCenter + y, 2, 2);
            g.FillRectangle(aBrush, xCenter - x, yCenter + y, 2, 2);
            g.FillRectangle(aBrush, xCenter + x, yCenter - y, 2, 2);
            g.FillRectangle(aBrush, xCenter - x, yCenter - y, 2, 2);
            g.FillRectangle(aBrush, xCenter + y, yCenter + x, 2, 2);
            g.FillRectangle(aBrush, xCenter - y, yCenter + x, 2, 2);
            g.FillRectangle(aBrush, xCenter + y, yCenter - x, 2, 2);
            g.FillRectangle(aBrush, xCenter - y, yCenter - x, 2, 2);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int x = 250 + Convert.ToInt32(textBox9.Text);
            int y = 250 - Convert.ToInt32(textBox10.Text);
            int r = Convert.ToInt32(textBox11.Text);
            panel1.Controls.Clear();
            this.Refresh();

            circleMidpoint(x, y, r);
        }

        //      Ellipse Circle Algorithm        //
        private void ellipseCircle(int xCenter, int yCenter, int Rx, int Ry)
        {
            int Rx2 = Rx * Rx;
            int Ry2 = Ry * Ry;
            int twoRx2 = 2 * Rx2;
            int twoRy2 = 2 * Ry2;
            int p;
            int x = 0;
            int y = Ry;
            int px = 0;
            int py = twoRx2 * y;

            ellipsePlotPoints(xCenter, yCenter, x, y);

            p = round(Ry2 - (Rx2 * Ry) + (0.25 * Rx2));
            while (px < py)
            {
                x++;
                px += twoRy2;
                if (p < 0)
                    p = p + Ry2 + px;
                else
                {
                    y--;
                    py = py - twoRx2;
                    p = p + Ry2 + px - py;
                }

                ellipsePlotPoints(xCenter, yCenter, x, y);

            }
            p = round(Ry2 * (x + 0.5) * (x + 0.5) + Rx2 * (y - 1) * (y - 1) - Rx2 * Ry2);
            while (y > 0)
            {
                y--;
                py = py - twoRx2;
                if (p > 0)
                    p = p + Rx2 - py;
                else
                {
                    x++;
                    px = px + twoRy2;
                    p = p + Rx2 - py + px;
                }
                ellipsePlotPoints(xCenter, yCenter, x, y);
            }
        }
        private void ellipsePlotPoints(int xCenter, int yCenter, int x, int y)
        {
            var aBrush = Brushes.Green;
            var g = panel1.CreateGraphics();
          
            g.FillRectangle(aBrush, xCenter + x, yCenter + y, 2, 2);
            g.FillRectangle(aBrush, xCenter - x, yCenter + y, 2, 2);
            g.FillRectangle(aBrush, xCenter + x, yCenter - y, 2, 2);
            g.FillRectangle(aBrush, xCenter - x, yCenter - y, 2, 2);
        }
        private int round(double a)
        {
            return Convert.ToInt32(a + 0.5);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int xc = 250 + Convert.ToInt32(textBox12.Text);
            int yc = 250 - Convert.ToInt32(textBox13.Text);
            int xr = Convert.ToInt32(textBox14.Text);
            int yr = Convert.ToInt32(textBox15.Text);
            panel1.Controls.Clear();
            this.Refresh();

            ellipseCircle(xc, yc, xr, yr);
        }

        //      Draw Rectangle      //
        private void rectangleDraw (Point p1,Point p2, Point p3, Point p4)
        {
            ddaLine(p1.X, p1.Y, p2.X, p2.Y);
            ddaLine(p2.X, p2.Y, p3.X, p3.Y);
            ddaLine(p3.X, p3.Y, p4.X, p4.Y);
            ddaLine(p4.X, p4.Y, p1.X, p1.Y);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            int x1 = 250 + Convert.ToInt32(textBox20.Text);
            int y1 = 250 - Convert.ToInt32(textBox21.Text);
            int x2 = 250 + Convert.ToInt32(textBox25.Text);
            int y2 = 250 - Convert.ToInt32(textBox24.Text);
            int x3 = 250 + Convert.ToInt32(textBox28.Text);
            int y3 = 250 - Convert.ToInt32(textBox27.Text);
            int x4 = 250 + Convert.ToInt32(textBox31.Text);
            int y4 = 250 - Convert.ToInt32(textBox30.Text);
            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            Point p3 = new Point(x3, y3);
            Point p4 = new Point(x4, y4);
            panel1.Controls.Clear();
            this.Refresh();
            if (x1 == x4 && x2 == x3 && y1 == y2 && y3 == y4 && x2 - x1 > y2 - y3)
            { rectangleDraw(p1,p2,p3,p4); }
            else
            { MessageBox.Show ("Invalid input"); }

        }

        //****************** ( 2D-Applications) **********************//
        //  2D Reflection     //

        // Reflect Over X //
        private void button9_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = Convert.ToInt32(textBox21.Text);
            int x2 = Convert.ToInt32(textBox25.Text);
            int y2 = Convert.ToInt32(textBox24.Text);
            int x3 = Convert.ToInt32(textBox28.Text);
            int y3 = Convert.ToInt32(textBox27.Text);
            int x4 = Convert.ToInt32(textBox31.Text);
            int y4 = Convert.ToInt32(textBox30.Text);
            int y1dash = 0;
            int y2dash = 0;
            int y3dash = 0;
            int y4dash = 0;


            int[,] currentMat1 = { {1,0,x1 },
                                   {0,1,y1 },
                                   {0,0,1} };


            int[,] currentMat2 = { {1,0,x2 },
                                   {0,1,y2 },
                                   {0,0,1} };


            int[,] currentMat3 = { {1,0,x3 },
                                   {0,1,y3 },
                                   {0,0,1} };

            int[,] currentMat4 = { {1,0,x4 },
                                   {0,1,y4 },
                                   {0,0,1} };


            int[,] newtMat1 = { {1,0,x1 },
                                {0,1,y1dash },
                                {0,0,1} };


            int[,] newtMat2 = { {1,0,x2 },
                                {0,1,y2dash },
                                {0,0,1} };


            int[,] newtMat3 = { {1,0,x3 },
                                {0,1,y3dash },
                                {0,0,1} };

            int[,] newtMat4 = { {1,0,x4},
                                {0,1,y4dash },
                                {0,0,1} };


            int[,] reflectMat = { {1,0,0 },
                                  {0,-1,0 },
                                  {0,0,1 } };

            multiply(reflectMat, currentMat1, newtMat1);
            multiply(reflectMat, currentMat2, newtMat2);
            multiply(reflectMat, currentMat3, newtMat3);
            multiply(reflectMat, currentMat4, newtMat4);




            // panel1.Refresh();
            Graphics draw = panel1.CreateGraphics();
            var aBrush = Brushes.BlueViolet;
            Pen BlackBrush = new Pen(aBrush, 2);
            draw.DrawLine(BlackBrush, x1 + 250, 250 - newtMat1[1, 2], x2 + 250, 250 - newtMat2[1, 2]);
            draw.DrawLine(BlackBrush, x2 + 250, 250 - newtMat2[1, 2], x3 + 250, 250 - newtMat3[1, 2]);
            draw.DrawLine(BlackBrush, x3 + 250, 250 - newtMat3[1, 2], x4 + 250, 250 - newtMat4[1, 2]);
            draw.DrawLine(BlackBrush, x1 + 250, 250 - newtMat1[1, 2], x4 + 250, 250 - newtMat4[1, 2]);
        }
        // Reflect Over Y //
        private void button10_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = Convert.ToInt32(textBox21.Text);
            int x2 = Convert.ToInt32(textBox25.Text);
            int y2 = Convert.ToInt32(textBox24.Text);
            int x3 = Convert.ToInt32(textBox28.Text);
            int y3 = Convert.ToInt32(textBox27.Text);
            int x4 = Convert.ToInt32(textBox31.Text);
            int y4 = Convert.ToInt32(textBox30.Text);
            int x1dash = 0;
            int x2dash = 0;
            int x3dash = 0;
            int x4dash = 0;

            int[,] currentMat1 = { {1,0,x1 },
                                   {0,1,y1 },
                                   {0,0,1} };

            int[,] currentMat2 = { {1,0,x2 },
                                   {0,1,y2 },
                                   {0,0,1} };

            int[,] currentMat3 = { {1,0,x3 },
                                   {0,1,y3 },
                                   {0,0,1} };

            int[,] currentMat4 = { {1,0,x4 },
                                   {0,1,y4 },
                                   {0,0,1} };

            int[,] newtMat1 = { {1,0,x1dash },
                                {0,1,y1 },
                                {0,0,1} };

            int[,] newtMat2 = { {1,0,x2dash },
                                {0,1,y2 },
                                {0,0,1} };

            int[,] newtMat3 = { {1,0,x3dash },
                                {0,1,y3 },
                                {0,0,1} };

            int[,] newtMat4 = { {1,0,x4dash },
                                {0,1,y4 },
                                {0,0,1} };

            int[,] reflectMat = { {-1,0,0 },
                                  {0,1,0 },
                                  {0,0,1 } };

            multiply(reflectMat, currentMat1, newtMat1);
            multiply(reflectMat, currentMat2, newtMat2);
            multiply(reflectMat, currentMat3, newtMat3);
            multiply(reflectMat, currentMat4, newtMat4);

            Graphics draw = panel1.CreateGraphics();
            var aBrush = Brushes.Blue;
            Pen BlackBrush = new Pen(aBrush, 2);
            draw.DrawLine(BlackBrush, newtMat1[0, 2] + 250, 250 - y1, newtMat2[0, 2] + 250, 250 - y2);
            draw.DrawLine(BlackBrush, newtMat2[0, 2] + 250, 250 - y2, newtMat3[0, 2] + 250, 250 - y3);
            draw.DrawLine(BlackBrush, newtMat3[0, 2] + 250, 250 - y3, newtMat4[0, 2] + 250, 250 - y4);
            draw.DrawLine(BlackBrush, newtMat1[0, 2] + 250, 250 - y1, newtMat4[0, 2] + 250, 250 - y4);
        }
        // Reflect Over Origin //
        private void button13_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox20.Text);
            int y1 = Convert.ToInt32(textBox21.Text);
            int x2 = Convert.ToInt32(textBox25.Text);
            int y2 = Convert.ToInt32(textBox24.Text);
            int x3 = Convert.ToInt32(textBox28.Text);
            int y3 = Convert.ToInt32(textBox27.Text);
            int x4 = Convert.ToInt32(textBox31.Text);
            int y4 = Convert.ToInt32(textBox30.Text);
            int x1dash = 0;
            int y1dash = 0;
            int x2dash = 0;
            int y2dash = 0;
            int x3dash = 0;
            int y3dash = 0;
            int x4dash = 0;
            int y4dash = 0;


            int[,] currentMat1 = { {1,0,x1 },
                                   {0,1,y1 },
                                   {0,0,1} };


            int[,] currentMat2 = { {1,0,x2 },
                                   {0,1,y2 },
                                   {0,0,1} };


            int[,] currentMat3 = { {1,0,x3 },
                                   {0,1,y3 },
                                   {0,0,1} };

            int[,] currentMat4 = { {1,0,x4 },
                                   {0,1,y4 },
                                   {0,0,1} };


            int[,] newtMat1 = { {1,0,x1dash },
                                {0,1,y1dash },
                                {0,0,1} };


            int[,] newtMat2 = { {1,0,x2dash },
                                {0,1,y2dash },
                                {0,0,1} };


            int[,] newtMat3 = { {1,0,x3dash },
                                {0,1,y3dash },
                                {0,0,1} };

            int[,] newtMat4 = { {1,0,x4dash },
                                {0,1,y4dash },
                                {0,0,1} };


            int[,] reflectMat = { {-1,0,0 },
                                  {0,-1,0 },
                                  {0,0,1 } };

            multiply(reflectMat, currentMat1, newtMat1);
            multiply(reflectMat, currentMat2, newtMat2);
            multiply(reflectMat, currentMat3, newtMat3);
            multiply(reflectMat, currentMat4, newtMat4);

            Graphics draw = panel1.CreateGraphics();
            var aBrush = Brushes.CadetBlue;
            Pen BlackBrush = new Pen(aBrush, 2);
            draw.DrawLine(BlackBrush, newtMat1[0, 2] + 250, 250 - newtMat1[1, 2], newtMat2[0, 2] + 250, 250 - newtMat2[1, 2]);
            draw.DrawLine(BlackBrush, newtMat2[0, 2] + 250, 250 - newtMat2[1, 2], newtMat3[0, 2] + 250, 250 - newtMat3[1, 2]);
            draw.DrawLine(BlackBrush, newtMat3[0, 2] + 250, 250 - newtMat3[1, 2], newtMat4[0, 2] + 250, 250 - newtMat4[1, 2]);
            draw.DrawLine(BlackBrush, newtMat1[0, 2] + 250, 250 - newtMat1[1, 2], newtMat4[0, 2] + 250, 250 - newtMat4[1, 2]);
        }

        //  2D Rotation     //
        private void rotation2d(Point p1, Point p2, Point p3, Point p4, double angle)
        {
            Graphics draw = panel1.CreateGraphics();
            var aBrush = Brushes.Green;
            Pen BlackBrush = new Pen(aBrush, 2);

            angle = (-(angle * 3.14 / 180));
            double xt1, yt1, xt2, yt2, xt3, yt3,
                dx1 = p1.X - p2.X,
                dy1 = p2.Y - p1.Y,
                dx2 = p1.X - p3.X,
                dy2 = p3.Y - p1.Y,
                dx3 = p1.X - p4.X,
                dy3 = p4.Y - p1.Y;
            xt1 = p1.X + (dx1 * Math.Cos(angle) - dy1 * Math.Sin(angle));
            yt1 = p1.X + (dx1 * Math.Sin(angle) + dy1 * Math.Cos(angle));
            p2.X = (int)xt1;
            p2.Y = (int)yt1;
            draw.DrawLine(BlackBrush, p1.X, p1.Y, p2.X, p2.Y);
            dy2 = p3.Y - p1.Y;
            xt2 = p1.X + (dx2 * Math.Cos(angle) - dy2 * Math.Sin(angle));
            yt2 = p1.X + (dx2 * Math.Sin(angle) + dy2 * Math.Cos(angle));
            p3.X = (int)xt2;
            p3.Y = (int)yt2;
            draw.DrawLine(BlackBrush, p1.X, p1.Y, p3.X, p3.Y);
            xt3 = p1.X + (dx3 * Math.Cos(angle) - dy3 * Math.Sin(angle));
            yt3 = p1.X + (dx3 * Math.Sin(angle) + dy3 * Math.Cos(angle));
            p4.X = (int)xt3;
            p4.Y = (int)yt3;            
            draw.DrawLine(BlackBrush, p2.X, p2.Y, p4.X, p4.Y);
            draw.DrawLine(BlackBrush, p4.X, p4.Y, p3.X, p3.Y);
        }
        private void button14_Click(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(textBox33.Text);
            int x1 = 250 - Convert.ToInt32(textBox20.Text);
            int y1 = 250 - Convert.ToInt32(textBox21.Text);
            int x2 = 250 - Convert.ToInt32(textBox25.Text);
            int y2 = 250 - Convert.ToInt32(textBox24.Text);
            int x3 = 250 - Convert.ToInt32(textBox28.Text);
            int y3 = 250 - Convert.ToInt32(textBox27.Text);
            int x4 = 250 - Convert.ToInt32(textBox31.Text);
            int y4 = 250 - Convert.ToInt32(textBox30.Text);
            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            Point p3 = new Point(x3, y3);
            Point p4 = new Point(x4, y4);
            rotation2d(p4, p3, p1, p2, a);
        }
        //  2D Translation     //

        private void button12_Click(object sender, EventArgs e)
        {
            int transX = 0, transY = 0;
            transX = Convert.ToInt32(textBox34.Text);
            transY = Convert.ToInt32(textBox35.Text);
            int x1 = transX + 250 + Convert.ToInt32(textBox20.Text);
            int y1 = 250 - transY -  Convert.ToInt32(textBox21.Text);
            int x2 = transX + 250 + Convert.ToInt32(textBox25.Text);
            int y2 = 250 - transY - Convert.ToInt32(textBox24.Text);
            int x3 = transX + 250 + Convert.ToInt32(textBox28.Text);
            int y3 = 250 - transY - Convert.ToInt32(textBox27.Text);
            int x4 = transX + 250 + Convert.ToInt32(textBox31.Text);
            int y4 = 250 - transY - Convert.ToInt32(textBox30.Text);


            Point p1t = new Point(x1, y1);
            Point p2t = new Point(x2, y2);
            Point p3t = new Point(x3, y3);
            Point p4t = new Point(x4, y4);

            Graphics draw = panel1.CreateGraphics();
            Brush brush = new SolidBrush(Color.YellowGreen);
            Pen YgBrush = new Pen(brush, 2);
            draw.DrawLine(YgBrush, p1t, p2t);
            draw.DrawLine(YgBrush, p2t, p3t);
            draw.DrawLine(YgBrush, p3t, p4t);
            draw.DrawLine(YgBrush, p4t, p1t);
        }
        //  2D Scaling     //
        private void button11_Click(object sender, EventArgs e)
        {
            int xScal = Convert.ToInt32(textBox34.Text);
            int yScal = Convert.ToInt32(textBox35.Text);
            if (xScal == 0 )  xScal = 1;
            if (yScal == 0 )  yScal = 1;

            int x1 = 250 + Convert.ToInt32(textBox20.Text);
            int y1 = 250 - Convert.ToInt32(textBox21.Text);
            int x2 = 250 + Convert.ToInt32(textBox25.Text);
            int y2 = 250 - Convert.ToInt32(textBox24.Text);
            int x3 = 250 + Convert.ToInt32(textBox28.Text);
            int y3 = 250 - Convert.ToInt32(textBox27.Text);
            int x4 = 250 + Convert.ToInt32(textBox31.Text);
            int y4 = 250 - Convert.ToInt32(textBox30.Text);

            Point p1s = new Point(x1, y1);
            Point p2s = new Point(x2, y2);
            Point p3s = new Point(x3, y3);
            Point p4s = new Point(x4, y4);
            if (xScal > 1)
            {
                p2s.X = 250 + xScal * Convert.ToInt32(textBox25.Text) ;
                p3s.X = 250 + xScal * Convert.ToInt32(textBox28.Text) ;
            }
            if (yScal > 1)
            {
                p1s.Y = 250 - yScal * Convert.ToInt32(textBox21.Text);
                p2s.Y = 250 - yScal * Convert.ToInt32(textBox24.Text);
            }
            Graphics draw = panel1.CreateGraphics();
            Brush brush = new SolidBrush(Color.Blue);
            Pen bluBrush = new Pen(brush, 2);
            draw.DrawLine(bluBrush, p1s, p2s);
            draw.DrawLine(bluBrush, p2s, p3s);
            draw.DrawLine(bluBrush, p3s, p4s);
            draw.DrawLine(bluBrush, p4s, p1s);

        }
        //      Shearing        //
        //      X-Shearing      //
        
        private void button7_Click(object sender, EventArgs e)
        {
            int pl = 250;
            int Shyx = Convert.ToInt32(textBox32.Text);
            int y1 = Convert.ToInt32(textBox21.Text);
            int x1 = Convert.ToInt32(textBox20.Text) + Shyx * y1;
            int y2 = Convert.ToInt32(textBox24.Text);
            int x2 = Convert.ToInt32(textBox25.Text) + Shyx * y2;
            int y3 = Convert.ToInt32(textBox27.Text);
            int x3 = Convert.ToInt32(textBox28.Text) + Shyx * y3;
            int y4 = Convert.ToInt32(textBox30.Text);
            int x4 = Convert.ToInt32(textBox31.Text) + Shyx * y4;
            Point p1 = new Point(pl + x1, pl - y1);
            Point p2 = new Point(pl + x2, pl - y2);
            Point p3 = new Point(pl + x3, pl - y3);
            Point p4 = new Point(pl + x4, pl - y4);
            Graphics draw = panel1.CreateGraphics();
            Brush brush = new SolidBrush(Color.Blue);
            Pen bluBrush = new Pen(brush, 2);
            draw.DrawLine(bluBrush, p1, p2);
            draw.DrawLine(bluBrush, p2, p3);
            draw.DrawLine(bluBrush, p3, p4);
            draw.DrawLine(bluBrush, p4, p1);
        }
        //      Y-Shearing      //
        private void button8_Click(object sender, EventArgs e)
        {
            int pl = 250;
            int ShY = Convert.ToInt32(textBox37.Text);
            int x1 = Convert.ToInt32(textBox20.Text) ;
            int y1 = Convert.ToInt32(textBox21.Text) + ShY* x1;
            int x2 = Convert.ToInt32(textBox25.Text) ;
            int y2 = Convert.ToInt32(textBox24.Text) + ShY * x2;
            int x3 = Convert.ToInt32(textBox28.Text) ;
            int y3 = Convert.ToInt32(textBox27.Text) + ShY * x3;
            int x4 = Convert.ToInt32(textBox31.Text) ;
            int y4 = Convert.ToInt32(textBox30.Text) + ShY * x4;
            
            Point p1 = new Point(pl + x1, pl - y1);
            Point p2 = new Point(pl + x2, pl - y2);
            Point p3 = new Point(pl + x3, pl - y3);
            Point p4 = new Point(pl + x4, pl - y4);
            Graphics draw = panel1.CreateGraphics();
            Brush brush = new SolidBrush(Color.Purple);
            Pen bluBrush = new Pen(brush, 2);
            draw.DrawLine(bluBrush, p1, p2);
            draw.DrawLine(bluBrush, p2, p3);
            draw.DrawLine(bluBrush, p3, p4);
            draw.DrawLine(bluBrush, p4, p1);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
