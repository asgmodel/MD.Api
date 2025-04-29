using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;


public static class InstallServ
{

    public class  CifegMapper : AutoMapper.Profile
    {
        public CifegMapper()
        {
            //CreateMap<NameModel, NameVM>()
            //    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Name} {src.Title}"));
            //CreateMap<CardModel, CardVM>()
            // .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name != null ? $"{src.Name.Name} {src.Name.Title}" : null))
            // .ForMember(dest => dest.SexType, opt => opt.MapFrom(src => src.SexType.HasValue ? src.SexType.ToString() : null));


            //CreateMap<CreateNameVM, NameModel>();

            //CreateMap<UpdateNameVM, NameModel>();
            //CreateMap<CreateRowVM, RowModel>().ReverseMap();
            //CreateMap<UpdateRowVM, RowModel>().ReverseMap();
            ////CreateMap<RowModel, UpdateRowVM>();
            CreateMap<CreateStudentVM, StudentModel>().ReverseMap();

            //CreateMap<CreateSchoolVM, SchoolModel>().ReverseMap();
            //CreateMap<UpdateSchoolVM, SchoolModel>().ReverseMap();



            CreateMap<NameModel, NameVM>().ReverseMap();
            
            // Card
            CreateMap<CardModel, CardVM>().ReverseMap();

            // School
            CreateMap<SchoolModel, SchoolVM>().ReverseMap();

            // Row
            CreateMap<RowModel, RowVM>().ReverseMap();
            CreateMap<RowModel, CreateRowVM>().ReverseMap();


            // Modul
            CreateMap<ModulModel, ModulVM>().ReverseMap();

            // Student
            CreateMap<StudentModel, StudentVM>()
                .ForMember(dest => dest.Row, opt => opt.MapFrom(src => src.Row))
                .ForMember(dest => dest.Card, opt => opt.MapFrom(src => src.Card))
                .ForMember(dest => dest.School, opt => opt.MapFrom(src => src.School))
                .ForMember(dest => dest.Moduls, opt => opt.MapFrom(src => src.Moduls))
                .ForMember(dest => dest.Teachers, opt => opt.MapFrom(src => src.Teachers))
                .ReverseMap();

            // Teacher
            CreateMap<TeacherModel, TeacherVM>().ReverseMap();
        }
    }

    public static void AddMDAPiServices(this IServiceCollection services)
    {
        services.AddScoped<INameRepository, NameRepository>();

        services.AddScoped<ICardRepository, CardRepository>();

        services.AddScoped<IRowRepository, RowRepository>();
        services.AddScoped<ISchoolRepository, SchoolRepository>();

        services.AddScoped<IStudentRepository, StudentRepository>();

        services.AddAutoMapper(typeof(CifegMapper));
    }
    


}