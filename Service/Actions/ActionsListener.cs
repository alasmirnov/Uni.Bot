using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.Configuration;
using Service.Model;

namespace Service.Actions
{
    public class ActionsListener : IHostedService
    {
        private static readonly Response NotAuthorizedResponse = Response.Message("You are not authorized");
        
        private readonly ActionsBot _bot;
        private readonly int _allowedId;
        private readonly string _allowedUserName;
        private readonly CancellationTokenSource _cts;
        private readonly ILogger<ActionsListener> _logger;

        private Task _loopTask;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Listen();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cts.Cancel();
            _cts.Dispose();

            _loopTask.Wait(cancellationToken);
            return Task.CompletedTask;
        }

        public ActionsListener(IServiceProvider serviceProvider)
        {
            TelegramConfig telegramConfig = serviceProvider.GetService<IOptions<TelegramConfig>>().Value;

            _bot = new ActionsBot(telegramConfig.Token);
            _allowedId = telegramConfig.UserId;
            _allowedUserName = telegramConfig.Username;

            _logger = serviceProvider.GetService<ILogger<ActionsListener>>();
            
            _cts = new CancellationTokenSource();
        }

        private void Listen()
        {
            if (_loopTask != null)
                throw new Exception("Listener has already been started");
            
            _loopTask = Task.Run(Loop, _cts.Token);
        }

        private async Task Loop()
        {
            CancellationToken token = _cts.Token;

            while (true)
            {
                await SafeProcessAsync(token);
                
                if (token.IsCancellationRequested)
                    break;
            }
        }

        private async Task SafeProcessAsync(CancellationToken token)
        {
            try
            {
                IReadOnlyList<ActionCommand> commands = await _bot.GetNewCommandsAsync(token);

                foreach (ActionCommand command in commands)
                {
                    if (AuthorizeUser(command.User))
                    {
                        string result = await ProcessAsync(command);
                        await _bot.SendAsync(Response.Message(result), command.ChatId, token);
                    }
                    else
                    {
                        await _bot.SendAsync(NotAuthorizedResponse, command.ChatId, token);
                    }
                }

                token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                _logger.LogDebug("Listener has been stopped");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to process actions");
            }
        }

        private bool AuthorizeUser(User user)
        {
            return user.Id == _allowedId && user.Name.EqualsNoCase(_allowedUserName);
        }

        private static async Task<string> ProcessAsync(ActionCommand command)
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