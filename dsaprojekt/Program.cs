using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text.Json;
using dsaprojekt.Graphs;

namespace dsaprojekt
{
    internal class Program
    {
        static string baseDataPath = Path.Combine(AppContext.BaseDirectory, "Data");

		static string baseOutputPath = Path.Combine(AppContext.BaseDirectory, "Output");
		static string outputResultPath = Path.Combine(baseOutputPath, "results.json");

		static string[] jsonFiles = { "notSorted.json", "reverseSorted.json", "sorted.json" };
       
		static List<ExportJsonData<int>> results = new List<ExportJsonData<int>>();

		static void Main(string[] args)
        {
			// Her looper jeg alle filerne igennem og sikrer at hver fil / datasæt bliver brugt med hver sorteringsalgoritme.
			foreach(var file in jsonFiles)
			{
				RunAllAlgorithmsOnJsonFile(file);
			}

			// skriver resultater til results.json
			WriteJsonData();


			// starter på grafer, DFS og BFS her, hvor jeg instantierer en graph, tilføjer nodes og laver edges til nodes.
			Graph<string> park = new Graph<string>();

			park.AddNode("Entrance");

			park.AddNode("Carousel");
			park.AddNode("Mini Train");
			park.AddNode("Ice Cream");

			park.AddNode("Roller Coaster");
			park.AddNode("Haunted House");
			park.AddNode("Water Ride");
			park.AddNode("Pirate Ship");

			park.AddNode("Climbing Tower");

			park.AddNode("Volcano Ride");



			park.AddEdge("Entrance", "Carousel");
			park.AddEdge("Entrance", "Mini Train");
			park.AddEdge("Entrance", "Ice Cream");

			park.AddEdge("Carousel", "Haunted House");
			park.AddEdge("Carousel", "Roller Coaster");

			park.AddEdge("Roller Coaster", "Climbing Tower");

			park.AddEdge("Climbing Tower", "Volcano Ride");


			park.AddEdge("Mini Train", "Water Ride");
			
			park.AddEdge("Ice Cream", "Pirate Ship");

			// Her kalder jeg så funktionen der kører BFS og DFS på grafens noder og printer alt dataen ud til opgaven, for begge mål.
			Console.WriteLine("-----------------------------------------------------------");
			Console.WriteLine("Mål A:");
			TestGraphSearch(park, "Entrance", "Water Ride");
			Console.WriteLine("Mål B:");
			TestGraphSearch(park, "Entrance", "Volcano Ride");
		}

		/// <summary>
		///		Instantierer BFS og DFS og finder path fra start node til goal node, med både BFS og DFS. Alt printes til Console.
		/// </summary>
		/// <param name="graph"></param>
		/// <param name="start"></param>
		/// <param name="goal"></param>
		static void TestGraphSearch(Graph<string> graph, string start, string goal)
		{
			Console.WriteLine("-----------------------------------------------------------");
			Console.WriteLine($"Start node:   {start}");
			Console.WriteLine($"Goal node:    {goal}");

			BFS<string> bfs = new BFS<string>();
			if (bfs.Search(graph.GetNode(start), graph.GetNode(goal)))
			{
				Console.WriteLine("\n///////////////////////////////////////////");
				Console.WriteLine("########### BFS #############:");
				Console.WriteLine($"Visited amount of nodes {bfs.VisitedOrder.Count}. Visited order:\n");
				for(int i = 0; i < bfs.VisitedOrder.Count; i++)
				{
					Console.Write(bfs.VisitedOrder[i].Data);
					if(i != bfs.VisitedOrder.Count -1)
					{
						Console.Write(" --> ");
					}
				}
				Console.WriteLine("\n\n///////////////////////////////////////////");
				List<Node<string>> bfsPath = bfs.GetPath(graph.GetNode(start), graph.GetNode(goal));
				Console.WriteLine($"\nPath to goal - {bfsPath.Count} nodes, {bfsPath.Count - 1} edges\n");
				for (int i = 0; i < bfsPath.Count; i++)
				{
					Console.Write(bfsPath[i].Data);
					if (i != bfsPath.Count - 1)
					{
						Console.Write(" --> ");
					}
				}
				Console.WriteLine("\n\n///////////////////////////////////////////");
			}


			DFS<string> dfs = new DFS<string>();
			if (dfs.Search(graph.GetNode(start), graph.GetNode(goal)))
			{
				Console.WriteLine("########### DFS #############:");
				Console.WriteLine($"Visited amount of nodes {dfs.VisitedOrder.Count}. Visited order:\n");
				for (int i = 0; i < dfs.VisitedOrder.Count; i++)
				{
					Console.Write(dfs.VisitedOrder[i].Data);
					if (i != dfs.VisitedOrder.Count - 1)
					{
						Console.Write(" --> ");
					}
				}
				Console.WriteLine("\n\n///////////////////////////////////////////");
				List<Node<string>> dfsPath = dfs.GetPath(graph.GetNode(start), graph.GetNode(goal));
				Console.WriteLine($"\nPath to goal - {dfsPath.Count} nodes, {dfsPath.Count - 1} edges\n");
				for (int i = 0; i < dfsPath.Count; i++)
				{
					Console.Write(dfsPath[i].Data);
					if (i != dfsPath.Count - 1)
					{
						Console.Write(" --> ");
					}
				}
				Console.WriteLine("\n\n///////////////////////////////////////////");
			}
			Console.WriteLine("\n\n\n\n\n\n-----------------------------------------------------------");
		}

