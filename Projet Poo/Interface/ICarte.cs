using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public interface ICarte
    {
        void calculerPoints();
        bool caseVide(int x, int y);
        ICase getCase(int x, int y);
        void selectionneUnite(IUnite unite);
        void selectionneCase(int x, int y);

        void placeUnite(List<Unite> l);

    }
}
