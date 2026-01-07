using AppWeb.Application.DataTransferObject;
using AutoMapper;

namespace AppWeb.Application.Mappings
{
    public class CottageMappingProfile : Profile
    {
        public CottageMappingProfile()
        {
            CreateMap<CottageDto, Domain.Entities.Cottage>()
                .ForMember(dest => dest.ContactDetails, opt => opt.MapFrom(src => new Domain.Entities.CottageDetails
                {
                    Price = src.Price,
                    MaxPersons = src.MaxPersons,
                    Street = src.Street,
                    City = src.City,
                    PostalCode = src.PostalCode
                }))
                // To mapowanie zamienia listę tekstów na listę obiektów bazy danych
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src =>
                    src.ImageUrls.Select(url => new Domain.Entities.CottageImage { Url = url })))

                .ForMember(dest => dest.EncodedName, opt => opt.MapFrom(src => src.Name.ToLower().Replace(" ", "-")));
        }
    }
}