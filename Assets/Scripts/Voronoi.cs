using System;
using UnityEngine;
using System.Collections.Generic;

namespace Voronoi2
{
	public class Voronoi
	{
		// ************* Приватные поля ******************
		double borderMinX, borderMaxX, borderMinY, borderMaxY;
		int siteidx;
		double xmin, xmax, ymin, ymax, deltax, deltay;
		int nvertices;
		int nedges;
		int nsites;
		Site[] sites;
		Site bottomsite;
		int sqrt_nsites;
		double minDistanceBetweenSites;
		int PQcount;
		int PQmin;
		int PQhashsize;
		Halfedge[] PQhash;

		const int LE = 0;
		const int RE = 1;

		int ELhashsize;
		Halfedge[] ELhash;
		Halfedge ELleftend, ELrightend;
		List<GraphEdge> allEdges;

		public Voronoi(double minDistanceBetweenSites)
		{
			siteidx = 0;
			sites = null;

			allEdges = null;
			this.minDistanceBetweenSites = minDistanceBetweenSites;
		}

		/**
		 * 
		 * @param xValuesIn Array of X values for each site.
		 * @param yValuesIn Array of Y values for each site. Must be identical length to yValuesIn
		 * @param minX The minimum X of the bounding box around the voronoi
		 * @param maxX The maximum X of the bounding box around the voronoi
		 * @param minY The minimum Y of the bounding box around the voronoi
		 * @param maxY The maximum Y of the bounding box around the voronoi
		 * @return
		 */
		// процесс, необходимый для создания диаграммы
		public List<GraphEdge> generateVoronoi(double[] xValuesIn, double[] yValuesIn, double minX, double maxX, double minY, double maxY)
		{
			sort(xValuesIn, yValuesIn, xValuesIn.Length);

			// Проверяем ограничивающие входные данные - если минимумы больше, чем максимумы, меняем их местами
			double temp = 0;
			if (minX > maxX)
			{
				temp = minX;
				minX = maxX;
				maxX = temp;
			}
			if (minY > maxY)
			{
				temp = minY;
				minY = maxY;
				maxY = temp;
			}

			borderMinX = minX;
			borderMinY = minY;
			borderMaxX = maxX;
			borderMaxY = maxY;

			siteidx = 0;
			voronoi_bd();
			return allEdges;
		}



		private void sort(double[] xValuesIn, double[] yValuesIn, int count)
		{
			sites = null;
			allEdges = new List<GraphEdge>();

			nsites = count;
			nvertices = 0;
			nedges = 0;

			double sn = (double)nsites + 4;
			sqrt_nsites = (int)Math.Sqrt(sn);

			// копируем входные данные
			double[] xValues = new double[count];
			double[] yValues = new double[count];
			for (int i = 0; i < count; i++)
			{
				xValues[i] = xValuesIn[i];
				yValues[i] = yValuesIn[i];
			}
			sortNode(xValues, yValues, count);
		}

		private void qsort(Site[] sites)
		{
			List<Site> listSites = new List<Site>(sites.Length);
			for (int i = 0; i < sites.Length; i++)
			{
				listSites.Add(sites[i]);
			}

			listSites.Sort(new SiteSorterYX());

			// копируем обратно в массив
			for (int i = 0; i < sites.Length; i++)
			{
				sites[i] = listSites[i];
			}
		}

		private void sortNode(double[] xValues, double[] yValues, int numPoints)
		{
			nsites = numPoints;
			sites = new Site[nsites];
			xmin = xValues[0];
			ymin = yValues[0];
			xmax = xValues[0];
			ymax = yValues[0];

			for (int i = 0; i < nsites; i++)
			{
				sites[i] = new Site();
				sites[i].coord = new Point(xValues[i], yValues[i]);
				sites[i].sitenbr = i;

				if (xValues[i] < xmin)
					xmin = xValues[i];
				else if (xValues[i] > xmax)
					xmax = xValues[i];

				if (yValues[i] < ymin)
					ymin = yValues[i];
				else if (yValues[i] > ymax)
					ymax = yValues[i];
			}

			qsort(sites);
			deltax = xmax - xmin;
			deltay = ymax - ymin;
		}

