using System;
using System.Collections.Generic;
using System.Text;

namespace CA_Nobeldij
{
    class Nobeldij
    {
        public int Ev { get; set; }
        public string Nev { get; set; }
        public string Szuleteshalalozas { get; set; }
        public string Orszagkod { get; set; }

        public Nobeldij(string sor)
        {
            var atmeneti = sor.Split(';');
            Ev = int.Parse(atmeneti[0]);
            Nev = atmeneti[1];
            Szuleteshalalozas = atmeneti[2];
            Orszagkod = atmeneti[3];
        }
    }
}
