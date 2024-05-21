using AutoMapper;
using Conferences.Application.Conferences.Commands.CreateConference;
using Conferences.Domain.Entities;
using NetTopologySuite.Geometries;

namespace Conferences.Application.Conferences.Dtos
{
    public class ConferencesProfile : Profile
    {
        public ConferencesProfile()
        {
            CreateMap<Conference, ConferenceDto>();

            CreateMap<Point, LocationDto>()
                .ForMember(dto => dto.Longitude, options => options.MapFrom(src => src.Coordinate.X))
                .ForMember(dto => dto.Latitude, options => options.MapFrom(src => src.Coordinate.Y));

            CreateMap<CreateConferenceCommand, Conference>()
                .ForMember(
                    conf => conf.Location,
                    options => options.MapFrom(src =>
                        new Point(src.Location.Longitude, src.Location.Latitude) { SRID = 4326})
                 );
        }
    }
}
