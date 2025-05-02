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

            CreateMap<StudentVM, StudentModel>().ReverseMap();
            CreateMap<CreateTeacherVM, TeacherModel>().ReverseMap();

            CreateMap<TeacherVM, TeacherModel>().ReverseMap();

            CreateMap<NameModel, NameVM>().ReverseMap();
            
            // Card
            CreateMap<CardModel, CardVM>().ReverseMap();

            // School
            CreateMap<SchoolModel, SchoolVM>().ReverseMap();

            // Row
            CreateMap<RowModel, RowVM>().ReverseMap();

            CreateMap<RowModel, RowVM>().ReverseMap();
            CreateMap<RowModel, CreateRowVM>().ReverseMap();
            

            // Modul
            CreateMap<ModulModel, ModulVM>().ReverseMap();
            CreateMap<CreateModulVM, ModulModel>().ReverseMap();





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
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IModulRepository, ModulRepository>();

        services.AddAutoMapper(typeof(CifegMapper));
    }
    


}