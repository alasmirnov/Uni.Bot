namespace Service.Model
{
    public class ActionCommand
    {
        public string Command { get; }

        public ActionCommand(string command)
        {
            Command = command;
        }
    }
}