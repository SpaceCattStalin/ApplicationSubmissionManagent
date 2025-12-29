using Application.DTOs;
using Application.UseCases.Commands.ClaimApplicationBooking;
using Application.UseCases.Commands.CreateApplicationBooking;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using System.Reflection;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            ApplyMappingsFromAssembly(currentAssembly);

            CreateMap<ApplicationBooking, ResponseApplicationBooking>();

            CreateMap<CreateApplicationCommand, ApplicationBooking>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.GraduationYear, opt => opt.MapFrom(src => ParseToInt(src.GraduationYear)))
                .ForMember(dest => dest.MathScore, opt => opt.MapFrom(src => ParseToInt(src.MathScore)))
                .ForMember(dest => dest.EnglishScore, opt => opt.MapFrom(src => ParseToInt(src.EnglishScore)))
                .ForMember(dest => dest.LiteratureScore, opt => opt.MapFrom(src => ParseToInt(src.LiteratureScore)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => ApplicationStatus.Waiting))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Histories, opt => opt.Ignore());

            CreateMap<ClaimApplicationCommand, ApplicationBooking>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ApplicationId));
        }

        private static int ParseToInt(string value)
        {
            return int.TryParse(value, out int result) ? result : 0;
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapFromType = typeof(IMapFrom<>);

            var mappingMethodName = nameof(IMapFrom<object>.Mapping);

            bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(mappingMethodName);

                if (methodInfo != null)
                {
                    methodInfo.Invoke(instance, new object[] { this });
                }
                else
                {
                    var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                    if (interfaces.Count > 0)
                    {
                        foreach (var @interface in interfaces)
                        {
                            var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                            interfaceMethodInfo?.Invoke(instance, new object[] { this });
                        }
                    }
                }
            }
        }
    }
}
