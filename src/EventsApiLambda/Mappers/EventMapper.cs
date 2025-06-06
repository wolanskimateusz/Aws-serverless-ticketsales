using api.Dtos.Artist;
using api.Dtos.Event;
using api.Migrations;
using api.Models;
using System.Runtime.CompilerServices;

namespace api.Mappers
{
    public static class EventMapper
    {
        public static Event ToEventFromCreateDto(this CreateEventDto eventDto)
        {
            return new Event
            {
                Name = eventDto.Name,
                Descripton = eventDto.Descripton,
                Date = eventDto.Date,
                Localization = eventDto.Localization,
                Artists = new List<Artist>(),
                NormalPrice = eventDto.NormalPrice,
                VipPrice = eventDto.VipPrice,
                
            };

        }

        public static EventDto ToEventDto(this Event eventModel)
        {
            return new EventDto
            {
                Id = eventModel.Id,
                Name = eventModel.Name,
                Descripton = eventModel.Descripton,
                Date = eventModel.Date,
                Localization = eventModel.Localization,
                Artists = eventModel.Artists.Select(x => x.ToArtistDto()).ToList() ?? new List<ArtistDto>(),
                NormalPrice = eventModel.NormalPrice,
                VipPrice= eventModel.VipPrice,
            };
        }

        public static EventListDto ToEventListDto(this Event eventModel)
        {
            return new EventListDto
            {
                Id = eventModel.Id,
                Name = eventModel.Name,
            };
        }
    }
}
