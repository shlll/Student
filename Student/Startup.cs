using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Student.Startup))]
namespace Student
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
