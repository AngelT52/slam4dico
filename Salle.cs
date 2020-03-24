using System;
using System.Collections.Generic;
using System.Text;

namespace Slam4Dico
{
   class Salle
    {
        private string nom;
        private Dictionary<int, Poste> lesPostes;

        public Salle(String unNom)
        {
            nom = unNom;
            lesPostes = new Dictionary<int, Poste>(); 
        }

        public void AjouterPoste(Poste unPoste) 
        {
            lesPostes.Add(unPoste.getNuméro(), unPoste);
        }

        public Dictionary<int, Poste> getLesPostes() 
        {
            return (lesPostes);
        }
    }
}