		private Site NextOne()
		{
			Site s;
			if (siteidx < nsites)
			{
				s = sites[siteidx];
				siteidx++;
				return s;
			}
			return null;
		}

		private Edge bisect(Site s1, Site s2)
		{
			double dx, dy, adx, ady;
			Edge newedge;

			newedge = new Edge();

			newedge.reg[0] = s1;
			newedge.reg[1] = s2;

			newedge.ep[0] = null;
			newedge.ep[1] = null;

			dx = s2.coord.x - s1.coord.x;
			dy = s2.coord.y - s1.coord.y;

			adx = dx > 0 ? dx : -dx;
			ady = dy > 0 ? dy : -dy;
			newedge.c = (double)(s1.coord.x * dx + s1.coord.y * dy + (dx * dx + dy * dy) * 0.5);

			if (adx > ady)
			{
				newedge.a = 1.0;
				newedge.b = dy / dx;
				newedge.c /= dx;
			}
			else
			{
				newedge.a = dx / dy;
				newedge.b = 1.0;
				newedge.c /= dy;
			}

			newedge.edgenbr = nedges;
			nedges++;

			return newedge;
		}

		private void makevertex(Site v)
		{
			v.sitenbr = nvertices;
			nvertices++;
		}

		private bool PQinitialize()
		{
			PQcount = 0;
			PQmin = 0;
			PQhashsize = 4 * sqrt_nsites;
			PQhash = new Halfedge[PQhashsize];

			for (int i = 0; i < PQhashsize; i++)
			{
				PQhash[i] = new Halfedge();
			}
			return true;
		}

		private int PQbucket(Halfedge he)
		{
			int bucket;

			bucket = (int)((he.ystar - ymin) / deltay * PQhashsize);
			if (bucket < 0)
				bucket = 0;
			if (bucket >= PQhashsize)
				bucket = PQhashsize - 1;
			if (bucket < PQmin)
				PQmin = bucket;

			return bucket;
		}

		// вставляем HalfEdge (половину ребра) в упорядоченный связанный список вершин
		private void PQinsert(Halfedge he, Site v, double offset)
		{
			Halfedge last, next;

			he.vertex = v;
			he.ystar = (double)(v.coord.y + offset);
			last = PQhash[PQbucket(he)];

			while
				(
					(next = last.PQnext) != null
					&&
					(he.ystar > next.ystar || (he.ystar == next.ystar && v.coord.x > next.vertex.coord.x))
				)
			{
				last = next;
			}

			he.PQnext = last.PQnext;
			last.PQnext = he;
			PQcount++;
		}

		// удаляем HalfEdge из списка вершин
		private void PQdelete(Halfedge he)
		{
			Halfedge last;

			if (he.vertex != null)
			{
				last = PQhash[PQbucket(he)];
				while (last.PQnext != he)
				{
					last = last.PQnext;
				}

				last.PQnext = he.PQnext;
				PQcount--;
				he.vertex = null;
			}
		}

		private bool PQempty()
		{
			return (PQcount == 0);
		}

		private Point PQMin()
		{
			Point answer;// = new Point();

			while (PQhash[PQmin].PQnext == null)
			{
				PQmin++;
			}

			answer = new Point(
				PQhash[PQmin].PQnext.vertex.coord.x,
				PQhash[PQmin].PQnext.ystar);
			return answer;
		}

		private Halfedge PQextractmin()
		{
			Halfedge curr;

			curr = PQhash[PQmin].PQnext;
			PQhash[PQmin].PQnext = curr.PQnext;
			PQcount--;

			return curr;
		}

		private Halfedge HEcreate(Edge e, int pm)
		{
			Halfedge answer = new Halfedge();
			answer.ELedge = e;
			answer.ELpm = pm;
			answer.PQnext = null;
			answer.vertex = null;

			return answer;
		}

		private bool ELinitialize()
		{
			ELhashsize = 2 * sqrt_nsites;
			ELhash = new Halfedge[ELhashsize];

			ELleftend = HEcreate(null, 0);
			ELrightend = HEcreate(null, 0);
			ELleftend.ELleft = null;
			ELleftend.ELright = ELrightend;
			ELrightend.ELleft = ELleftend;
			ELrightend.ELright = null;
			ELhash[0] = ELleftend;
			ELhash[ELhashsize - 1] = ELrightend;

			return true;
		}

