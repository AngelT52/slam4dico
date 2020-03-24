using System;
using System.Collections.Generic;
using System.Text;

namespace Slam4Dico
{
    class Poste
    {
        private int numPoste, numTravée, numRangée;
        private List<Poste> lesPostesVisibles;

        public Poste(int unNuméro, int uneTravée, int uneRangée)
        {
            numPoste = unNuméro;
            numTravée = uneTravée;
            numRangée = uneRangée;
            lesPostesVisibles = new List<Poste>();
        }
        public int getNuméro() 
        {
            return (numPoste);
        }
        public int getNuméroTravée()
        {
            return (numTravée);
        }
        public int getNuméroRangée()
        {
            return (numRangée);
        }

        public List<Poste> getLesPostesVisibles()
        {
            return (lesPostesVisibles);
        }

        public void maj(List<Poste> lp)
        {
            foreach (Poste unlp in lp)
            {
                Poste unposte;
                unposte = lp[unlp.getNuméro()];
                lesPostesVisibles.Add(unposte);
            }
        }

        
    }
}
