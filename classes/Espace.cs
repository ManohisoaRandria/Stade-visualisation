using Npgsql;
using rallye.outil;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espace.classes{
    public class Espace{
        private String idEspace;
        private String nomEspace;
        private String pointsEspace;

        public Espace(){

        }
        public Espace(string idEspace, string nomEspace, string pointsEspace){
            this.IdEspace = idEspace;
            this.NomEspace = nomEspace;
            this.PointsEspace = pointsEspace;
        }
        public Espace(string idEspace, string nomEspace, Point[] pointsEspace) {
            this.IdEspace = idEspace;
            this.NomEspace = nomEspace;
            setPoints(pointsEspace);
        }
        public Point[] getPoints() {
            return new Util<int>().getPoints(this.pointsEspace);
        }
        public void setPoints(Point[]pts) {
            if (pts.Length < 3) {
                throw new Exception("aucun point pour l'espace");
            }
            this.pointsEspace=new Util<int>().setPoints(pts);
        }
        public Zone[] getZones(NpgsqlConnection con) {
            Zone[] zones=new Zone[0];
            try {
                zones=(Zone[])new Util<Zone>().multiFind(new Zone(), "zone", " idespace='"+this.idEspace+"'",con);
            }
            catch (Exception ex) {
                throw ex;
            }
            return zones;
        }
        public void insert(NpgsqlConnection con) {
            try {
                new Util<int>().insertObject(this, "espace", con);
            }
            catch(Exception ex) {
                throw ex;
            }
        }
        public override string ToString() {
            return this.nomEspace;
        }
        public string IdEspace { get => idEspace; set => idEspace = value; }
        public string NomEspace {
            get => nomEspace;
            set {
                if (value.Trim() == "") {
                    throw new Exception("aucun nom");
                }
                else {
                    nomEspace = value;
                }
            }
                   
        }
        public string PointsEspace { get => pointsEspace; set => pointsEspace = value; }
    }
}
