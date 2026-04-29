namespace TaskUnMEP.Models
{
    public class UserData
    {
        public UserData(int userId, string userName, int totalMinutes, List<int> taskDist)
        {
            this.userId = userId;
            this.userName = userName;
            this.totalMinutes = totalMinutes;
            this.taskDist = taskDist;
        }

        public int userId { get; set; }
        public string userName { get; set; }
        public int totalMinutes { get; set; }
        public List<int> taskDist { private get; set; }

        public List<int> GetTaskDist()
        {
            return taskDist;
        }
    }
}
