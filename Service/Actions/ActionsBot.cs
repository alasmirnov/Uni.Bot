using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Service.Model;
#pragma warning disable 1998

namespace Service.Actions
{
    public class ActionsBot
    {
        private readonly string _token;

        public async Task<IReadOnlyList<ActionCommand>> GetNewCommandsAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SendAsync(Response response, int chatId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        
        public ActionsBot(string token)
        {
            _token = token;
        }
    }
}