namespace dsaprojekt.Graphs
{
	public class BFS<T>
	{
		public List<Node<T>> VisitedOrder;

		// Dictionary til holdes styr på hver enkelte Node parent
		//• Stien findes ved at følge parentes fra destinationen tilbage til start
		Dictionary<Node<T>, Node<T>> Parent;

		public BFS()
		{
			VisitedOrder = new List<Node<T>>();
			Parent = new Dictionary<Node<T>, Node<T>>();
		}

		//procedure BFS(Graph, source):
		//create a queue Q
		//enqueue source onto Q
		//mark source
		//while Q is not empty:
		//dequeue an item from Q into v
		//for each edge e incident on v in Graph:
		//let w be the other end of e
		//if w is not marked:
		//mark w
		//enqueue w onto Q


		//Forklaring af algoritmen

		//1. Opret en queue og placer en fiktiv kant gående fra og til start

		//2. Marker startnoden som besøgt

		//3. Fjern og anvend den første kant fra jeres queue

		//4. Hvis en slutnode ønskes fundet testes det om destinationen er
		//slutnoden
		//• Algoritmen afsluttes hvis dette er tilfældet
		//• Stien findes ved at følge parentes fra destinationen tilbage til start

		//5. Hvis slutnoden ikke er fundet tilføjes alle nabokanter med
		//unvisited noder til jeres queue, hvis der ingen noder er
		//backtrackes der.
		//• Noderne markeres som visited.
		//• Parent noden noteres

		//6. Algoritmen gentages fra step 3


		public bool Search(Node<T> start, Node<T> goal)
		{
			//1. Opret en queue og placer en fiktiv kant gående fra og til start
			// Bruger nodes i stedet for edges
			Queue<Node<T>> queue = new Queue<Node<T>>();
			HashSet<Node<T>> visited = new HashSet<Node<T>>();

			queue.Enqueue(start);
			//2. Marker startnoden som besøgt
			visited.Add(start);
			VisitedOrder.Add(start);

			Parent[start] = start;

			if (start.Data.Equals(goal.Data))
			{
				return true;
			}

			while (queue.Count > 0)
			{
				//3. Fjern og anvend den første kant fra jeres queue
				Node<T> current = queue.Dequeue();


				foreach (var edge in current.Edges)
				{
					//5. Hvis slutnoden ikke er fundet tilføjes alle nabokanter med
					//unvisited noder til jeres queue, hvis der ingen noder er
					//backtrackes der.
					//• Noderne markeres som visited.
					//• Parent noden noteres
					if (!visited.Contains(edge.To))
					{
						visited.Add(edge.To);
						VisitedOrder.Add(edge.To);
						Parent[edge.To] = current;
						//4. Hvis en slutnode ønskes fundet testes det om destinationen er
						//slutnoden
						//• Algoritmen afsluttes hvis dette er tilfældet
						//• Stien findes ved at følge parentes fra destinationen tilbage til start
						if (edge.To.Data.Equals(goal.Data))
						{
							return true;
						}
						queue.Enqueue(edge.To);
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
