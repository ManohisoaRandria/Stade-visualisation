using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espace.classes {
    class Reservation {
        private string idReservation;
        private DateTime dateReservation;
        private string idSeza;
        private string idSpectacle;
        private double prixReservation;
        private int etat;

        public Reservation() {

        }

        public Reservation(string idReservation, DateTime dateReservation, string idSeza, string idSpectacle, double prixReservation, int etat) {
            this.idReservation = idReservation;
            this.dateReservation = dateReservation;
            this.idSeza = idSeza;
            this.idSpectacle = idSpectacle;
            this.prixReservation = prixReservation;
            this.etat = etat;
        }
        public void reserver() {

        }
        public string IdReservation { get => idReservation; set => idReservation = value; }
        public DateTime DateReservation { get => dateReservation; set => dateReservation = value; }
        public string IdSeza { get => idSeza; set => idSeza = value; }
        public string IdSpectacle { get => idSpectacle; set => idSpectacle = value; }
        public double PrixReservation { get => prixReservation; set => prixReservation = value; }
        public int Etat { get => etat; set => etat = value; }
    }
}
