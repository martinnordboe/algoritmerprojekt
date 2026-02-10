using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text.Json;

namespace dsaprojekt
{
    internal class Program
    {
        static string basePath = Path.Combine(Environment.CurrentDirectory, "Data");
        static string[] jsonFiles = { "notSorted.json", "reverseSorted.json", "sorted.json" };
        
		static MyList<int> myListOfNotSortedJson = new MyList<int>();
		static MyList<int> myListOfReverseSortedJson = new MyList<int>();
		static MyList<int> myListOfSortedJson = new MyList<int>();

		static void Main(string[] args)
        {

			PopulateMyList(myListOfNotSortedJson, Path.Combine(basePath, "notSorted.json"));
			PopulateMyList(myListOfReverseSortedJson, Path.Combine(basePath, "reverseSorted.json"));
			PopulateMyList(myListOfSortedJson, Path.Combine(basePath, "sorted.json"));
		}

        static void PopulateMyList<T>(MyList<T> myList, string completeFilePathy)
        {
			string jsonContent = File.ReadAllText(completeFilePathy);

			JsonSerializerOptions options = new JsonSerializerOptions();
			var data = JsonSerializer.Deserialize<JsonData<T>>(jsonContent);

			if (data != null)
			{
				foreach (var item in data.values)
				{
					myList.Add(item);
				}
			}
			Console.WriteLine($"Contents of {completeFilePathy} have been added to list");
		}
	}

	public class JsonData<T>
	{
		public T[] values { get; set; }
	}
}
