using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espace.classes {
    class Simulation {
        private string idSimulation;
        private string idZone;
        private double pourcentage;
        private string idSpectacle;
        private double prixSimulation;

        public Simulation() {

        }

        public Simulation(string idSimulation, string idZone, double pourcentage, string idSpectacle, double prixSimulation) {
            this.IdSimulation = idSimulation;
            this.IdZone = idZone;
            this.Pourcentage = pourcentage;
            this.IdSpectacle = idSpectacle;
            this.PrixSimulation = prixSimulation;
        }

        public string IdSimulation { get => idSimulation; set => idSimulation = value; }
        public string IdZone { get => idZone; set => idZone = value; }
        public double Pourcentage { get => pourcentage; set => pourcentage = value; }
        public string IdSpectacle { get => idSpectacle; set => idSpectacle = value; }
        public double PrixSimulation { get => prixSimulation; set => prixSimulation = value; }
    }
}
