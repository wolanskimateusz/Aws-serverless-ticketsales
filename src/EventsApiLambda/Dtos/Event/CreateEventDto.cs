namespace api.Dtos.Event
{
    using api.Models;
    public class CreateEventDto
    {
        public string Name { get; set; } = string.Empty;
        public string Descripton { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Localization { get; set; } = string.Empty;
        public List<string> Artists { get; set; } = new List<string>();
        public int NormalPrice { get; set; }
        public int VipPrice { get; set; }
    }
}
