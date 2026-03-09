using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBornesCollecte.Models
{
    public class Borne
    {
        public int IdBorne { get; set; }
        public string Nom { get; set; } = "";
        public int NiveauRemplissage { get; set; } // 0 à 100
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Adresse { get; set; } = "";
    }
}