		private Halfedge ELright(Halfedge he)
		{
			return he.ELright;
		}

		private Halfedge ELleft(Halfedge he)
		{
			return he.ELleft;
		}

		private Site LeftReg(Halfedge he)
		{
			if (he.ELedge == null)
			{
				return bottomsite;
			}
			return (he.ELpm == LE ? he.ELedge.reg[LE] : he.ELedge.reg[RE]);
		}

		private void ELinsert(Halfedge lb, Halfedge newHe)
		{
			newHe.ELleft = lb;
			newHe.ELright = lb.ELright;
			(lb.ELright).ELleft = newHe;
			lb.ELright = newHe;
		}

		// Эта процедура удаления не может вернуть узел, так как могут присутствовать указатели из хэш-таблицы

		private void ELdelete(Halfedge he)
		{
			(he.ELleft).ELright = he.ELright;
			(he.ELright).ELleft = he.ELleft;
			he.deleted = true;
		}

		// Получаем запись из хэш-таблицы, удаляя все удаленные узлы
		private Halfedge ELgethash(int b)
		{
			Halfedge he;
			if (b < 0 || b >= ELhashsize)
				return null;

			he = ELhash[b];
			if (he == null || !he.deleted)
				return he;

			// Хэш-таблица указывает на удаленную половину ребра
			ELhash[b] = null;
			return null;
		}

		private Halfedge ELleftbnd(Point p)
		{
			int bucket;
			Halfedge he;

			/* Use hash table to get close to desired halfedge */
			// use the hash function to find the place in the hash map that this
			// HalfEdge should be
			bucket = (int)((p.x - xmin) / deltax * ELhashsize);

			// make sure that the bucket position is within the range of the hash
			// array
			if (bucket < 0) bucket = 0;
			if (bucket >= ELhashsize) bucket = ELhashsize - 1;

			he = ELgethash(bucket);

			// if the HE isn't found, search backwards and forwards in the hash map
			// for the first non-null entry
			if (he == null)
			{
				for (int i = 1; i < ELhashsize; i++)
				{
					if ((he = ELgethash(bucket - i)) != null)
						break;
					if ((he = ELgethash(bucket + i)) != null)
						break;
				}
			}

			/* Now search linear list of halfedges for the correct one */
			if (he == ELleftend || (he != ELrightend && RightOf(he, p)))
			{
				// keep going right on the list until either the end is reached, or
				// you find the 1st edge which the point isn't to the right of
				do
				{
					he = he.ELright;
				}
				while (he != ELrightend && RightOf(he, p));
				he = he.ELleft;
			}
			else
			// if the point is to the left of the HalfEdge, then search left for
			// the HE just to the left of the point
			{
				do
				{
					he = he.ELleft;
				}
				while (he != ELleftend && !RightOf(he, p));
			}

			/* Update hash table and reference counts */
			if (bucket > 0 && bucket < ELhashsize - 1)
			{
				ELhash[bucket] = he;
			}

			return he;
		}

		private void PushGraphEdge(Site leftSite, Site rightSite, double x1, double y1, double x2, double y2)
		{
			GraphEdge newEdge = new GraphEdge();
			allEdges.Add(newEdge);
			newEdge.x1 = x1;
			newEdge.y1 = y1;
			newEdge.x2 = x2;
			newEdge.y2 = y2;

			newEdge.site1 = leftSite.sitenbr;
			newEdge.site2 = rightSite.sitenbr;
		}

