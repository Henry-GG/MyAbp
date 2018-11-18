using System.Reflection;
using Abp.Modules;

namespace MyAbp
{
    public class MyAbpCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
