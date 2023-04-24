using D365Demo.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using FluentAssertions.Equivalency.Tracing;
using Dynamitey.Internal.Optimization;

namespace D365Demo.Utilities
{
    
    public class FormatJsonFile
    {

        private readonly Test_Data_Environment_Config? accountsData;
        public void updateJsonFile(String filePath, String jsonNode, String jsonNodeValue)
        {
            Console.WriteLine($"Writing data to filepath - {filePath} for node - {jsonNode} having value as {jsonNodeValue}");
            String jsonString = File.ReadAllText(filePath);
            // Convert the JSON string to a JObject:
            JObject? jObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString) as JObject;
            // Select a nested property using a single string:
            JToken? jToken = jObject!.SelectToken(jsonNode);
            // Update the value of the property: 
            jToken!.Replace(jsonNodeValue);
            // Convert the JObject back to a string:
            String updatedJsonString = jObject.ToString();
            File.WriteAllText(filePath, updatedJsonString);
            Console.WriteLine(updatedJsonString);
        }
        public String DirProject()
        {
            String DirDebug = System.IO.Directory.GetCurrentDirectory();
            String DirProject = DirDebug;

            for (int counter_slash = 0; counter_slash < 3; counter_slash++)
            {
                DirProject = DirProject.Substring(0, DirProject.LastIndexOf(@"\"));
            }

            return DirProject;
        }

        [Test]
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
