using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espace.classes {
    class Pub {
        private string idPub;
        private DateTime datePub;
        private string idMedia;
        private string idSpectacle;

        public Pub() {

        }

        public Pub(string idPub, DateTime datePub, string idMedia, string idSpectacle) {
            this.idPub = idPub;
            this.datePub = datePub;
            this.idMedia = idMedia;
            this.idSpectacle = idSpectacle;
        }

        public string IdPub { get => idPub; set => idPub = value; }
        public DateTime DatePub { get => datePub; set => datePub = value; }
        public string IdMedia { get => idMedia; set => idMedia = value; }
        public string IdSpectacle { get => idSpectacle; set => idSpectacle = value; }
    }
}
