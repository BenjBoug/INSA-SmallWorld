using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCLR;
using System.Xml.Serialization;
using System.Xml;

namespace Modele
{
    [XmlInclude(typeof(CarteClassique))]
    public abstract class Carte : ICarte, IXmlSerializable
    {

        enum PeupleInt { Gaulois = 1, Viking = 0, Nain = 2 };
        enum CaseInt { Plaine = 0, Eau = 1, Montagne = 2, Desert = 3, Foret = 4 };

        public Carte()
        {
            fabriqueCase = new FabriqueCase();
        }


        protected List<Unite>[][] unites;
        [XmlIgnore]
        public List<Unite>[][] Unites
        {
            get { return unites; }
            set { unites = value; }
        }

        protected Case[][] cases;

        public Case[][] Cases
        {
            get { return cases; }
            set { cases = value; }
        }
        private FabriqueCase fabriqueCase;

        [XmlIgnore]
        public FabriqueCase FabriqueCase
        {
            get { return fabriqueCase; }
            set { fabriqueCase = value; }
        }

        private int largeur, hauteur, nbToursMax, nbUniteParPeuble;

        public int NbUniteParPeuble
        {
            get { return nbUniteParPeuble; }
            set { nbUniteParPeuble = value; }
        }

        public int NbToursMax
        {
            get { return nbToursMax; }
            set { nbToursMax = value; }
        }

        public int Hauteur
        {
            get { return hauteur; }
            set { hauteur = value; }
        }

        public int Largeur
        {
            get { return largeur; }
            set { largeur = value; }
        }

        public bool estAdjacente(int x, int y, int x2, int y2)
        {
            return ((Math.Abs(x - x2) <= 1) && (Math.Abs(y - y2) <= 1));
        }

        public abstract Unite getAdversaire();
        public abstract void calculerPoints();

        public void chargerCarte(ICreationCarte creationCarte)
        {
            creationCarte.chargerCarte(this);
        }

        public abstract bool caseVide(int x, int y);

        public Case getCase(int x, int y)
        {
            return Cases[x][y];
        }

        public void setCase(int x, int y, Case _case)
        {
            Cases[x][y] = _case;
        }

        public int[] getCoord(IUnite u)
        {
            int[] coord = new int[2];

            for (int i = 0; i < largeur; i++)
            {
                for (int j = 0; j < hauteur; j++)
                {
                    if (unites[i][j] != null && unites[i][j].Contains(u))
                    {
                        coord[0] = i;
                        coord[1] = j;
                        break;
                    }
                }
            }

            return coord;
        }

        public abstract void selectionneUnite(IUnite unite);

        public abstract void selectionneCase(int x, int y);

        public abstract void placeUnite(List<Unite> list);

        public void actualiseDeplacement()
        {
            for (int i = 0; i < largeur; i++)
            {
                for (int j = 0; j < hauteur; j++)
                {
                    if (unites[i][j] != null && unites[i][j].Count > 0)
                    {
                        foreach(Unite u in unites[i][j])
                        {
                            u.PointsDepl += 1;
                        }
                    }
                }
            }
        }


        public void deplaceUnite(Unite u, int column, int row)
        {
            for (int i = 0; i < largeur; i++)
            {
                for (int j = 0; j < hauteur; j++)
                {
                    if (unites[i][j] != null && unites[i][j].Count > 0)
                    {
                        if (unites[i][j].Contains(u))
                        {
                            if (unites[column][row] == null)
                                unites[column][row] = new List<Unite>();
                            unites[column][row].Add(u);
                            unites[i][j].Remove(u);
                        }
                    }
                }
            }

            u.PointsDepl--;

        }

