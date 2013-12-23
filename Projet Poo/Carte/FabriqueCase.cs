using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCLR;

namespace Modele
{
    public class FabriqueCase
    {
        private CaseEau caseEau;
        private CaseForet caseForet;
        private CaseDesert caseDesert;
        private CasePlaine casePlaine;
        private CaseMontagne caseMontagne;

        public FabriqueCase()
        {
            caseEau = null;
            caseForet = null;
            caseDesert = null;
            casePlaine = null;
            caseMontagne = null;
        }

        /// <summary>
        /// Retourne une case en fonction d'un entier
        /// </summary>
        /// <param name="_case"></param>
        /// <returns></returns>
        public Case getCase(int _case)
        {
            switch (_case)
            {
                case (int)Case.CaseInt.Plaine:
                    if (casePlaine == null)
                        casePlaine = new CasePlaine();
                    return casePlaine;
                case (int)Case.CaseInt.Eau:
                    if (caseEau == null)
                        caseEau = new CaseEau();
                    return caseEau;

                case (int)Case.CaseInt.Montagne:
                    if (caseMontagne == null)
                        caseMontagne = new CaseMontagne();
                    return caseMontagne;

                case (int)Case.CaseInt.Desert:
                    if (caseDesert == null)
                        caseDesert = new CaseDesert();
                    return caseDesert;

                case (int)Case.CaseInt.Foret:
                    if (caseForet == null)
                        caseForet = new CaseForet();
                    return caseForet;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
