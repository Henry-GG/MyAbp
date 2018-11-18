using Abp.Web.Mvc.Controllers;

namespace MyAbp.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class MyAbpControllerBase : AbpController
    {
        protected MyAbpControllerBase()
        {
            LocalizationSourceName = MyAbpConsts.LocalizationSourceName;
        }
    }
}