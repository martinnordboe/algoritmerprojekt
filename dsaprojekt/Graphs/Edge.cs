using System;
using System.Collections.Generic;
using System.Text;

namespace dsaprojekt.Graphs
{
	public class Edge<T>
	{
		// Properties...
		public Node<T> From { get; set; }
		public Node<T> To { get; set; }

		// En constructor...
		public Edge(Node<T> from, Node<T> to)
		{
			From = from;
			To = to;
		}
	}
}
