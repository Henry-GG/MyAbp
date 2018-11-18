using Abp.Web.Mvc.Views;

namespace MyAbp.Web.Views
{
    public abstract class MyAbpWebViewPageBase : MyAbpWebViewPageBase<dynamic>
    {

    }

    public abstract class MyAbpWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected MyAbpWebViewPageBase()
        {
            LocalizationSourceName = MyAbpConsts.LocalizationSourceName;
        }
    }
}