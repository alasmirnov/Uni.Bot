using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Model;
#pragma warning disable 1998

namespace Service.Actions
{
    public class ActionsBot
    {
        private readonly string _token;

        public async Task<IReadOnlyList<ActionCommand>> GetNewCommandsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SendAsync(Response response)
        {
            throw new NotImplementedException();
        }
        
        public ActionsBot(string token)
        {
            _token = token;
        }
    }
}