		/// <summary>
		/// Bruges til at konvertere fra T[] array til MyList, da der ikke kan deserializes direkte til MyList
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="myList"></param>
		/// <param name="completeFilePath"></param>
		static void PopulateMyList<T>(MyList<T> myList, string completeFilePath)
        {
			string jsonContent = File.ReadAllText(completeFilePath);

			JsonSerializerOptions options = new JsonSerializerOptions();
			var data = JsonSerializer.Deserialize<JsonData<T>>(jsonContent);

			if (data != null)
			{
				foreach (var item in data.values)
				{
					myList.Add(item);
				}
			}
		}

		/// <summary>
		/// Bruges til at konvertere fra MyList til en T[] array, da jeg ikke kan JSON serialize med  MyList
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="myList"></param>
		/// <returns></returns>
		static T[] DepopulateMyList<T>(MyList<T> myList)
		{
			T[] values = new T[myList.Count];
			for(int i = 0;  i < myList.Count; i++)
			{
				values[i] = myList[i];
			}
			return values;
		}

		/// <summary>
		/// Instantierer 4 MyList, kalder PopulateMyList som får values array fra JSON deserialized, loopet over i mine MyList, da JsonSerializer ikke kan deserialize til MyList instanser.
		/// Laver forskellig sortering på hver af listerne.
		/// Instantierer nye ExportJsonData objekter og tilføjer performance målinger dem, samt tilføjer objekterne til listen results.
		/// </summary>
		/// <param name="fileName"></param>
		static void RunAllAlgorithmsOnJsonFile(string fileName)
		{
			MyList<int> insertionSortedMyList = new MyList<int>();
			MyList<int> bubbleSortedMyList = new MyList<int>();
			MyList<int> quickSortedMyList = new MyList<int>();
			MyList<int> middleQuickSortedMyList = new MyList<int>();

			PopulateMyList(insertionSortedMyList, Path.Combine(baseDataPath, fileName));
			PopulateMyList(bubbleSortedMyList, Path.Combine(baseDataPath, fileName));
			PopulateMyList(quickSortedMyList, Path.Combine(baseDataPath, fileName));
			PopulateMyList(middleQuickSortedMyList, Path.Combine(baseDataPath, fileName));

			insertionSortedMyList.InsertionSort();
			bubbleSortedMyList.BubbleSort();
			quickSortedMyList.QuickSort();
			middleQuickSortedMyList.QuickSortMiddlePivot();

			results.Add(new ExportJsonData<int> { fileName = fileName, sorting = "Insertion Sort", comparisons = insertionSortedMyList.comparisonCount, elapsedMilliseconds = insertionSortedMyList.elapsedMilliseconds, elapsedNanoseconds = insertionSortedMyList.elapsedNanoseconds, values = DepopulateMyList(insertionSortedMyList) });
			results.Add(new ExportJsonData<int> { fileName = fileName, sorting = "Bubble Sort", comparisons = bubbleSortedMyList.comparisonCount, elapsedMilliseconds = bubbleSortedMyList.elapsedMilliseconds, elapsedNanoseconds = bubbleSortedMyList.elapsedNanoseconds, values = DepopulateMyList(bubbleSortedMyList) });
			results.Add(new ExportJsonData<int> { fileName = fileName, sorting = "Quick Sort", comparisons = quickSortedMyList.comparisonCount, elapsedMilliseconds = quickSortedMyList.elapsedMilliseconds, elapsedNanoseconds = quickSortedMyList.elapsedNanoseconds, values = DepopulateMyList(quickSortedMyList) });
			results.Add(new ExportJsonData<int> { fileName = fileName, sorting = "Quick Sort Middle Pivot", comparisons = middleQuickSortedMyList.comparisonCount, elapsedMilliseconds = middleQuickSortedMyList.elapsedMilliseconds, elapsedNanoseconds = middleQuickSortedMyList.elapsedNanoseconds, values = DepopulateMyList(middleQuickSortedMyList) });
		}

		/// <summary>
		/// Gemmer JSON til JSON fil
		/// </summary>
		static void WriteJsonData()
		{
			File.WriteAllText(outputResultPath, SerializeToJson());
		}

		/// <summary>
		/// Serializer results (listen af ExportJsonData objekter) til JSON og returner JSON string
		/// </summary>
		/// <returns></returns>
		static string SerializeToJson() 
		{
			JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
			return JsonSerializer.Serialize(results, options);
		}
	}

	/// <summary>
	///		Klasse med property values til deserializing af JSON til array der kan bruges til instantiering af MyList med rigtig data.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class JsonData<T>
	{
		public T[] values { get; set; }
	}

	/// <summary>
	///		Klasse med properties til serializing af performance dataen til JSON, så der kan gemmes i results.json filen, som krav i opgaven
	/// </summary>
	/// <typeparam name="T"></typeparam>
    public class ExportJsonData<T>
    {
		public string fileName { get ; set; }
		public string sorting { get; set; }
		public int comparisons { get; set; }
		public double elapsedMilliseconds { get; set; }
		public double elapsedNanoseconds { get; set; }
        public T[] values { get; set; }
    }
}
