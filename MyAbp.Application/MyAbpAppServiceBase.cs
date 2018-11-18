using Abp.Application.Services;

namespace MyAbp
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class MyAbpAppServiceBase : ApplicationService
    {
        protected MyAbpAppServiceBase()
        {
            LocalizationSourceName = MyAbpConsts.LocalizationSourceName;
        }
    }
}