using System.Reflection;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;

namespace MyAbp
{
    [DependsOn(typeof(AbpWebApiModule), typeof(MyAbpApplicationModule))]
    public class MyAbpWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(MyAbpApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
