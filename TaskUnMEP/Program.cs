using Newtonsoft.Json;
using TaskUnMEP.Models;

List<UserTask> userTasks = new();
string path = "../../../data.json";

try
{
    using (StreamReader r = new StreamReader(path))
    {

        string json = r.ReadToEnd();
        userTasks = JsonConvert.DeserializeObject<List<UserTask>>(json);
    }
}
catch (Exception e)
{
    Console.WriteLine("Aquivo não encontrado: " + e.Message);
}




