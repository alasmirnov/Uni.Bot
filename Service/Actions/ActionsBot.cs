using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Service.Dto;
using Service.Model;

namespace Service.Actions
{
    public class ActionsBot
    {
        private readonly string _token;

        private int _offset;

        public async Task<IReadOnlyList<ActionCommand>> GetNewCommandsAsync(CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>
            {
                {"offset", _offset.ToString()},
                {"timeout", "25"}
            };

            string url = UrlBuilder.BuildUrl("getUpdates", _token, parameters);

            var result = new List<ActionCommand>();

            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url, cancellationToken);

                responseMessage.EnsureSuccessStatusCode();

                string responseJson = await responseMessage.Content.ReadAsStringAsync();

                var response = JsonSerializer.Deserialize<GetUpdatesResponse>(responseJson);
                
                response.CheckException();

                foreach (UpdateDto update in response.Updates)
                {
                    var user = new User(update.Message.User.Id, update.Message.User.Name);
                    
                    result.Add(new ActionCommand(update.Message.Text, user, user.Id));

                    if (update.Id > _offset)
                        _offset = update.Id;
                }
            }

            return result;
        }

        public async Task SendAsync(Response response, int chatId, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>
            {
                {"chat_id", chatId.ToString()}, 
                {"text", response.Text}
            };
            
            string url = UrlBuilder.BuildUrl("sendMessage", _token, parameters);

            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url, cancellationToken);

                string resp = await responseMessage.Content.ReadAsStringAsync();

                var telegramResponse = JsonSerializer.Deserialize<SendMessageResponse>(resp);
                
                telegramResponse.CheckException();
            }
        }
        
        public ActionsBot(string token)
        {
            _offset = 0;
            _token = token;
        }
    }
}