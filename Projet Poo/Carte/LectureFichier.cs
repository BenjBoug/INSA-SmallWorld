using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace Modele
{
    public class LectureFichier : ICreationCarte
    {
        public LectureFichier(string fileName)
        {
            this.fileName = fileName;
        }

        private string fileName;

        public void chargerCarte(Carte c)
        {
            //XmlSerializer mySerializer = new XmlSerializer(c.GetType());
            BinaryFormatter mySerializer = new BinaryFormatter();
            FileStream myFileStream = new FileStream(fileName, FileMode.Open);
            c = (Carte)mySerializer.Deserialize(myFileStream);
        }
    }
}
