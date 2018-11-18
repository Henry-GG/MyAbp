using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using MyAbp.EntityFramework;

namespace MyAbp
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(MyAbpCoreModule))]
    public class MyAbpDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<MyAbpDbContext>(null);
        }
    }
}
