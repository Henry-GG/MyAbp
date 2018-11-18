
using System.Data.Entity.Migrations;

namespace MyAbp.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MyAbp.EntityFramework.MyAbpDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;//自动迁移打开，不用add-migration，直接update-database
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "MyAbp";
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(MyAbp.EntityFramework.MyAbpDbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...

            new CommentUpdater().UpdateComment(context);
            new DemoDataCreator(context).Create();

        }
    }
}
