namespace LudotecaApi.Models
{
    public class GameDetails
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int YearPublished { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public string Thumbnail { get; set; } = "";
    }

}