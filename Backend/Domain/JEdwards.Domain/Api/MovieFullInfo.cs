namespace JEdwards.Domain.Api
{
    public record MovieFullInfo(string Title, string Year, string ImdbID, string Type, string Poster, string Rated, string Released, string Runtime, string Genre, string Director, string Writer, string Actors, string Plot, string Language, string Country, string Awards, List<Rating> Ratings, int Metascore, float ImdbRating, string ImdbVotes, string DVD, string BoxOffice, string Production, string Website, string Response) : Movie(Title, Year, ImdbID, Type, Poster);
}
