using System.Reflection;
using Abp.Modules;

namespace MyAbp
{
    [DependsOn(typeof(MyAbpCoreModule))]
    public class MyAbpApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
