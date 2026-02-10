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
        
		static MyList<int> myListOfNotSortedJson = new MyList<int>();
		static MyList<int> myListOfReverseSortedJson = new MyList<int>();
		static MyList<int> myListOfSortedJson = new MyList<int>();

		static void Main(string[] args)
        {
			PopulateMyList(myListOfNotSortedJson, Path.Combine(baseDataPath, "notSorted.json"));
			PopulateMyList(myListOfReverseSortedJson, Path.Combine(baseDataPath, "reverseSorted.json"));
			PopulateMyList(myListOfSortedJson, Path.Combine(baseDataPath, "sorted.json"));

			Console.WriteLine("Insertion Sorting");
			myListOfNotSortedJson.InsertionSort();
			Console.WriteLine("\n\n");
			myListOfReverseSortedJson.BubbleSort();
			Console.WriteLine("Bubble Sorting");
			Console.WriteLine("\n\n");
			Console.WriteLine("Quick Sorting");
			myListOfSortedJson.QuickSort();
			Console.WriteLine("\n\n");

			Console.WriteLine("\n\n");
			Console.WriteLine("SORTED!");
			Console.WriteLine("\n");


			Console.WriteLine("FROM THE JSON 'notSorted.json\n");
			foreach (var item in myListOfNotSortedJson)
			{
				Console.WriteLine(item);
			}
			Console.WriteLine($"\n----Performance----\nComparisons: {myListOfNotSortedJson.comparisonCount}\nTime elapsed: {myListOfNotSortedJson.elapsedMilliseconds} ms\nTime elapsed: {myListOfNotSortedJson.elapsedNanoseconds} ns");
			Console.WriteLine("\n\n");

			WriteJsonData();
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

		static void WriteJsonData()
		{
			List<ExportJsonData<int>> results = new List<ExportJsonData<int>>()
			{
				new ExportJsonData<int> { fileName = "notSorted.json", values = DepopulateMyList(myListOfNotSortedJson) },
				new ExportJsonData<int> { fileName = "reverseSorted.json", values = DepopulateMyList(myListOfReverseSortedJson) },
				new ExportJsonData<int> { fileName = "sorted.json", values = DepopulateMyList(myListOfSortedJson) }
			};

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

        public T[] values { get; set; }
    }
}
