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
            if (File.Exists(fileName))
                this.fileName = fileName;
            else
                throw new FileNotFoundException();
        }

        private string fileName;

        public void chargerCarte(Carte c)
        {
            try
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(CarteClassique));
                StreamReader myWriter = new StreamReader(fileName);
                c = (Carte)mySerializer.Deserialize(myWriter);
                myWriter.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
