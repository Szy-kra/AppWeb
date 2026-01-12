using AppWeb.Application.Cottage.Commands.CreateCottage;
using AppWeb.Application.Cottage.Commands.EditCottage;
using AppWeb.Application.DataTransferObject;
using AppWeb.Domain.Entities;
using AutoMapper;

namespace AppWeb.Application.Mappings
{
    public class CottageMappingProfile : Profile
    {
        public CottageMappingProfile()
        {
            // 1. Mapowanie z DTO do Bazy (Domain)
            CreateMap<CottageDto, AppWeb.Domain.Entities.Cottage>()
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
                    src.ImageUrls != null ? src.ImageUrls.Select(url => new CottageImage { Url = url }) : new List<CottageImage>()))
                .ForMember(dest => dest.EncodedName, opt => opt.MapFrom(src =>
                    src.Name != null ? src.Name.ToLower().Replace(" ", "-") : string.Empty));

            // 2. Mapowanie z Bazy (Domain) do DTO
            CreateMap<AppWeb.Domain.Entities.Cottage, CottageDto>()
                .ForMember(dto => dto.Description, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.Description : string.Empty))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.Price : 0))
                .ForMember(dto => dto.City, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.City : string.Empty))
                .ForMember(dto => dto.Street, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.Street : string.Empty))
                .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.PostalCode : string.Empty))
                .ForMember(dto => dto.MaxPersons, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.MaxPersons : 0))
                .ForMember(dto => dto.ImageUrls, opt => opt.MapFrom(src => src.Images != null ? src.Images.Select(img => img.Url).ToList() : new List<string>()));

            // 3. Mapowania dla Komend MediatR

            // Tworzenie
            CreateMap<CottageDto, CreateCottageCommand>();
            CreateMap<CreateCottageCommand, AppWeb.Domain.Entities.Cottage>()
                .IncludeBase<CottageDto, AppWeb.Domain.Entities.Cottage>();

            // Edycja: DTO -> Command (Potrzebne w Kontrolerze!)
            // To pozwoli na: _mapper.Map<EditCottageCommand>(cottageDto)
            CreateMap<CottageDto, EditCottageCommand>();

            // Edycja: Entity -> Command (Ładowanie danych do formularza)
            CreateMap<AppWeb.Domain.Entities.Cottage, EditCottageCommand>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.Description : string.Empty))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.Price : 0))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.City : string.Empty))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.Street : string.Empty))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.PostalCode : string.Empty))
                .ForMember(dest => dest.MaxPersons, opt => opt.MapFrom(src => src.ContactDetails != null ? src.ContactDetails.MaxPersons : 0))
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images != null ? src.Images.Select(img => img.Url).ToList() : new List<string>()));
        }
    }
}