using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espace.classes {
    public class NumSeza {
        private string idNumSeza;
        private string idSeza;
        private string idSpectacle;
        private int num;

        public NumSeza(string idNumSeza, string idSeza, string idSpectacle, int num) {
            this.idNumSeza = idNumSeza;
            this.idSeza = idSeza;
            this.idSpectacle = idSpectacle;
            this.num = num;
        }

        public string IdNumSeza { get => idNumSeza; set => idNumSeza = value; }
        public string IdSeza { get => idSeza; set => idSeza = value; }
        public string IdSpectacle { get => idSpectacle; set => idSpectacle = value; }
        public int Num { get => num; set => num = value; }
    }
}
