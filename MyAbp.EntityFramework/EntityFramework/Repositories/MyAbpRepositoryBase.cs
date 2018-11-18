using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace MyAbp.EntityFramework.Repositories
{
    public abstract class MyAbpRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<MyAbpDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected MyAbpRepositoryBase(IDbContextProvider<MyAbpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class MyAbpRepositoryBase<TEntity> : MyAbpRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected MyAbpRepositoryBase(IDbContextProvider<MyAbpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
