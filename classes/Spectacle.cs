using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espace.classes{
    class Spectacle{
        private String idSpectacle;
        private String nomSpectacle;
        private String idEspace;
        private DateTime dateSpectacle;

        public Spectacle(){

        }

        public Spectacle(string idSpectacle, string nomSpectacle, string idEspace, DateTime dateSpectacle){
            this.idSpectacle = idSpectacle;
            this.nomSpectacle = nomSpectacle;
            this.idEspace = idEspace;
            this.dateSpectacle = dateSpectacle;
        }

        public string IdSpectacle { get => idSpectacle; set => idSpectacle = value; }
        public string NomSpectacle { get => nomSpectacle; set => nomSpectacle = value; }
        public string IdEspace { get => idEspace; set => idEspace = value; }
        public DateTime DateSpectacle { get => dateSpectacle; set => dateSpectacle = value; }
    }
}
