using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Service.Model;

namespace Service.Actions
{
    public class ActionsListener
    {
        private static readonly Response NotAuthorizedResponse = Response.Message("You are not authorized");
        
        private readonly ActionsBot _bot;
        private readonly int _allowedId;
        private readonly string _allowedUserName;

        private Task _loopTask;
        private CancellationToken _token;

        public ActionsListener(ActionsBot bot, int allowedId, string allowedUserName)
        {
            _bot = bot;
            _allowedId = allowedId;
            _allowedUserName = allowedUserName;
        }

        public void Listen(CancellationToken cancellationToken)
        {
            if (_loopTask != null)
                throw new Exception("Listener has already been started");
            
            _loopTask = Task.Run(Loop, cancellationToken);
            _token = cancellationToken;
        }

        private async Task Loop()
        {
            while (true)
            {
                await SafeProcessAsync();
                
                if (_token.IsCancellationRequested)
                    break;
            }
        }

        private async Task SafeProcessAsync()
        {
            try
            {
                IReadOnlyList<ActionCommand> commands = await _bot.GetNewCommandsAsync(_token);

                foreach (ActionCommand command in commands)
                {
                    if (AuthorizeUser(command.User))
                    {
                        string result = await ProcessAsync(command);
                        await _bot.SendAsync(Response.Message(result), command.ChatId, _token);
                    }
                    else
                    {
                        await _bot.SendAsync(NotAuthorizedResponse, command.ChatId, _token);
                    }
                }

                _token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                // suppressed
            }
            catch
            {
                // TODO: log it
            }
        }

        private bool AuthorizeUser(User user)
        {
            return user.Id == _allowedId && user.Name.EqualsNoCase(_allowedUserName);
        }

        private async Task<string> ProcessAsync(ActionCommand command)
        {
            string[] parts = command.Command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.IsEmpty())
                throw new Exception("Command cannot be empty");

            return parts[0] switch
            {
                "/start" => $"Hello, {command.User.Name}",
                _ => $"Unexpected command '{parts[0]}'"
            };
        }
    }
}