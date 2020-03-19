using banque;
using espace.classes;
using Npgsql;
using rallye.outil;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace espace.view
{
    public partial class Accueil : Form
    {
        List<Point> pts;
        List<List<Point>> ptsbdb;
        RectangleF rect;
        int count;
        Boolean seza = false;
        public Accueil()
        {
            InitializeComponent();
            NpgsqlConnection con = null;
            try {
                con = new DbConnect().connect();
                getEspaces(con);
            }
            catch (Exception ex) {
                //throw ex;
               this.erreurEspace.Text = ex.Message;
            }
            finally {
                if (con != null) con.Close();
            }
            
            this.pts = new List<Point>();
            this.ptsbdb = new List<List<Point>>();
            this.panel1.Enabled = false;
            this.button1.Enabled = false;
        }
        public void getEspaces(NpgsqlConnection con) {
            
            try {
                Espace[] esp = new Util<Espace>().multiFind(new Espace(), "espace", null, con);
                for(int i = 0; i < esp.Length; i++) {
                    this.comboBox1.Items.Add(esp[i]);
                    this.comboBox2.Items.Add(esp[i]);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
      
        

        /*private void button7_Click(object sender, EventArgs e) {
            this.count = 0;
            this.ptsbdb.Add(this.pts);
            //this.pts = new List<Point>();
            this.textBox3.Text = "" + this.ptsbdb.Count;
            this.button7.Enabled = false;
            this.panel1.Invalidate();
            this.panel1.Enabled = false;
        }*/
        public void drawSeza(Graphics g) {
            try {
                int lng = 30;
                int larg = 30;
                int espacement = 5;
                int x = (int)(this.rect.X + espacement);
                int y = (int)(this.rect.Y + espacement + 5);

                //g.DrawRectangle(pen, rect);
                GraphicsPath gp = new GraphicsPath();

                // Create pen.
                Pen blackPen = new Pen(Color.Black, 2);
                // Pen blackPen2 = new Pen(Color.Black, 2);

                //this.pts = new List<Point>();
                int compter = 1;
                while (1 > 0) {
                    if ((x + lng + espacement) >= this.rect.Right) {
                        y += (larg + espacement + 5);
                        x = (int)(this.rect.X + espacement);
                    }
                    if ((y + larg) >= this.rect.Bottom) {
                        break;
                    }
                    Rectangle rect = new Rectangle(x, y, lng, larg);
                    GraphicsPath gp2 = new GraphicsPath();
                    Point[] curvePoints = this.pts.ToArray();
                    gp2.AddPolygon(curvePoints);
                    Point[] points = getPoints(rect);
                    if (gp2.IsVisible(points[0]) && gp2.IsVisible(points[1]) && gp2.IsVisible(points[2]) && gp2.IsVisible(points[3])) {
                        // Create string to draw.
                        String drawString = compter.ToString();

                        // Create font and brush.
                        Font drawFont = new Font("Arial", 9);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        // Set format of string.
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;

                        // Draw string to screen.

                        //gp.AddString(drawString, drawFont, drawBrush, rect, drawFormat);
                        gp.AddRectangle(rect);
                        g.DrawPath(blackPen, gp);
                        g.DrawString(drawString, drawFont, drawBrush, rect, drawFormat);
                        compter++;
                    }
                    x += (lng + espacement);
                }


            }
            catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
            }
        }
        public Point[] getPoints(Rectangle rect) {
            Point[] pt = new Point[4];
            pt[0] = new Point(rect.X, rect.Y);
            pt[1] = new Point(rect.X + rect.Width, rect.Y);
            pt[2] = new Point(rect.X, rect.Y + rect.Height);
            pt[3] = new Point(rect.X + rect.Width, rect.Y + rect.Height);
            return pt;
        }
        public void DrawPolygonPoint(Graphics g) {
            try {
                if (this.pts.Count >= 3) {
                    GraphicsPath gp = new GraphicsPath();
                    Point[] curvePoints = this.pts.ToArray();
                    gp.AddPolygon(curvePoints);
                    this.rect = gp.GetBounds();
                    // gp.AddRectangle(rect);
                    // Create pen.
                    Pen blackPen = new Pen(Color.Black, 2);
                    // Pen blackPen2 = new Pen(Color.Black, 2);
                    g.DrawPath(blackPen, gp);
                    //this.pts = new List<Point>();
                }

            }
            catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
            }
        }
        public void DrawEfateo(Graphics g) {
            try {
                for (int i = 0; i < this.ptsbdb.Count; i++) {
                    GraphicsPath gp = new GraphicsPath();
                    Point[] curvePoints = this.ptsbdb.ElementAt(i).ToArray();
                    gp.AddPolygon(curvePoints);
                    RectangleF rect = gp.GetBounds();
                    gp.AddRectangle(rect);
                    // Create pen.
                    Pen blackPen = new Pen(Color.Red, 2);
                    // Pen blackPen2 = new Pen(Color.Black, 2);
                    g.DrawPath(blackPen, gp);
                    //this.pts = new List<Point>();
                }

            }
            catch (Exception ex) {
                throw ex;
            }
        }

         /*private void Panel1_MouseClick(object sender, MouseEventArgs e) {
             this.count++;
             if (this.count >= 3) {
                 this.button1.Enabled = true;

             }
             this.pts.Add(e.Location);
             Graphics g = this.panel1.CreateGraphics();
             Pen pen = new Pen(Color.Black, 2);
             Rectangle rect = new Rectangle(e.X, e.Y, 3, 3);
             g.DrawRectangle(pen, rect);
            
         }*/

        /* private void Button2_Click(object sender, EventArgs e) {
             this.count = 0;
             this.pts = new List<Point>();
             this.ptsbdb = new List<List<Point>>();
             this.button7.Enabled = false;
             this.rect = new RectangleF();
             this.count = 0;
             this.seza = false;
             this.panel1.Invalidate();
         }*/

        private void Main_Load(object sender, EventArgs e) {

        }

        private void Label1_Click(object sender, EventArgs e) {

        }

        private void button4_Click(object sender, EventArgs e) {
            this.panel1.Enabled = true;
        }

        private void Button1_Click_1(object sender, EventArgs e) {

        }

       
        private void Accueil_Load(object sender, EventArgs e) {
            chart1.Series["Test"].Points.AddXY("haha", 10);
            chart1.Series["Test"].Points.AddXY("hahefa", 59);
            chart1.Series["Test"].Points.AddXY("hazaezha", 56);
            chart1.Series["Test"].Points.AddXY("hahay", 100);
        }


        private void Button8_Click(object sender, EventArgs e) {

        }

        private void Button5_Click_1(object sender, EventArgs e) {

        }

        private void Button3_Click(object sender, EventArgs e) {
            if (this.comboBox1.SelectedItem != null) {
                Main mn = new Main((Espace)this.comboBox1.SelectedItem);
                mn.Show();
            }
            else {
                this.erreurEspace.Text="no espace selected";
            }
           
        }

        private void Label4_Click(object sender, EventArgs e) {

        }

        private void Button6_Click(object sender, EventArgs e) {

        }

        private void Button9_Click(object sender, EventArgs e) {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e) {
            DrawPolygonPoint(e.Graphics);
        }

        private void Button13_Click(object sender, EventArgs e) {
            this.count = 0;
            this.pts = new List<Point>();
            this.ptsbdb = new List<List<Point>>();
            this.button1.Enabled = false;
            this.rect = new RectangleF();
            this.count = 0;
            this.seza = false;
            this.panel1.Invalidate();
            this.panel1.Enabled = true;
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.panel1.Enabled = false;
            this.panel1.Invalidate();
        }

        private void Panel1_MouseClick_1(object sender, MouseEventArgs e) {
            try {
                this.count++;
                if (this.count >= 3) {
                    this.button1.Enabled = true;

                }
                this.pts.Add(e.Location);
                Graphics g = this.panel1.CreateGraphics();
                Pen pen = new Pen(Color.Black, 2);
                Rectangle rect = new Rectangle(e.X, e.Y, 3, 3);
                g.DrawRectangle(pen, rect);
            }
            catch (Exception ex) {

                
            }
           
        }

        private void Button14_Click(object sender, EventArgs e) {
            Main_Load(sender,e);
        }

        private void Button15_Click(object sender, EventArgs e) {
            this.count = 0;
            this.pts = new List<Point>();
            this.ptsbdb = new List<List<Point>>();
            this.button1.Enabled = false;
            this.rect = new RectangleF();
            this.count = 0;
            this.seza = false;
            this.panel1.Invalidate();
        }

        private void Button2_Click(object sender, EventArgs e) {
            NpgsqlConnection con = null;
 
            try {
                con = new DbConnect().connect();
                Espace esp = new Espace("nextval('seq_espace')",this.textBox1.Text, pts.ToArray());
                esp.insert(con);
                this.comboBox1.Items.Clear();
                getEspaces(con);
                this.textBox1.Text = "";
                Button15_Click(sender, e);
            }
            catch (Exception ex) {
                this.erreurEspace.Text = ex.Message;

            }
            finally {
                if (con != null) con.Close();
            }
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e) {
            this.textBox2.Text = this.dateTimePicker1.Value.ToString();
        }

        private void Panel3_Paint(object sender, PaintEventArgs e) {

        }
    }
}
