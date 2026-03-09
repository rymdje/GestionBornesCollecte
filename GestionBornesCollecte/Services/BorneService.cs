using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionBornesCollecte.Models;


namespace GestionBornesCollecte.Services
{
    public class BorneService
    {
        // V1 = données simulées (on remplacera par REST plus tard)
        public Task<List<Borne>> GetBornesAsync()
        {
            var bornes = new List<Borne>
            {
                new Borne { IdBorne = 1, Nom = "Borne Centre-Ville", Adresse = "Place de la Mairie", NiveauRemplissage = 22, Latitude = 44.618, Longitude = 4.921 },
                new Borne { IdBorne = 2, Nom = "Borne Gare", Adresse = "Rue de la Gare", NiveauRemplissage = 77, Latitude = 44.622, Longitude = 4.918 },
                new Borne { IdBorne = 3, Nom = "Borne Fac", Adresse = "Avenue des Étudiants", NiveauRemplissage = 91, Latitude = 44.615, Longitude = 4.930 },
                new Borne { IdBorne = 4, Nom = "Borne Marché", Adresse = "Place du Marché", NiveauRemplissage = 45, Latitude = 44.620, Longitude = 4.925 },
                new Borne { IdBorne = 5, Nom = "Borne Stade", Adresse = "Rue du Stade", NiveauRemplissage = 88, Latitude = 44.610, Longitude = 4.915 },
            };

            return Task.FromResult(bornes);
        }
    }
}
