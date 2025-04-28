using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;

public static class InstallServ
{

    public class  CifegMapper : AutoMapper.Profile
    {
        public CifegMapper()
        {
            CreateMap<CreateStudentVM, StudentModel>().ReverseMap();

         
        }
    }

    public static void AddMDAPiServices(this IServiceCollection services)
    {
       
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<INameRepository, NameRepository>();

        services.AddAutoMapper(typeof(CifegMapper));
    }

   
        
}