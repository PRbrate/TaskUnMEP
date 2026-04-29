namespace TaskUnMEP.Models
{
    public class UserData
    {
        public UserData(int userId, string userName, int totalMinutes, List<int> taskDist, int taskQuant)
        {
            this.userId = userId;
            this.userName = userName;
            this.totalMinutes = totalMinutes;
            this.taskDist = taskDist;
            this.taskQuant = taskQuant;
        }

        public int userId { get; set; }
        public string userName { get; set; }
        public int totalMinutes { get; set; }
        public List<int> taskDist { private get; set; }
        public int taskQuant { private get; set; }

        public List<int> GetTaskDist()
        {
            return taskDist;
        }

        public int GetTaskQuant()
        {
            return taskQuant;
        }
    }
}
