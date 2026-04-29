using TaskUnMEP.Models.enums;

namespace TaskUnMEP.Models
{
    public class UserTask
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public int taskId { get; set; }
        public string taskName { get; set; }
        public statusEnum status { get; set; }
        public int minutes { get; set; }
        public DateOnly date { get; set; }

        public override string ToString()
        {
            return $"userId: {userId}, \nuserName: {userName}, " +
                $"\ntaskId: {taskId}, \ntaskName: {taskName},\nstatus: {status}" +
                $"\nminutes: {minutes} \ndate: {date}";
        }

    }
}
