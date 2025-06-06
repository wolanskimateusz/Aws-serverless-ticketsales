namespace api.Dtos.Event
{
    public class UpdateEventDto
    {
        public string Name { get; set; } = string.Empty;
        public string Descripton { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Localization { get; set; } = string.Empty;
        public int NormalPrice { get; set; }
        public int VipPrice { get; set; }
    }
}
