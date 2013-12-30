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

        public void chargerCarte(ref Carte c)
        {
            StreamReader myWriter = null;
            try
            {
                XmlSerializer mySerializer = new XmlSerializer(c.GetType());
                myWriter = new StreamReader(fileName);
                c = (Carte)mySerializer.Deserialize(myWriter);
                myWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                myWriter.Close();

            }
        }
    }
}
