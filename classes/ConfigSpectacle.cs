using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espace.classes {
    public class ConfigSpectacle {
        private String idConfigSpectacle;
        private String idSpectacle;
        private String idZone;
	    private int debutNum;
        private String sensHorizontal;
	    private String sensVertical;
	    private String direction;

        public ConfigSpectacle(string idConfigSpectacle, string idSpectacle, string idZone, int debutNum, string sensHorizontal, string sensVertical, string direction) {
            this.idConfigSpectacle = idConfigSpectacle;
            this.idSpectacle = idSpectacle;
            this.idZone = idZone;
            this.debutNum = debutNum;
            this.sensHorizontal = sensHorizontal;
            this.sensVertical = sensVertical;
            this.direction = direction;
        }

        public string IdConfigSpectacle { get => idConfigSpectacle; set => idConfigSpectacle = value; }
        public string IdSpectacle { get => idSpectacle; set => idSpectacle = value; }
        public string IdZone { get => idZone; set => idZone = value; }
        public int DebutNum { get => debutNum; set => debutNum = value; }
        public string SensHorizontal { get => sensHorizontal; set => sensHorizontal = value; }
        public string SensVertical { get => sensVertical; set => sensVertical = value; }
        public string Direction { get => direction; set => direction = value; }
    }
}
