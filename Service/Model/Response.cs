namespace Service.Model
{
    public class Response
    {
        public string Text { get; }

        public Response(string text)
        {
            Text = text;
        }
    }
}