		private void clip_line(Edge e)
		{
			double pxmin, pxmax, pymin, pymax;
			Site s1, s2;

			double x1 = e.reg[0].coord.x;
			double y1 = e.reg[0].coord.y;
			double x2 = e.reg[1].coord.x;
			double y2 = e.reg[1].coord.y;
			double x = x2 - x1;
			double y = y2 - y1;

			// если расстояние между двумя точками, из которых была создана эта линия, меньше квадратного корня из 2 ?, игнорируем его
			if (Math.Sqrt((x * x) + (y * y)) < minDistanceBetweenSites)
			{
				return;
			}
			pxmin = borderMinX;
			pymin = borderMinY;
			pxmax = borderMaxX;
			pymax = borderMaxY;

			if (e.a == 1.0 && e.b >= 0.0)
			{
				s1 = e.ep[1];
				s2 = e.ep[0];
			}
			else
			{
				s1 = e.ep[0];
				s2 = e.ep[1];
			}

			if (e.a == 1.0)
			{
				y1 = pymin;

				if (s1 != null && s1.coord.y > pymin)
					y1 = s1.coord.y;
				if (y1 > pymax)
					y1 = pymax;
				x1 = e.c - e.b * y1;
				y2 = pymax;

				if (s2 != null && s2.coord.y < pymax)
					y2 = s2.coord.y;
				if (y2 < pymin)
					y2 = pymin;
				x2 = e.c - e.b * y2;
				if (((x1 > pxmax) & (x2 > pxmax)) | ((x1 < pxmin) & (x2 < pxmin)))
					return;

				if (x1 > pxmax)
				{
					x1 = pxmax;
					y1 = (e.c - x1) / e.b;
				}
				if (x1 < pxmin)
				{
					x1 = pxmin;
					y1 = (e.c - x1) / e.b;
				}
				if (x2 > pxmax)
				{
					x2 = pxmax;
					y2 = (e.c - x2) / e.b;
				}
				if (x2 < pxmin)
				{
					x2 = pxmin;
					y2 = (e.c - x2) / e.b;
				}

			}
			else
			{
				x1 = pxmin;
				if (s1 != null && s1.coord.x > pxmin)
					x1 = s1.coord.x;
				if (x1 > pxmax)
					x1 = pxmax;
				y1 = e.c - e.a * x1;

				x2 = pxmax;
				if (s2 != null && s2.coord.x < pxmax)
					x2 = s2.coord.x;
				if (x2 < pxmin)
					x2 = pxmin;
				y2 = e.c - e.a * x2;

				if (((y1 > pymax) & (y2 > pymax)) | ((y1 < pymin) & (y2 < pymin)))
					return;

				if (y1 > pymax)
				{
					y1 = pymax;
					x1 = (e.c - y1) / e.a;
				}
				if (y1 < pymin)
				{
					y1 = pymin;
					x1 = (e.c - y1) / e.a;
				}
				if (y2 > pymax)
				{
					y2 = pymax;
					x2 = (e.c - y2) / e.a;
				}
				if (y2 < pymin)
				{
					y2 = pymin;
					x2 = (e.c - y2) / e.a;
				}
			}

			PushGraphEdge(e.reg[0], e.reg[1], x1, y1, x2, y2);
		}

		private void EndPoint(Edge e, int lr, Site s)
		{
			e.ep[lr] = s;
			if (e.ep[RE - lr] == null)
				return;
			clip_line(e);
		}

		// возвращает истину, если p справа от половины ребра e
		private bool RightOf(Halfedge el, Point p)
		{
			Edge e;
			Site topsite;
			bool right_of_site;
			bool above, fast;
			double dxp, dyp, dxs, t1, t2, t3, yl;

			e = el.ELedge;
			topsite = e.reg[1];

			if (p.x > topsite.coord.x)
				right_of_site = true;
			else
				right_of_site = false;

			if (right_of_site && el.ELpm == LE)
				return true;
			if (!right_of_site && el.ELpm == RE)
				return false;

			if (e.a == 1.0)
			{
				dxp = p.x - topsite.coord.x;
				dyp = p.y - topsite.coord.y;
				fast = false;

				if ((!right_of_site & (e.b < 0.0)) | (right_of_site & (e.b >= 0.0)))
				{
					above = dyp >= e.b * dxp;
					fast = above;
				}
				else
				{
					above = p.x + p.y * e.b > e.c;
					if (e.b < 0.0)
						above = !above;
					if (!above)
						fast = true;
				}
				if (!fast)
				{
					dxs = topsite.coord.x - (e.reg[0]).coord.x;
					above = e.b * (dxp * dxp - dyp * dyp)
					< dxs * dyp * (1.0 + 2.0 * dxp / dxs + e.b * e.b);

					if (e.b < 0)
						above = !above;
				}
			}
			else // e.b == 1.0
			{
				yl = e.c - e.a * p.x;
				t1 = p.y - yl;
				t2 = p.x - topsite.coord.x;
				t3 = yl - topsite.coord.y;
				above = t1 * t1 > t2 * t2 + t3 * t3;
			}
			return (el.ELpm == LE ? above : !above);
		}

