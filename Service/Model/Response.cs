namespace Service.Model
{
    public class Response
    {
        public string Text { get; }

        private Response(string text)
        {
            Text = text;
        }

        public static Response Message(string message) => new Response(message);
    }
}