using banque;
using espace.classes;
using Npgsql;
using rallye.outil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace espace {
    public partial class Main : Form {
        Espace esp;
        List<Point> pts;
        List<Point> oneZone;
        List<Seza> sezaZone;
        //List<List<Point>> zones;
        RectangleF zoneBounds;
        int count;
        Boolean seza = false;

        public Main(Espace espace) {
            InitializeComponent();
            this.oneZone = new List<Point>();
            this.sezaZone = new List<Seza>();
            // this.zones = new List<List<Point>>();
            this.panel1.Enabled = false;
            this.Button1.Enabled = false;
            this.count = 0;
            this.esp = espace;
            NpgsqlConnection con = null;
            try {
                con = new DbConnect().connect();
                getZones(con);
            }
            catch (Exception ex) {
                //throw ex;
                this.textBox6.Text = ex.Message;
            }
            finally {
                if (con != null) con.Close();
            }
            // this.pts = this.esp.getPoints();
            this.panel1.Invalidate();
            this.textBox3.Text = "espace" + this.esp.IdEspace;
        }
        public void getZones(NpgsqlConnection con) {

            try {
                Zone[] zn = this.esp.getZones(con);
                for (int i = 0; i < zn.Length; i++) {
                    this.comboBox4.Items.Add(zn[i]);
                    //this.comboBox2.Items.Add(zn[i]);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        private void Button1_Click(object sender, EventArgs e) {
            this.count = 0;
            //this.zones.Add(this.pts);
            //this.pts = new List<Point>();
            //this.textBox3.Text = "" + this.zones.Count;
            this.Button1.Enabled = false;
            this.panel1.Invalidate();
            this.panel1.Enabled = false;
        }
        public void drawSeza(Graphics g) {
            try {
                GraphicsPath gp = new GraphicsPath();

                int compter = 1;
                // Create pen.
                Pen blackPen = new Pen(Color.Black, 2);
                for (int i = 0; i < this.sezaZone.Count; i++) {
                    Rectangle zoneBounds = new Rectangle(sezaZone.ElementAt(i).X, sezaZone.ElementAt(i).Y, sezaZone.ElementAt(i).Lng, sezaZone.ElementAt(i).Larg);
                
                    gp.AddRectangle(zoneBounds);
                    g.DrawPath(blackPen, gp);
                    
                    compter++;
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
            }
        }
        public void placerSeza() {
            try {
                int lng = (int)this.numericUpDown2.Value;
                int larg = (int)this.numericUpDown3.Value;
                int espacement = (int)this.numericUpDown4.Value;
                int x = (int)(this.zoneBounds.X + espacement);
                int y = (int)(this.zoneBounds.Y + espacement);
                this.sezaZone = new List<Seza>();

                int compter = 1;
                while (1 > 0) {
                    if ((x + lng + espacement) >= this.zoneBounds.Right) {
                        y += (larg + espacement);
                        x = (int)(this.zoneBounds.X + espacement);
                    }
                    if ((y + larg) >= this.zoneBounds.Bottom) {
                        break;
                    }
                    Rectangle zoneBounds = new Rectangle(x, y, lng, larg);
                    GraphicsPath gp2 = new GraphicsPath();
                    Point[] curvePoints = this.oneZone.ToArray();
                    gp2.AddPolygon(curvePoints);
                    Point[] points = getPoints(zoneBounds);
                    if (gp2.IsVisible(points[0]) && gp2.IsVisible(points[1]) && gp2.IsVisible(points[2]) && gp2.IsVisible(points[3])) {
                        this.sezaZone.Add(new Seza("nextval('seq_seza')", "", compter, zoneBounds.X, zoneBounds.Y, lng, larg, 0));
                        compter++;
                    }
                    x += (lng + espacement);
                }


            }
            catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
            }
        }
        public Point[] getPoints(Rectangle zoneBounds) {
            Point[] pt = new Point[4];
            pt[0] = new Point(zoneBounds.X, zoneBounds.Y);
            pt[1] = new Point(zoneBounds.X + zoneBounds.Width, zoneBounds.Y);
            pt[2] = new Point(zoneBounds.X, zoneBounds.Y + zoneBounds.Height);
            pt[3] = new Point(zoneBounds.X + zoneBounds.Width, zoneBounds.Y + zoneBounds.Height);
            return pt;
        }
        public void DrawEspace(Graphics g) {
            try {

                GraphicsPath gp = new GraphicsPath();
                Point[] curvePoints = this.esp.getPoints();
                gp.AddPolygon(curvePoints);
                Pen blackPen = new Pen(Color.Black, 2);
                g.DrawPath(blackPen, gp);

            }
            catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
            }
        }
        public void DrawOneZone(Graphics g) {
            try {

                GraphicsPath gp = new GraphicsPath();
                Point[] curvePoints = this.oneZone.ToArray();
                gp.AddPolygon(curvePoints);
                this.zoneBounds = gp.GetBounds();
                Pen blackPen = new Pen(Color.Black, 2);
                g.DrawPath(blackPen, gp);

            }
            catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
            }
        }
        public void DrawEfateo(Graphics g) {
            NpgsqlConnection con = null;
            try {
                con = new DbConnect().connect();
                Zone[] zonesEsp = this.esp.getZones(con);
                Seza[] placesZone = new Seza[0];
                int compter = 1;
                // Create pen.
                Pen blackPen = new Pen(Color.Red, 2);
                for (int i = 0; i < zonesEsp.Length; i++) {
                    GraphicsPath gp = new GraphicsPath();
                    Point[] curvePoints = zonesEsp[i].getPoints();
                    gp.AddPolygon(curvePoints);
                    g.DrawPath(blackPen, gp);
                    placesZone = zonesEsp[i].getSeza(con);
                    for (int j = 0; j < placesZone.Length; j++) {
                        Rectangle seza = new Rectangle(placesZone[j].X, placesZone[j].Y, placesZone[j].Lng, placesZone[j].Larg);
                        //// Create string to draw.
                        //String drawString = compter.ToString();

                        //// Create font and brush.
                        //Font drawFont = new Font("Arial", 9);
                        //SolidBrush drawBrush = new SolidBrush(Color.Black);
                        //// Set format of string.
                        //StringFormat drawFormat = new StringFormat();
                        //drawFormat.Alignment = StringAlignment.Center;

                        //// Draw string to screen.

                        ////gp.AddString(drawString, drawFont, drawBrush, zoneBounds, drawFormat);
                        gp.AddRectangle(seza);
                        g.DrawPath(blackPen, gp);
                      //  g.DrawString(drawString, drawFont, drawBrush, seza, drawFormat);
                        compter++;
                    }
                }

            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (con != null) con.Close();
            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e) {
            DrawEspace(e.Graphics);
            DrawOneZone(e.Graphics);
            DrawEfateo(e.Graphics);
            if (this.seza) {
                drawSeza(e.Graphics);
                this.seza = false;
            }
        }

        private void Panel1_MouseClick(object sender, MouseEventArgs e) {
            //test if inside espace
            GraphicsPath gp2 = new GraphicsPath();
            Point[] curvePoints = this.esp.getPoints();
            gp2.AddPolygon(curvePoints);
            if (gp2.IsVisible(e.Location)) {
                this.count++;
                if (this.count >= 3) {
                    this.Button1.Enabled = true;

                }

                this.oneZone.Add(e.Location);
                Graphics g = this.panel1.CreateGraphics();
                Pen pen = new Pen(Color.Black, 2);
                Rectangle zoneBounds = new Rectangle(e.X, e.Y, 3, 3);
                g.DrawRectangle(pen, zoneBounds);
                textBox1.Text = "" + e.X;
                textBox2.Text = "" + e.Y;
            }


        }

        private void Button2_Click(object sender, EventArgs e) {
            this.count = 0;
            this.oneZone = new List<Point>();
            //this.zones = new List<List<Point>>();
            this.Button1.Enabled = false;
            this.zoneBounds = new RectangleF();
            this.count = 0;
            this.seza = false;
            this.panel1.Invalidate();
        }

        private void Main_Load(object sender, EventArgs e) {

        }

        private void Label1_Click(object sender, EventArgs e) {

        }

        private void Button4_Click(object sender, EventArgs e) {
            this.panel1.Enabled = true;
        }

        private void Button1_Click_1(object sender, EventArgs e) {

        }

        private void Button5_Click(object sender, EventArgs e) {
            this.seza = true;
            placerSeza();
            this.panel1.Invalidate();

        }

        private void Button6_Click(object sender, EventArgs e) {

        }

        private void Button3_Click(object sender, EventArgs e) {
            NpgsqlConnection con = null;
            NpgsqlTransaction tran = null;
            try {
                
                if (this.oneZone.Count == 0) {
                    throw new Exception("zone non generer");
                }
                else if (this.sezaZone.Count == 0) {
                    throw new Exception("seza non generer");
                }
                else {
                    con = new DbConnect().connect();
                  
                    int lng = (int)this.numericUpDown2.Value;
                    int larg = (int)this.numericUpDown3.Value;
                    int espacement = (int)this.numericUpDown4.Value;
                    string nextval = new Util<int>().getNextval("seq_zone",con);

                    Zone zonInsert = new Zone(nextval, this.textBox4.Text, this.esp.IdEspace, decimal.Parse(this.textBox5.Text), lng, larg, espacement, oneZone.ToArray());
                    tran = con.BeginTransaction();
                    zonInsert.insertTran(tran);
                    for (int i = 0; i < this.sezaZone.Count; i++) {
                        this.sezaZone.ElementAt(i).IdZone = nextval;
                        this.sezaZone.ElementAt(i).insertTran(tran);
                    }
                    tran.Commit();
                    Button2_Click(sender,e);
                    this.comboBox4.Items.Clear();
                    getZones(con);
                    this.textBox4.Text = "";
                    this.textBox5.Text = "";
                }
                 
            }
            catch (Exception ex) {
                if(tran!=null)tran.Rollback();
                this.textBox6.Text = ex.Message;
            }
            finally {
                if (con != null) con.Close();
            }
        }
    }
}
