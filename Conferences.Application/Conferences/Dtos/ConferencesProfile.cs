using AutoMapper;
using Conferences.Application.Conferences.Commands.CreateConference;
using Conferences.Application.Conferences.Commands.UpdateConference;
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
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Coordinate.X))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Coordinate.Y));

            CreateMap<CreateConferenceCommand, Conference>()
                .ForMember(
                    dest => dest.Location,
                    opt => opt.MapFrom(src =>
                        new Point(src.Location.Longitude, src.Location.Latitude) { SRID = 4326 })
                 );

            CreateMap<UpdateConferenceCommand, Conference>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom((src, dest) => src.Title ?? dest.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom((src, dest) => src.Description ?? dest.Description))
                .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom((src, dest) => src.LogoUrl ?? dest.LogoUrl))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom((src, dest) => src.StartDate ?? dest.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom((src, dest) => src.EndDate ?? dest.EndDate))
                .ForMember(
                    dest => dest.Location,
                    opt => opt.MapFrom((src, dest) =>
                        new Point(src.Location?.Longitude ?? dest.Location.Coordinate.X,
                        src.Location?.Latitude ?? dest.Location.Coordinate.Y)
                        { SRID = 4326 })

                )
                .ForMember(dest => dest.WebsiteUrl, opt => opt.MapFrom((src, dest) => src.WebsiteUrl ?? dest.WebsiteUrl))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom((src, dest) => src.CategoryId ?? dest.CategoryId));
        }
    }
}
