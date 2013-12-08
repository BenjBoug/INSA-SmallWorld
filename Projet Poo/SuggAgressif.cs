using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class SuggAgressif : Suggestion
    {
        Carte carte;
        Unite unite;
        List<Node> closedset = new List<Node>();
        List<Node> openset = new List<Node>();
        Dictionary<Node, Node> came_from = new Dictionary<Node, Node>();
        public override int[][][] getSuggestion(Carte carte, Unite unite)
        {
            this.carte = carte;
            this.unite = unite;
            //on récupère les suggestions classique en fonction du terrain
            int[][][] res = base.getSuggestion(carte, unite);

            
            List<Coordonnees> listCoordAttaquableProche = new List<Coordonnees>();
            List<Coordonnees> listCoordAttaquable = new List<Coordonnees>();
            // cherche les unités a proximité, et les autres
            foreach (Unite u in carte.Unites.Where(z => z.IdProprietaire!=unite.IdProprietaire))
            {
                if (res[u.Coord.X][u.Coord.Y][0] != 0)
                {
                    if (!listCoordAttaquableProche.Contains(u.Coord))
                        listCoordAttaquableProche.Add(u.Coord);
                }
                else
                {
                    if (!listCoordAttaquable.Contains(u.Coord))
                        listCoordAttaquable.Add(u.Coord);
                }
            }
            if (listCoordAttaquableProche.Count > 0) // si il y a des unités a proximité, on l'attaque
            {
                Coordonnees coordAttable = null;
                int min = int.MaxValue;
                foreach (Coordonnees coord in listCoordAttaquableProche)
                {
                    List<Unite> tmp = carte.getUniteFromCoord(coord);
                    if (tmp.Count < min)
                    {
                        coordAttable = coord;
                        min = tmp.Count;
                    }
                }

                res[coordAttable.X][coordAttable.Y][0] = int.MaxValue;
            }
            else if (listCoordAttaquable.Count>0) // sinon on se déplace vers elles
            {
                Coordonnees plusProche=null;
                double distMin = int.MaxValue;
                foreach (Coordonnees c in listCoordAttaquable)
                {
                    if (distance(unite.Coord, c) < distMin)
                    {
                        plusProche = c;
                        distMin = distance(unite.Coord, c);
                    }
                }
                List<Node> path = pathFinding(new Node(unite.Coord), new Node(plusProche));

                foreach (Node n in path)
                {
                    if (res[n.Coord.X][n.Coord.Y][0] != 0)
                        res[n.Coord.X][n.Coord.Y][0] = res[n.Coord.X][n.Coord.Y][1] + 10;
                }

            }

            return res;
        }

        private List<Node> pathFinding(Node start, Node goal)
        {
            closedset.Clear();
            openset.Clear();
            came_from.Clear();

            openset.Add(start);
            start.G_score = 0;
            start.F_score = start.G_score = distance(start, goal);
            Node current=null;
            while (openset.Count>0)
            {
                openset.Sort(new ByFScore());
                current = openset.First();

                if (current == goal)
                    return reconstruct_path(came_from, goal);

                openset.Remove(current);
                closedset.Add(current);
                foreach (Node neighbor in neighbor_nodes(current))
                {
                    double tentative_g_score = current.G_score + distance(current,neighbor);
                    double tentative_f_score = tentative_g_score + distance(neighbor, goal);

                    if (closedset.Contains(neighbor) && tentative_f_score >= neighbor.F_score)
                        continue;

                    if (!openset.Contains(neighbor) || tentative_f_score < neighbor.F_score)
                    {
                        came_from[neighbor] = current;
                        neighbor.G_score = tentative_g_score;
                        neighbor.F_score = tentative_f_score;
                        if (!openset.Contains(neighbor))
                        {
                            openset.Add(neighbor);
                        }
                    }
                }
            }

            return reconstruct_path(came_from, current);
        }

        private List<Node> reconstruct_path(Dictionary<Node, Node> came_from, Node current)
        {
            if (came_from.Keys.Contains(current))
            {
                List<Node> tmp = reconstruct_path(came_from,came_from[current]);
                tmp.Add(current);
                return tmp;
            }
            else
            {
                List<Node> path = new List<Node>();
                path.Add(current);
                return path;
            }
        }

        private List<Node> neighbor_nodes(Node current)
        {
            List<Node> res =  new List<Node>();
            Coordonnees coordCurrent = current.Coord;
            Node tmp;
            if (coordCurrent.X - 1 >= 0 && carte.Cases[coordCurrent.X-1][coordCurrent.Y].estAccessible(unite.Proprietaire.Peuple))
            {
                tmp = new Node(new Coordonnees(coordCurrent.X - 1, coordCurrent.Y));
                List<Node> tmp2 = closedset.Where(u => u == tmp).ToList();
                if (tmp2.Count>0)
                {
                    res.Add(tmp2.First());
                }
                else
                {
                    res.Add(tmp);
                }
                
            }
            if (coordCurrent.X + 1 < carte.Largeur && carte.Cases[coordCurrent.X + 1][coordCurrent.Y].estAccessible(unite.Proprietaire.Peuple))
            {
                tmp = new Node(new Coordonnees(coordCurrent.X + 1, coordCurrent.Y));
                List<Node> tmp2 = closedset.Where(u => u == tmp).ToList();
                if (tmp2.Count > 0)
                {
                    res.Add(tmp2.First());
                }
                else
                {
                    res.Add(tmp);
                }
            }
            if (coordCurrent.Y - 1 >= 0 && carte.Cases[coordCurrent.X][coordCurrent.Y-1].estAccessible(unite.Proprietaire.Peuple))
            {
                tmp = new Node(new Coordonnees(coordCurrent.X, coordCurrent.Y-1));
                List<Node> tmp2 = closedset.Where(u => u == tmp).ToList();
                if (tmp2.Count > 0)
                {
                    res.Add(tmp2.First());
                }
                else
                {
                    res.Add(tmp);
                }
            }
            if (coordCurrent.Y + 1 < carte.Hauteur && carte.Cases[coordCurrent.X][coordCurrent.Y+1].estAccessible(unite.Proprietaire.Peuple))
            {
                tmp = new Node(new Coordonnees(coordCurrent.X, coordCurrent.Y+1));
                List<Node> tmp2 = closedset.Where(u => u == tmp).ToList();
                if (tmp2.Count > 0)
                {
                    res.Add(tmp2.First());
                }
                else
                {
                    res.Add(tmp);
                }
            }

            return res;
        }

        private class Node : IComparable<Node>, IEquatable<Node>
        {
            public Node(Coordonnees coord)
            {
                this.coord = coord;
                g_score = 0;
                f_score = 0;
            }

            Coordonnees coord;

            public Coordonnees Coord
            {
                get { return coord; }
                set { coord = value; }
            }
            double g_score;

            public double G_score
            {
                get { return g_score; }
                set { g_score = value; }
            }
            double f_score;

            public double F_score
            {
                get { return f_score; }
                set { f_score = value; }
            }

            public static bool operator ==(Node a, Node b)
            {
                return (a.Coord == b.Coord);
            }

            public static bool operator !=(Node a, Node b)
            {
                return (a.Coord != b.Coord);
            }

            public override int GetHashCode()
            {
                return coord.GetHashCode();
            }

            public int CompareTo(Node other)
            {
                return ((this.coord.X - other.coord.X) + (this.coord.Y - other.coord.Y));
            }

            public override bool Equals(object obj)
            {
                Node other = obj as Node;
                return this.coord == other.coord;
            }

            public bool Equals(Node other)
            {
                return this.coord == other.coord;
            }
        }

        private class ByFScore : IComparer<Node>
        {

            public int Compare(Node x, Node y)
            {
                return (int) (x.F_score - y.F_score);
            }
        }

        private double distance(Node a, Node b)
        {
            return Math.Sqrt(Math.Pow(a.Coord.X - b.Coord.X, 2) + Math.Pow(a.Coord.Y - b.Coord.Y, 2));
        }
        private double distance(Coordonnees a, Coordonnees b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
        private int[][] getCoeffEnnemis(Carte carte, Unite unite)
        {
            int[][] res = new int[carte.Largeur][];
            for (int i = 0; i < carte.Largeur; i++)
            {
                res[i] = new int[carte.Hauteur];
                for (int j = 0; j < carte.Hauteur; j++)
                {
                    res[i][j]=0;
                }
            }
            coeffFromCoord(unite.Coord,carte,res,0);

            int k = 1;
            foreach (Unite u in carte.Unites.Where(u => u.IdProprietaire!=unite.IdProprietaire))
            {
                coeffFromCoord(u.Coord, carte, res, 0);
                k++;
            } 
            
            for (int i = 0; i < carte.Largeur; i++)
            {
                for (int j = 0; j < carte.Hauteur; j++)
                {
                    res[i][j] /= k;
                }
            }

            return res;
        }

        private void coeffFromCoord(Coordonnees coord, Carte carte, int[][] sugg, int coef)
        {
            sugg[coord.X][coord.Y]+=coef;
            if (coord.X - 1 >= 0)
            {
                if (sugg[coord.X-1][coord.Y]<coef)
                    coeffFromCoord(new Coordonnees(coord.X - 1, coord.Y), carte, sugg, coef + 1);
            }
            if (coord.X + 1 < carte.Largeur)
            {
                if (sugg[coord.X + 1][coord.Y] < coef)
                    coeffFromCoord(new Coordonnees(coord.X + 1, coord.Y), carte, sugg, coef + 1);
            }
            if (coord.Y - 1 >= 0)
            {
                if (sugg[coord.X][coord.Y - 1] < coef)
                    coeffFromCoord(new Coordonnees(coord.X, coord.Y - 1), carte, sugg, coef + 1);
            }
            if (coord.Y + 1 < carte.Hauteur)
            {
                if (sugg[coord.X][coord.Y + 1] < coef)
                 coeffFromCoord(new Coordonnees(coord.X, coord.Y + 1), carte, sugg, coef + 1);
            }
        }
    }
}
