using System;
using System.Collections.Generic;
using System.Text;

namespace dsaprojekt.Graphs
{
	public class Edge<T>
	{
		public Node<T> From { get; set; }
		public Node<T> To { get; set; }

		public Edge(Node<T> from, Node<T> to)
		{
			From = from;
			To = to;
		}
	}
}
