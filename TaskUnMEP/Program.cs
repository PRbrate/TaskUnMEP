using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
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


var usersValid = userTasks.Where(u => u.minutes >= 0).ToList();
var totalMinutes = usersValid.Select(u => u.minutes).Sum();
var taskByGroup  = usersValid.GroupBy(u => u.taskId).ToList();

var taskToReturn = new List<TaskData>();

foreach (var group in taskByGroup)
{

    var totalByTask = group.Sum(v => v.minutes);
    int id = group.Select(u => u.taskId).First();
    string name = group.Select(u => u.taskName).First();
    int sum = group.Select(u => u.minutes).Sum();

    double percentage = (double)totalByTask / totalMinutes * 100;

    string percentageReturn = $"{percentage:F2}%";

    var task = new TaskData(id, name, sum, percentageReturn);
    taskToReturn.Add(task);
}

var moreWork = taskByGroup.MaxBy(t => t.Select(u => u.minutes).Sum());

var top3TasksPercentage = taskToReturn
    .OrderByDescending(t => t.totalMinutes)
    .Take(3)
    .ToList();

var employee = usersValid.GroupBy(u => u.userId).ToList();
var employeeReturn = new List<UserData>();

foreach(var emp in employee)
{
    var quantTask = emp.Select(e => e.taskId).Distinct().Count();
    List<int> distTask = emp.OrderBy(e => e.taskId).Select(e => e.taskId).Distinct().ToList();  
    var id = emp.Select(u => u.userId).First();
    var name = emp.Select(u => u.userName).First();
    int totalEmpMinutes = emp.Select(u => u.minutes).Sum();
    employeeReturn.Add(new UserData(id, name, totalEmpMinutes, distTask, quantTask));
}

var top3Employees = employeeReturn
    .OrderByDescending(t => t.totalMinutes)
    .Take(3)
    .ToList();

var mostDistinctUserOnTasks = employeeReturn.OrderBy(e => e.userId).OrderByDescending(e => e.GetTaskQuant()).First();


