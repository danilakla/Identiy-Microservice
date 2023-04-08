using Identiy_API.Model;
using Identiy_API.Services.UniversityService;
using Identiy_API.Services;

namespace Identiy_API.Infrastructure.AuthFactory.AuthAgregatorFactory
{
    public class AgregatorAuthUser: IAgregatorAuthFactory
    {
        private readonly ITokenServices tokenServices;
        private readonly IUniversityService universityService;
        private readonly IDeanService deanService;
        private readonly IStudentService studentService;

        public AgregatorAuthUser(ITokenServices tokenServices,
        IUniversityService universityService,
        IDeanService deanService,
        IStudentService studentService)
        {
            this.tokenServices = tokenServices;
            this.universityService = universityService;
            this.deanService = deanService;
            this.studentService = studentService;
        }
        public IAuthFactoy GetAuthService(string role)
        {
            try
            {
                switch (role)
                {

                    case "Manager": return new ManagerAuthService(tokenServices, universityService); break;
                    case "Teacher": return new TeacherAuthService(tokenServices, universityService); break;
                    case "Dean": return new DeanAuthService(tokenServices, deanService); break;
                    case "Student": return new StudentAuthService(tokenServices, studentService); break;
                    default:
                        throw new Exception("500, are  not implemented service");
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
