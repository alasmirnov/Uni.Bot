namespace Service.Model
{
    public class ActionCommand
    {
        public string Username { get; }
        public int UserId { get; }
        public int ChatId { get; }
        public string Command { get; }

        public ActionCommand(string command, string username, int userId, int chatId)
        {
            Command = command;
            Username = username;
            UserId = userId;
            ChatId = chatId;
        }
    }
}