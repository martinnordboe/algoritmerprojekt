namespace dsaprojekt.Graphs
{
	public class DFS<T>
	{
		public List<Node<T>> VisitedOrder;

		// Dictionary til holdes styr på hver enkelte Node parent
		//• Stien findes ved at følge parentes fra destinationen tilbage til start
		Dictionary<Node<T>, Node<T>> Parent;

		public DFS()
		{
			VisitedOrder = new List<Node<T>>();
			Parent = new Dictionary<Node<T>, Node<T>>();
		}


		// Pseudo kode fra undervisning

		// procedure DFS-iterative(G,v):
		// let S be a stack
		// S.push(v)
		//while S is not empty
		//v ← S.pop()
		//if v is not labeled as discovered:
		//label v as discovered
		//for all edges from v to w in G.adjacentEdges(v) do
		//S.push(w)


		//Forklaring af algoritmen

		//1. Opret en stack og placer en fiktiv kant gående fra og til start

		//2. Fjern og anvend top kanten fra jeres stack

		//3. Noter parent til noden, start er parent til sig selv(kan også
		//gøres i step 6, dette kan være lettere)

		//4. Noter destinationsnoden som besøgt

		//5. Hvis en slutnode ønskes fundet testes det om destinationen er
		//slutnoden
		//• Algoritmen afsluttes hvis dette er tilfældet
		//• Stien findes ved at følge parentes fra destinationen tilbage til start

		//6. Hvis slutnoden ikke er fundet tilføjes alle nabokanter med
		//ubesøgte noder til stacken, hvis der ingen noder er backtrackes
		//der

		//7. Algoritmen gentages fra punkt 2 til målet er fundet


		public bool Search(Node<T> start, Node<T> goal)
		{
			//1. Opret en stack og placer en fiktiv kant gående fra og til start
			// Bruger nodes i stedet for edges
			Stack<Node<T>> stack = new Stack<Node<T>>();
			HashSet<Node<T>> visited = new HashSet<Node<T>>();

			stack.Push(start);
			//3. Noter parent til noden, start er parent til sig selv(kan også
			//gøres i step 6, dette kan være lettere)
			Parent[start] = start;

			while (stack.Count > 0)
			{
				//2. Fjern og anvend top kanten fra jeres stack
				Node<T> current = stack.Pop();

				// Tjek om allerede besøgt, hvis ja, så spring over
				if (visited.Contains(current))
				{
					continue;
				}

				//4. Noter destinationsnoden som besøgt
				visited.Add(current);
				VisitedOrder.Add(current);

				//5. Hvis en slutnode ønskes fundet testes det om destinationen er
				//slutnoden
				if (current.Equals(goal))
				{
					return true;
				}

				//6. Hvis slutnoden ikke er fundet tilføjes alle nabokanter med
				//ubesøgte noder til stacken, hvis der ingen noder er backtrackes
				//der
				foreach (var edge in current.Edges)
				{
					if (!visited.Contains(edge.To))
					{
						stack.Push(edge.To);
						if (!Parent.ContainsKey(edge.To))
						{
							Parent[edge.To] = current;
						}
					}
				}
			}

			return false;
		}

		// Backtracker fra goal node til start node, ved at bruge Parent dictionary. Dette er den smartere metode, i stedet for at sætte Parent property.
		public List<Node<T>> GetPath(Node<T> start, Node<T> goal)
		{
			List<Node<T>> path = new List<Node<T>>();
			// Vi starter med goal og arbejder os baglæns ved hjælp af parent dictionary
			Node<T> current = goal;

			// Så længe at nuværende valgte node ikke er vores start node, så fortsætter vi op gennem parents fra goal node.
			while (!current.Equals(start))
			{
				path.Add(current);
				current = Parent[current];
			}
			// Når pathen er fundet og nuværende node er samme som start node, så tilføjes start node og listen vendes, så en korrekt vej vises.
			path.Add(start);
			path.Reverse();

			return path;
		}
	}
}
