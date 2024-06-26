namespace JEdwards.Domain.Entities
{
    public record SearchQuery(string Query)
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? ErrorMessage { get; set; }
    }

}
