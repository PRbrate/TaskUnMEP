namespace TaskUnMEP.Models
{
    public class UserData
    {
        public UserData(int userId, string userName, int totalMinutes)
        {
            this.userId = userId;
            this.userName = userName;
            this.totalMinutes = totalMinutes;
        }

        public int userId { get; set; }
        public string userName { get; set; }
        public int totalMinutes { get; set; }
    }
}
