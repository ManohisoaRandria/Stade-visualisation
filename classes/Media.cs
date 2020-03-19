using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espace.classes {
    class Media {
        private string idMedia;
        private string nomMedia;

        public Media() {

        }
        public Media(string idMedia, string nomMedia) {
            this.idMedia = idMedia;
            this.nomMedia = nomMedia;
        }

        public string IdMedia { get => idMedia; set => idMedia = value; }
        public string NomMedia { get => nomMedia; set => nomMedia = value; }
    }
}
