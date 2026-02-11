using System;
using System.Collections.Generic;
using System.Text;

namespace dsaprojekt.Graphs
{
	public class Node<T>
	{
		public T Data { get; set; }
		public List<Edge<T>> Edges { get; set; }

		public Node(T data)
		{
			Data = data;
			Edges = new List<Edge<T>>();
		}

		public void AddEdge(Node<T> other)
		{
			Edges.Add(new Edge<T>(this, other));
		}
	}
}
