using Newtonsoft.Json;
using System.Globalization;
using TaskUnMEP.Models;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

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
var taskByGroup = usersValid.GroupBy(u => u.taskId).ToList();

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

var moreWork = taskToReturn.MaxBy(t => t.totalMinutes);


var top3TasksPercentage = taskToReturn
    .OrderByDescending(t => t.totalMinutes)
    .Select(t => new
    {
        taskId = t.taskId,
        taskName = t.taskName,
        percentage = t.percentage
    })
    .Take(3)
    .ToList();
var employee = usersValid.GroupBy(u => u.userId).ToList();
var employeeReturn = new List<UserData>();

foreach (var emp in employee)
{
    List<int> distTask = emp.OrderBy(e => e.taskId).Select(e => e.taskId).Distinct().ToList();
    var id = emp.Select(u => u.userId).First();
    var name = emp.Select(u => u.userName).First();
    int totalEmpMinutes = emp.Select(u => u.minutes).Sum();
    employeeReturn.Add(new UserData(id, name, totalEmpMinutes, distTask));
}

var top3Employees = employeeReturn
    .OrderByDescending(t => t.totalMinutes)
    .Take(3)
    .ToList();

var mostDistinctUserOnTasks = employeeReturn.OrderBy(e => e.userId).OrderByDescending(e => e.GetTaskDist().Count()).First();


Dictionary<string, object> dataReturn = new Dictionary<string, object>();

dataReturn["totalMinutes"] = totalMinutes;
dataReturn["tasks"] = taskToReturn.OrderByDescending(t => t.totalMinutes);
dataReturn["mostWorkedTask"] = moreWork;
dataReturn["top3TasksPercentage"] = top3TasksPercentage;
dataReturn["top3Employees"] = top3Employees;
dataReturn["mostDistinctUserOnTasks"] = new
{
    userId = mostDistinctUserOnTasks.userId,
    userName = mostDistinctUserOnTasks.userName,
    distinctTasks = mostDistinctUserOnTasks.GetTaskDist().Count(),
    taskIds = mostDistinctUserOnTasks.GetTaskDist()
};
dataReturn["ignoredRecords"] = userTasks.Count() - usersValid.Count();

string jsonReturn = JsonConvert.SerializeObject(dataReturn, Formatting.Indented);
File.WriteAllText("tasks.json", jsonReturn);