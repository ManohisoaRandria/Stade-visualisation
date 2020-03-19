using Npgsql;
using rallye.outil;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espace.classes {
    public class Zone {
        private string idZone;
        private string nomZone;
        private string idEspace;
        private decimal puZone;
        private int lngSeza;
        private int largSeza;
        private int ecartEntreSeza;
        private string pointZone;

        public Zone() {

        }

        public Zone(string idZone, string nomZone, string idEspace, decimal puZone, int lngSeza, int largSeza, int ecartEntreSeza, string pointZone) {
            this.idZone = idZone;
            this.NomZone = nomZone;
            this.idEspace = idEspace;
            this.PuZone = puZone;
            this.lngSeza = lngSeza;
            this.largSeza = largSeza;
            this.ecartEntreSeza = ecartEntreSeza;
            this.pointZone = pointZone;
        }
        public Zone(string idZone, string nomZone, string idEspace, decimal puZone, int lngSeza, int largSeza, int ecartEntreSeza, Point[] pointZone) {
            this.idZone = idZone;
            this.NomZone = nomZone;
            this.idEspace = idEspace;
            this.PuZone = puZone;
            this.lngSeza = lngSeza;
            this.largSeza = largSeza;
            this.ecartEntreSeza = ecartEntreSeza;
           setPoints(pointZone);
        }
        public Point[] getPoints() {
            return new Util<int>().getPoints(this.PointZone);
        }
        public void setPoints(Point[] pts) {
            this.PointZone = new Util<int>().setPoints(pts);
        }
        public Seza[] getSeza(NpgsqlConnection con) {
            Seza[] seza = new Seza[0];
            try {
                seza = (Seza[])new Util<Seza>().multiFind(new Seza(), "seza", " idzone='" + this.IdZone + "'", con);
            }
            catch (Exception ex) {
                throw ex;
            }
            return seza;
        }
        public void insert(NpgsqlConnection con) {
            try {
                //verifier si nom deja prise
                Zone[] zone = (Zone[])new Util<Zone>().multiFind(new Zone(), "zone", " nomZone='"+this.nomZone+"'", con);
                if (zone.Length != 0) {
                    throw new Exception("nom deja prise");
                }
                else {
                    new Util<int>().insertObject(this, "zone", con);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public void insertTran(NpgsqlTransaction tran) {
            try {
                //verifier si nom deja prise
                Zone[] zone = (Zone[])new Util<Zone>().multiFind(new Zone(), "zone", " nomZone='" + this.nomZone + "'", tran.Connection);
                if (zone.Length != 0) {
                    throw new Exception("nom deja prise");
                }
                else {
                    new Util<int>().insertObjectTran(this, "zone", tran);
                }
               
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public override string ToString() {
            return this.nomZone;
        }
        public string IdZone { get => idZone; set => idZone = value; }
        public string NomZone {
            get => nomZone; set {
                if (value=="") {
                    throw new Exception("nom vide");
                }
                else {
                    nomZone = value; 
                }
            }
         }
        public string IdEspace { get => idEspace; set => idEspace = value; }
        public decimal PuZone { get => puZone; set {
                if (value < 0) {
                    throw new Exception("pu negative");
                }
                else {
                    puZone = value;
                }
            } }
        public int LngSeza { get => lngSeza; set => lngSeza = value; }
        public int LargSeza { get => largSeza; set => largSeza = value; }
        public int EcartEntreSeza { get => ecartEntreSeza; set => ecartEntreSeza = value; }
        public string PointZone { get => pointZone; set => pointZone = value; }


    }
}
