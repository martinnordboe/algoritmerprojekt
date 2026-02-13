using System;
using System.Collections.Generic;
using System.Text;

namespace dsaprojekt.Graphs
{
	/// <summary>
	/// Klassens node med Data property og liste af Edge<T>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Node<T>
	{
		// Properties... Generisk data og liste af Edge<T>
		public T Data { get; set; }
		public List<Edge<T>> Edges { get; set; }

		// Constructor der sætter data parameter i Data property
		public Node(T data)
		{
			Data = data;
			Edges = new List<Edge<T>>();
		}

		// Funktion der opretter ny edge med relation til anden node, markeret "other" i parameter og modtages som argument.
		public void AddEdge(Node<T> other)
		{
			// instantieres edge og tilføjes til node objektets liste af edges
			Edges.Add(new Edge<T>(this, other));
		}
	}
}
