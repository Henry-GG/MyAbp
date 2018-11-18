
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAbp.Migrations
{
    public static  class ModelConfigurationExtension
    {

        public static void UpdateDatabasComment(this Database database, string tableName, string comments)
        {
            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(comments))
            {
                return;
            }
            var sql = $"Alter Table {tableName} Comment '{comments}'";
            database.ExecuteSqlCommand(sql);
        }

        public static void UpdateColumnComment(this Database database, string tableName, string columnName, string property, string comments)
        {
            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(columnName) ||
                string.IsNullOrWhiteSpace(property) || string.IsNullOrWhiteSpace(comments))
            {
                return;
            }
            var sql = $"Alter Table {tableName} Modify column {columnName} {property} Comment '{comments}'";
            database.ExecuteSqlCommand(sql);
        }
    }
}
