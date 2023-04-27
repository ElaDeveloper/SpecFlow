using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpecFlowDemo.TestData;

namespace SpecFlowDemo.Utilities
{

    public class FormatJsonFile
    {

        private readonly Test_Data_Environment_Config? accountsData;
        public void updateJsonFile(string filePath, string jsonNode, string jsonNodeValue)
        {
            Console.WriteLine($"Writing data to filepath - {filePath} for node - {jsonNode} having value as {jsonNodeValue}");
            string jsonString = File.ReadAllText(filePath);
            // Convert the JSON string to a JObject:
            JObject? jObject = JsonConvert.DeserializeObject(jsonString) as JObject;
            // Select a nested property using a single string:
            JToken? jToken = jObject!.SelectToken(jsonNode);
            // Update the value of the property: 
            jToken!.Replace(jsonNodeValue);
            // Convert the JObject back to a string:
            string updatedJsonString = jObject.ToString();
            File.WriteAllText(filePath, updatedJsonString);
            Console.WriteLine(updatedJsonString);
        }
        public string DirProject()
        {
            string DirDebug = Directory.GetCurrentDirectory();
            string DirProject = DirDebug;

            for (int counter_slash = 0; counter_slash < 3; counter_slash++)
            {
                DirProject = DirProject.Substring(0, DirProject.LastIndexOf(@"\"));
            }

            return DirProject;
        }

        //[Test]
        public void Test()
        {
            //updateJsonFile("C:\\Ela\\PlayWright\\SpecFlowLearn\\TestData\\JsonFiles\\Cases.json", "CaseId", "123456789");
            string text = File.ReadAllText("C:\\Ela\\PlayWright\\SpecFlowLearn\\TestData\\JsonFiles\\EnvironmentConfiguration.json");
            //accountsData! = System.Text.Json.JsonSerializer.Deserialize<Test_Data_Environment_Config>(text);
            using (StreamReader file = File.OpenText("C:\\Ela\\PlayWright\\SpecFlowLearn\\TestData\\JsonFiles\\EnvironmentConfiguration.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                foreach (JProperty property in o2.Properties())
                {
                    //Console.WriteLine(property.Name + " - " + property.Value);
                    //Console.WriteLine(property.Next);
                }
                Console.WriteLine(o2.GetValue("configKeys"));
            }
        }

    }
}