		private Site rightreg(Halfedge he)
		{
			if (he.ELedge == (Edge)null)
			// если эта половина не имеет ребра, вернуть нижнюю сторону (что бы под этим не подразумевалось)
			{
				return (bottomsite);
			}

			// если поле ELpm равно нулю, вернуть сторону 0, которая это ребро делит пополам, в противном случае вернуть сторону номер 1
			return (he.ELpm == LE ? he.ELedge.reg[RE] : he.ELedge.reg[LE]);
		}

		private double Dist(Site s, Site t)
		{
			double dx, dy;
			dx = s.coord.x - t.coord.x;
			dy = s.coord.y - t.coord.y;
			return Math.Sqrt(dx * dx + dy * dy);
		}

		// создаем новую сторону, где пересекаются половины ребер el1 и el2 
		// точка в списке аргументов не используется
		private Site Intersect(Halfedge el1, Halfedge el2)
		{
			Edge e1, e2, e;
			Halfedge el;
			double d, x, y;
			bool right_of_site;
			Site v; // вершина

			e1 = el1.ELedge;
			e2 = el2.ELedge;

			if (e1 == null || e2 == null)
				return null;

			// если два ребра делят пополам одного и того же родителя, вернуть null
			if (e1.reg[1] == e2.reg[1])
				return null;

			d = e1.a * e2.b - e1.b * e2.a;
			if (-1.0e-10 < d && d < 1.0e-10)
				return null;

			x = (e1.c * e2.b - e2.c * e1.b) / d;
			y = (e2.c * e1.a - e1.c * e2.a) / d;

			if ((e1.reg[1].coord.y < e2.reg[1].coord.y)
				|| (e1.reg[1].coord.y == e2.reg[1].coord.y && e1.reg[1].coord.x < e2.reg[1].coord.x))
			{
				el = el1;
				e = e1;
			}
			else
			{
				el = el2;
				e = e2;
			}

			right_of_site = x >= e.reg[1].coord.x;
			if ((right_of_site && el.ELpm == LE)
				|| (!right_of_site && el.ELpm == RE))
				return null;

			// создаем новый сайт в точке пересечения - новое векторное событие
			v = new Site();
			v.coord = new Point(x, y);
			return v;
		}


		// неявные параметры: nsites, sqrt_nsites, xmin, xmax, ymin, ymax, deltax, deltay (все они могут быть оценочными). 
		// Производительность страдает; лучше сделать nsites, deltax и deltay скорее побольше, чем поменьше

