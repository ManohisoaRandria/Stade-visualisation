using Npgsql;
using rallye.outil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espace.classes {
    public class Seza {
        private String idSeza;
        private String idZone;
        private int rang;
        private int x;
        private int y;
        private int lng;
        private int larg;
        private int etat;

        public Seza() {

        }

        public Seza(string idSeza, string idZone, int rang, int x, int y, int lng, int larg, int etat) {
            this.idSeza = idSeza;
            this.idZone = idZone;
            this.rang = rang;
            this.x = x;
            this.y = y;
            this.lng = lng;
            this.larg = larg;
            this.etat = etat;
        }
        public void insert(NpgsqlConnection con) {
            try {
                new Util<int>().insertObject(this, "seza", con);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public void insertTran(NpgsqlTransaction tran) {
            try {
                new Util<int>().insertObjectTran(this, "seza", tran);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public string IdSeza { get => idSeza; set => idSeza = value; }
        public string IdZone { get => idZone; set => idZone = value; }
        public int Rang { get => rang; set => rang = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Lng { get => lng; set => lng = value; }
        public int Larg { get => larg; set => larg = value; }
        public int Etat { get => etat; set => etat = value; }
    }
}
