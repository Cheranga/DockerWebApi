namespace Docker.WebApi.DTO
{
    public class TriviaResponse
    {
        public string Text { get; set; }
        public int Number { get; set; }
        public bool Found { get; set; }
        public string Type { get; set; }
    }
}