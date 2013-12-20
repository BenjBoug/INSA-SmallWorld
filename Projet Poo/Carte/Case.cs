using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Modele
{
    [Serializable]
    [XmlInclude(typeof(CaseDesert))]
    [XmlInclude(typeof(CasePlaine))]
    [XmlInclude(typeof(CaseEau))]
    [XmlInclude(typeof(CaseMontagne))]
    [XmlInclude(typeof(CaseForet))]
    public abstract class Case : ICase
    {
        /// <summary>
        /// le bonus de points sur la case en fonction du peuple
        /// </summary>
        /// <param name="p">le peuple</param>
        /// <returns>le bonus de points</returns>
        public virtual int bonusPoints(Peuple p)
        {
            return 1;
        }
        /// <summary>
        /// Teste si la case est acessible pour un peuple donnée
        /// </summary>
        /// <param name="p">le peuple</param>
        /// <returns>vrai si la case est acessible pour le peuple, faux sinon</returns>
        public virtual bool estAccessible(Peuple p)
        {
            return true;
        }

    }

}