        public int[][] suggestion(IUnite unite, int x, int y)
        {
            WrapperMapAleatoire wrap = new WrapperMapAleatoire();
            List<int> emplUnites = new List<int>();
            for (int i = 0; i < Largeur; i++)
            {
                for (int j = 0; j < Hauteur; j++)
                {
                    if (Unites[i][j] != null)
                        emplUnites.Add(Unites[i][j].Count);
                    else
                        emplUnites.Add(0);
                }
            }

            List<int> carteInt = new List<int>();
            for (int i = 0; i < Largeur; i++)
            {
                for (int j = 0; j < Hauteur; j++)
                {
                    CaseInt tile = CaseInt.Plaine;
                    if (Cases[i][j] is CaseDesert)
                    {
                        tile = CaseInt.Desert;
                    }
                    else if (Cases[i][j] is CaseEau)
                    {
                        tile = CaseInt.Eau;
                    }
                    else if (Cases[i][j] is CaseForet)
                    {
                        tile = CaseInt.Foret;
                    }
                    else if (Cases[i][j] is CaseMontagne)
                    {
                        tile = CaseInt.Montagne;
                    }
                    else if (Cases[i][j] is CasePlaine)
                    {
                        tile = CaseInt.Plaine;
                    }
                    carteInt.Add((int)tile);
                }
            }

            PeupleInt peuple = PeupleInt.Gaulois;
            IPeuple p = unite.Proprietaire.Peuple;

            if (p is PeupleViking)
                peuple = PeupleInt.Viking;
            else if (p is PeupleNain)
                peuple = PeupleInt.Nain;
            else if (p is PeupleGaulois)
                peuple = PeupleInt.Gaulois;

            List<int> sugg = wrap.getSuggestion(carteInt, emplUnites, Largeur, x, y, unite.PointsDepl, (int)peuple);

            int[][] res = new int[Largeur][];
            for (int i = 0; i < Largeur; i++)
            {
                res[i] = new int[Hauteur];
            }

            for (int i = 0; i < Largeur; i++)
            {
                for (int j = 0; j < Hauteur; j++)
                {
                    res[i][j] = sugg[i*Largeur + j];
                }
            }

            return res;
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "Largeur")
                    {
                        if (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Text)
                                this.Largeur = int.Parse(reader.Value);
                        }
                    }
                    else if (reader.Name == "Hauteur")
                    {
                        if (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Text)
                                this.Hauteur = int.Parse(reader.Value);
                        }
                    }
                    else if (reader.Name == "NbToursMax")
                    {
                        if (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Text)
                                this.NbToursMax = int.Parse(reader.Value);
                        }
                    }
                    else if (reader.Name == "NbUniteParPeuble")
                    {
                        if (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Text)
                                this.NbUniteParPeuble = int.Parse(reader.Value);
                        }
                    }
                    else if (reader.Name == "Cases")
                    {
                        if (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                XmlSerializer xs = new XmlSerializer(typeof(Case[][]));
                                this.Cases = xs.Deserialize(reader) as Case[][];
                            }
                        }
                    }
                    else if (reader.Name == "Unites")
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {

                                if (Unites == null)
                                {
                                    Unites = new List<Unite>[Largeur][];
                                    for (int i = 0; i < Largeur; i++)
                                    {
                                        Unites[i] = new List<Unite>[Hauteur];
                                    }
                                }
                                int x = int.Parse(reader.GetAttribute("x"));
                                int y = int.Parse(reader.GetAttribute("y"));
                                if (reader.Read())
                                {
                                    if (reader.NodeType == XmlNodeType.Element)
                                    {
                                        XmlSerializer xs = new XmlSerializer(typeof(List<Unite>));
                                        this.Unites[x][y] = xs.Deserialize(reader) as List<Unite>;//
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {


            XmlSerializer xs;

            writer.WriteElementString("Largeur", this.Largeur.ToString());
            writer.WriteElementString("Hauteur", this.Hauteur.ToString());
            writer.WriteElementString("NbToursMax", this.NbToursMax.ToString());
            writer.WriteElementString("NbUniteParPeuble", this.NbUniteParPeuble.ToString());


            writer.WriteStartElement("Cases");
            xs = new XmlSerializer(typeof(Case[][]));
            xs.Serialize(writer, this.Cases);
            writer.WriteEndElement();

            
            writer.WriteStartElement("Unites");
            for (int i = 0; i < Largeur; i++)
            {
                for (int j = 0; j < Hauteur; j++)
                {
                    if (this.Unites[i][j] != null && this.Unites[i][j].Count > 0)
                    {
                        writer.WriteStartElement("Coordonnees");
                        writer.WriteAttributeString("x", i.ToString());
                        writer.WriteAttributeString("y", j.ToString());
                        xs = new XmlSerializer(typeof(List<Unite>));
                        xs.Serialize(writer, this.Unites[i][j]);
                        writer.WriteEndElement();
                    }
                }
            }
            writer.WriteEndElement();
        }
    }
}
