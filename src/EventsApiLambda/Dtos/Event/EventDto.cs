namespace api.Dtos.Event
{
    using api.Dtos.Artist;

    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Descripton { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Localization { get; set; } = string.Empty;
        public List<ArtistDto>? Artists {  get; set; }
        public int NormalPrice { get; set; }
        public int VipPrice { get; set; }
    }
}
