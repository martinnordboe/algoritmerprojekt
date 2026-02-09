using System.Text.Json;

namespace dsaprojekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string basePath = Path.Combine(Environment.CurrentDirectory, "Data");
            string[] jsonFiles = { "notSorted.json", "reverseSorted.json", "sorted.json" };

            string filePath = Path.Combine(basePath, jsonFiles[0]);
            string jsonContent = File.ReadAllText(filePath);

            //Console.WriteLine(jsonContent);


            JsonSerializerOptions options = new JsonSerializerOptions();
            var data = JsonSerializer.Deserialize<JsonData>(jsonContent);

            if(data != null)
            {
                foreach(var item in data.values)
                {
                    Console.WriteLine(item);
                }
            }
        }

    }

	public class JsonData
	{
        public int[] values { get; set; }
	}
}
