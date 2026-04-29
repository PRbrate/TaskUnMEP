namespace TaskUnMEP.Models
{
    public class TaskData
    {
        public TaskData(int taskId, string taskName, int totalMinutes, string percentage)
        {
            this.taskId = taskId;
            this.taskName = taskName;
            this.totalMinutes = totalMinutes;
            this.percentage = percentage;
        }

        public int taskId { get; set; }
        public string taskName { get; set; }
        public int totalMinutes { get; set; }
        public string percentage { get; set; }
    }
}