		private bool voronoi_bd()
		{
			Site newsite, bot, top, temp, p;
			Site v;
			Point newintstar = null;
			int pm;
			Halfedge lbnd, rbnd, llbnd, rrbnd, bisector;
			Edge e;

			PQinitialize();
			ELinitialize();

			bottomsite = NextOne();
			newsite = NextOne();
			while (true)
			{
				if (!PQempty())
				{
					newintstar = PQMin();
				}
				// если нижняя сторона имеет меньшее значение y, чем нижнее векторное пересечение - обработать сторону, иначе обработать векторное пересечение


				if (newsite != null && (PQempty()
										|| newsite.coord.y < newintstar.y
										|| (newsite.coord.y == newintstar.y
										&& newsite.coord.x < newintstar.x)))
				{
					/* new site is smallest -this is a site event */
					// get the first HalfEdge to the LEFT of the new site
					lbnd = ELleftbnd((newsite.coord));
					// get the first HalfEdge to the RIGHT of the new site
					rbnd = ELright(lbnd);
					// if this halfedge has no edge,bot =bottom site (whatever that
					// is)
					bot = rightreg(lbnd);
					// create a new edge that bisects
					e = bisect(bot, newsite);

					// create a new HalfEdge, setting its ELpm field to 0
					bisector = HEcreate(e, LE);
					// insert this new bisector edge between the left and right
					// vectors in a linked list
					ELinsert(lbnd, bisector);

					// if the new bisector intersects with the left edge,
					// remove the left edge's vertex, and put in the new one
					if ((p = Intersect(lbnd, bisector)) != null)
					{
						PQdelete(lbnd);
						PQinsert(lbnd, p, Dist(p, newsite));
					}
					lbnd = bisector;
					// create a new HalfEdge, setting its ELpm field to 1
					bisector = HEcreate(e, RE);
					// insert the new HE to the right of the original bisector
					// earlier in the IF stmt
					ELinsert(lbnd, bisector);

					// if this new bisector intersects with the new HalfEdge
					if ((p = Intersect(bisector, rbnd)) != null)
					{
						// push the HE into the ordered linked list of vertices
						PQinsert(bisector, p, Dist(p, newsite));
					}
					newsite = NextOne();
				}
				else if (!PQempty())
				/* intersection is smallest - this is a vector event */
				{
					// pop the HalfEdge with the lowest vector off the ordered list
					// of vectors
					lbnd = PQextractmin();
					// get the HalfEdge to the left of the above HE
					llbnd = ELleft(lbnd);
					// get the HalfEdge to the right of the above HE
					rbnd = ELright(lbnd);
					// get the HalfEdge to the right of the HE to the right of the
					// lowest HE
					rrbnd = ELright(rbnd);
					// get the Site to the left of the left HE which it bisects
					bot = LeftReg(lbnd);
					// get the Site to the right of the right HE which it bisects
					top = rightreg(rbnd);

					v = lbnd.vertex; // get the vertex that caused this event
					makevertex(v); // set the vertex number - couldn't do this
								   // earlier since we didn't know when it would be processed
					EndPoint(lbnd.ELedge, lbnd.ELpm, v);
					// set the EndPoint of
					// the left HalfEdge to be this vector
					EndPoint(rbnd.ELedge, rbnd.ELpm, v);
					// set the EndPoint of the right HalfEdge to
					// be this vector
					ELdelete(lbnd); // mark the lowest HE for
									// deletion - can't delete yet because there might be pointers
									// to it in Hash Map
					PQdelete(rbnd);
					// remove all vertex events to do with the right HE
					ELdelete(rbnd); // mark the right HE for
									// deletion - can't delete yet because there might be pointers
									// to it in Hash Map
					pm = LE; // set the pm variable to zero

					if (bot.coord.y > top.coord.y)
					// if the site to the left of the event is higher than the
					// Site
					{ // to the right of it, then swap them and set the 'pm'
					  // variable to 1
						temp = bot;
						bot = top;
						top = temp;
						pm = RE;
					}
					e = bisect(bot, top); // create an Edge (or line)
										  // that is between the two Sites. This creates the formula of
										  // the line, and assigns a line number to it
					bisector = HEcreate(e, pm); // create a HE from the Edge 'e',
												// and make it point to that edge
												// with its ELedge field
					ELinsert(llbnd, bisector); // insert the new bisector to the
											   // right of the left HE
					EndPoint(e, RE - pm, v); // set one EndPoint to the new edge
											 // to be the vector point 'v'.
											 // If the site to the left of this bisector is higher than the
											 // right Site, then this EndPoint
											 // is put in position 0; otherwise in pos 1

					// if left HE and the new bisector Intersect, then delete
					// the left HE, and reinsert it
					if ((p = Intersect(llbnd, bisector)) != null)
					{
						PQdelete(llbnd);
						PQinsert(llbnd, p, Dist(p, bot));
					}

					// if right HE and the new bisector Intersect, then
					// reinsert it
					if ((p = Intersect(bisector, rrbnd)) != null)
					{
						PQinsert(bisector, p, Dist(p, bot));
					}
				}
				else
				{
					break;
				}
			}

			for (lbnd = ELright(ELleftend); lbnd != ELrightend; lbnd = ELright(lbnd))
			{
				e = lbnd.ELedge;
				clip_line(e);
			}

			return true;
		}

	} // Voronoi Class End
} // namespace Voronoi2 End