using AppWeb.Application.DataTransferObject;
using AppWeb.Domain.Entities;
using AutoMapper;

namespace AppWeb.Application.Mappings
{
    public class CottageMappingProfile : Profile
    {
        public CottageMappingProfile()
        {
            // 1. Mapowanie z DTO (Formularz) do Encji (Baza)
            CreateMap<CottageDto, Cottage>()
                .MaxDepth(1) // Zatrzymuje mapowanie w kółko
                .ForMember(dest => dest.ContactDetails, opt => opt.MapFrom(src => new CottageDetails
                {
                    Description = src.Description,
                    Price = src.Price,
                    MaxPersons = src.MaxPersons,
                    Street = src.Street,
                    City = src.City,
                    PostalCode = src.PostalCode
                }))
                .ForMember(dest => dest.EncodedName, opt => opt.MapFrom(src =>
                    src.Name != null ? src.Name.ToLower().Replace(" ", "-") : ""));

            // 2. Mapowanie z Encji (Baza) do DTO (Wyświetlanie na liście)
            // TO TUTAJ NASTĘPOWAŁ CRASH PRZY POBIERANIU LISTY
            CreateMap<Cottage, CottageDto>()
                .MaxDepth(1) // Zatrzymuje mapowanie w kółko
                .ForMember(dto => dto.Description, opt => opt.MapFrom(src => src.ContactDetails.Description))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(src => src.ContactDetails.Price))
                .ForMember(dto => dto.City, opt => opt.MapFrom(src => src.ContactDetails.City))
                .ForMember(dto => dto.Street, opt => opt.MapFrom(src => src.ContactDetails.Street))
                .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(src => src.ContactDetails.PostalCode))
                .ForMember(dto => dto.MaxPersons, opt => opt.MapFrom(src => src.ContactDetails.MaxPersons))
                .ForMember(dto => dto.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(img => img.Url).ToList()));

            // 3. Mapowanie dla samych zdjęć (pomocnicze)
            CreateMap<CottageImage, CottageImageDto>();
        }
    }
}