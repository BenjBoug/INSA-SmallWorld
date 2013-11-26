using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class FabriqueCase
    {
        public CaseEau caseEau;
        public CaseForet caseForet;
        public CaseDesert caseDesert;
        public CasePlaine casePlaine;
        public CaseMontagne caseMontagne;

        public FabriqueCase()
        {
            caseEau = null;
            caseForet = null;
            caseDesert = null;
            casePlaine = null;
            caseMontagne = null;
        }

        public ICase getCase(int _case)
        {
            switch (_case)
            {
                case 0:
                    if (caseEau == null)
                        caseEau = new CaseEau();
                    return caseEau;

                case 1:
                    if (caseForet == null)
                        caseForet = new CaseForet();
                    return caseForet;

                case 2:
                    if (caseDesert == null)
                        caseDesert = new CaseDesert();
                    return caseDesert;

                case 3:
                    if (casePlaine == null)
                        casePlaine = new CasePlaine();
                    return casePlaine;

                case 4:
                    if (caseMontagne == null)
                        caseMontagne = new CaseMontagne();
                    return caseMontagne;

                default:
                    return null;
                    //throw new Excetion...

            }
        }
    }
}
