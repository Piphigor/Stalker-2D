using System;
using System.Collections.Generic;

namespace Voronoi2
{
    public class Point
    {
        public int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(double x, double y)
        {
            this.x = (int)x;
            this.y = (int)y;
        }
    }

    // используется для сторон и вершин
    public class Site
    {
        public Point coord;
        public int sitenbr;
    }

    public class Edge
    {
        public double a = 0, b = 0, c = 0;
        public Site[] ep;
        public Site[] reg;
        public int edgenbr;

        public Edge()
        {
            ep = new Site[2];
            reg = new Site[2];
        }
    }


    public class Halfedge
    {
        public Halfedge ELleft, ELright;
        public Edge ELedge;
        public bool deleted;
        public int ELpm;
        public Site vertex;
        public double ystar;
        public Halfedge PQnext;

        public Halfedge()
        {
            PQnext = null;
        }
    }

    public class GraphEdge
    {
        public double x1, y1, x2, y2;
        public int site1, site2;
    }

    // установки
    public class SiteSorterYX : IComparer<Site>
    {
        public int Compare(Site p1, Site p2)
        {
            Point s1 = p1.coord;
            Point s2 = p2.coord;
            if (s1.y < s2.y) return -1;
            if (s1.y > s2.y) return 1;
            if (s1.x < s2.x) return -1;
            if (s1.x > s2.x) return 1;
            return 0;
        }
    }
}