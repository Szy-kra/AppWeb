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
                    Description = src.Description, // Krótki opis trafia do detali
                    Price = src.Price,             // Decimal do Decimal (bez konwersji!)
                    MaxPersons = src.MaxPersons,
                    Street = src.Street,
                    City = src.City,
                    PostalCode = src.PostalCode
                }))
                // Mapowanie listy linków na obiekty CottageImage
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src =>
                    src.ImageUrls.Select(url => new CottageImage { Url = url })))
                // Tworzenie sluga dla adresu URL
                .ForMember(dest => dest.EncodedName, opt => opt.MapFrom(src =>
                    src.Name.ToLower().Replace(" ", "-")));

            // 2. Mapowanie z Bazy (Domain) do Formularza (DTO) - potrzebne np. przy edycji
            CreateMap<Cottage, CottageDto>()
                .ForMember(dto => dto.Description, opt => opt.MapFrom(src => src.ContactDetails.Description))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(src => src.ContactDetails.Price)) // Decimal do Decimal
                .ForMember(dto => dto.City, opt => opt.MapFrom(src => src.ContactDetails.City))
                .ForMember(dto => dto.Street, opt => opt.MapFrom(src => src.ContactDetails.Street))
                .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(src => src.ContactDetails.PostalCode))
                .ForMember(dto => dto.MaxPersons, opt => opt.MapFrom(src => src.ContactDetails.MaxPersons))
                // Jeśli chcesz zaciągać zdjęcia z powrotem do DTO:
                .ForMember(dto => dto.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(img => img.Url).ToList()));
        }
    }
}