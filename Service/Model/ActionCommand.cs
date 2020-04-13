namespace Service.Model
{
    public class ActionCommand
    {
        public User User { get; }
        public int ChatId { get; }
        public string Command { get; }

        public ActionCommand(string command, User user, int chatId)
        {
            Command = command;
            ChatId = chatId;
            User = user;
        }
    }
}