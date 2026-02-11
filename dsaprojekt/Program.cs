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

		static string baseOutputPath = Path.Combine(Environment.CurrentDirectory, "Output");
		static string outputResultPath = Path.Combine(baseOutputPath, "results.json");

		static string[] jsonFiles = { "notSorted.json", "reverseSorted.json", "sorted.json" };
       
		static List<ExportJsonData<int>> results = new List<ExportJsonData<int>>();

		static void Main(string[] args)
        {
			foreach(var file in jsonFiles)
			{
				RunAllAlgorithmsOnJsonFile(file);
			}

			WriteJsonData();

			Graph<string> park = new Graph<string>();

			// Skulle gerne være alle de nodes i opgaven - HUSK AT KIGGE IGENNEM IGEN, FOR AT SIKRE AT DET ER ALLE
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


			Console.WriteLine($"Data of node 'Carousel': {park.GetNode("Carousel").Data}");
			Console.WriteLine("\nAll Edges:\n");
			foreach (var edge in park.GetNode("Carousel").Edges)
			{
				Console.WriteLine(edge.To.Data);
				Console.WriteLine(edge.From.Data);
				Console.WriteLine("\n");
			}

			TestGraphSearch(park, "Entrance", "Volcano Ride");
		}

		static void TestGraphSearch(Graph<string> graph, string start, string goal)
		{
			BFS<string> bfs = new BFS<string>();
			if (bfs.Search(graph.GetNode(start), graph.GetNode(goal)))
			{
				Console.WriteLine("BFS Results:");
				Console.WriteLine($"Visited order:");
				for(int i = 0; i < bfs.VisitedOrder.Count; i++)
				{
					Console.WriteLine(bfs.VisitedOrder[i].Data);
					if(i != bfs.VisitedOrder.Count -1)
					{
						Console.WriteLine("  |  ");
						Console.WriteLine("  |  ");
						Console.WriteLine("  V ");
					}
				}
				Console.WriteLine($"\n\nPath to goal:");
				List<Node<string>> bfsPath = bfs.GetPath(graph.GetNode(start), graph.GetNode(goal));
				for (int i = 0; i < bfsPath.Count; i++)
				{
					Console.WriteLine(bfsPath[i].Data);
					if (i != bfsPath.Count - 1)
					{
						Console.WriteLine("  |  ");
						Console.WriteLine("  |  ");
						Console.WriteLine("  V ");
					}
				}
			}

		}


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
			Console.WriteLine($"Contents of {completeFilePath} have been added to list");
		}

		static T[] DepopulateMyList<T>(MyList<T> myList)
		{
			T[] values = new T[myList.Count];
			for(int i = 0;  i < myList.Count; i++)
			{
				values[i] = myList[i];
			}
			return values;
		}

		static void RunAllAlgorithmsOnJsonFile(string fileName)
		{
			MyList<int> insertionSortedMyList = new MyList<int>();
			MyList<int> bubbleSortedMyList = new MyList<int>();
			MyList<int> quickSortedMyList = new MyList<int>();

			PopulateMyList(insertionSortedMyList, Path.Combine(baseDataPath, fileName));
			PopulateMyList(bubbleSortedMyList, Path.Combine(baseDataPath, fileName));
			PopulateMyList(quickSortedMyList, Path.Combine(baseDataPath, fileName));

			insertionSortedMyList.InsertionSort();
			bubbleSortedMyList.BubbleSort();
			quickSortedMyList.QuickSort();

			results.Add(new ExportJsonData<int> { fileName = fileName, sorting = "Insertion Sort", comparisons = insertionSortedMyList.comparisonCount, elapsedMilliseconds = insertionSortedMyList.elapsedMilliseconds, elapsedNanoseconds = insertionSortedMyList.elapsedNanoseconds, values = DepopulateMyList(insertionSortedMyList) });
			results.Add(new ExportJsonData<int> { fileName = fileName, sorting = "Bubble Sort", comparisons = bubbleSortedMyList.comparisonCount, elapsedMilliseconds = bubbleSortedMyList.elapsedMilliseconds, elapsedNanoseconds = bubbleSortedMyList.elapsedNanoseconds, values = DepopulateMyList(bubbleSortedMyList) });
			results.Add(new ExportJsonData<int> { fileName = fileName, sorting = "Quick Sort", comparisons = quickSortedMyList.comparisonCount, elapsedMilliseconds = quickSortedMyList.elapsedMilliseconds, elapsedNanoseconds = quickSortedMyList.elapsedNanoseconds, values = DepopulateMyList(quickSortedMyList) });
		}

		static void WriteJsonData()
		{
			JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
			var jsonContent = JsonSerializer.Serialize(results, options);

			File.WriteAllText(outputResultPath, jsonContent);
		}

		static void WriteTextFile()
		{
			//
		}
	}

	public class JsonData<T>
	{
		public T[] values { get; set; }
	}

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
