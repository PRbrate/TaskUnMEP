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


Dictionary<string, object> dataReturn = new Dictionary<string, object>();

var usersValid = userTasks.Where(u => u.minutes >= 0).ToList();
var totalMinutes = usersValid.Select(u => u.minutes).Sum();
var taskByGroup  = usersValid.GroupBy(u => u.taskId).ToList();






