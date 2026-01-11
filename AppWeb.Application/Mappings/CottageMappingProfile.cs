using AppWeb.Application.DataTransferObject;
using AppWeb.Domain.Entities;
using AutoMapper;

namespace AppWeb.Application.Mappings
{
    public class CottageMappingProfile : Profile
    {
        public CottageMappingProfile()
        {
            // 1. Mapowanie z Formularza (DTO) do Bazy (Domain)
            CreateMap<CottageDto, Cottage>()
                .ForMember(dest => dest.ContactDetails, opt => opt.MapFrom(src => new CottageDetails
                {
                    Description = src.Description,
                    Price = src.Price,
                    MaxPersons = src.MaxPersons,
                    Street = src.Street,
                    City = src.City,
                    PostalCode = src.PostalCode
                }))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src =>
                    src.ImageUrls.Select(url => new CottageImage { Url = url })))
                .ForMember(dest => dest.EncodedName, opt => opt.MapFrom(src =>
                    src.Name.ToLower().Replace(" ", "-")));

            // 2. Mapowanie z Bazy (Domain) do Formularza (DTO)
            CreateMap<Cottage, CottageDto>()
                .ForMember(dto => dto.Description, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.Description : string.Empty))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.Price : 0))
                .ForMember(dto => dto.City, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.City : string.Empty))
                .ForMember(dto => dto.Street, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.Street : string.Empty))
                .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.PostalCode : string.Empty))
                .ForMember(dto => dto.MaxPersons, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.MaxPersons : 0))
                .ForMember(dto => dto.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(img => img.Url).ToList()));
        }
    }
}