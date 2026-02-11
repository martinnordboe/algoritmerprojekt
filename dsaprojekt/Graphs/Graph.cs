using System;
using System.Collections.Generic;
using System.Text;

namespace dsaprojekt.Graphs
{
	public class Graph<T>
	{
		private List<Node<T>> nodes;

		public Graph()
		{
			nodes = new List<Node<T>>();
		}

		public void AddNode(T value)
		{
			nodes.Add(new Node<T>(value));
		}

		public void AddDirectedEdge(T from, T to)
		{
			// Hmmmm... får kun en to values og ikke to Nodes.
			// Måske sammenlign med Data i Node elementer i listen?
		}

		public void AddEdge(T from, T to)
		{
			AddDirectedEdge(from, to);
			AddDirectedEdge(to, from);
		}
	}
}
