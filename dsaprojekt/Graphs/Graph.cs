using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace dsaprojekt.Graphs
{
	public class Graph<T>
	{
		// Dictionary som collection af nodes. Gør det muligt med O(1) at finde Node med dataen der søges efter.
		// Dette er som alternativ til Find, da Find er O(n) - som skrevet i rapport burde AddDirectedEdge og AddEdge være Node<T> i stedet for T.
		// Men følger materialets UML
		private Dictionary<T, Node<T>> nodes;

		// En constructor...
		public Graph()
		{
			nodes = new Dictionary<T, Node<T>>();
		}

		// Hvis der ikke allerede findes en Node med den value, så instantieres ny Node og lægges i Dictionary
		public void AddNode(T value)
		{
			if (!nodes.ContainsKey(value))
			{
				nodes[value] = new Node<T>(value);
			}
		}

		// Igen, burde være Node<T> i stedet, da det her umuliggør Nodes med lignende data. Men for opgavens og eksemplets skyld, følger jeg materialet og videoerne
		public void AddDirectedEdge(T from, T to)
		{
			// Hmmmm... får kun en to values og ikke to Nodes.
			// Måske sammenlign med Data i Node elementer i listen?

			// Fixet med Dictionary
			if (nodes.ContainsKey(from) && nodes.ContainsKey(to))
			{
				nodes[from].AddEdge(nodes[to]);
			}
		}

		// Igen, burde være Node<T> i stedet, da det her umuliggør Nodes med lignende data. Men for opgavens og eksemplets skyld, følger jeg materialet og videoerne
		public void AddEdge(T from, T to)
		{
			AddDirectedEdge(from, to);
			AddDirectedEdge(to, from);
		}

		public Node<T> GetNode(T value)
		{
			if (nodes.ContainsKey(value))
			{
				return nodes[value];
			}
			else
			{
				return null;
			}
		}

	}
}
