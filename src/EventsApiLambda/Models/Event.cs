using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Events")]
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Descripton { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Localization { get; set; } = string.Empty;
        public int NormalPrice { get; set; }
        public int VipPrice { get; set; }

        public List<Artist> Artists { get; set; } = new List<Artist>();

    }
}
