namespace JEdwards.Domain
{
    public class Movie
    {
        public string Title { get; init; }
        public string Year { get; init; }
        public string ImdbID { get; init; } // can`t be int 
        public string Type { get; init; }
        public string Poster { get; init; }
    }
}
