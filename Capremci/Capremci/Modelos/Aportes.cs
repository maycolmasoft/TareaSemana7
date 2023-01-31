using System;
using System.Collections.Generic;
using System.Text;

namespace Capremci.Modelos
{
    public class Aportes
    {
        public int id_contribucion_tipo { get; set; }
        public int id_participes { get; set; }
        public string tipo { get; set; }
        public float aporte { get; set; }

    }

    public class DetalleAportes
    {
        public DateTime fecha { get; set; }
        public float aporte { get; set; }

    }
}
