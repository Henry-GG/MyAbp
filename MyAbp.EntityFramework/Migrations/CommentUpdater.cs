
using MyAbp.Attributes;
using MyAbp.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyAbp.Migrations
{
    public  class CommentUpdater
    {

        public void UpdateComment(MyAbpDbContext context)
        {
            var properties = typeof(MyAbpDbContext).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var prop in properties)
            {
                if (prop.PropertyType.Name.Contains("IDbSet"))
                {
                    var tableType = prop.PropertyType.GetGenericArguments()[0];
                    UpdateComment(context, tableType);
                }
            }
        }

        private static void UpdateComment(MyAbpDbContext context,Type tableType)
        {
            string tableName;
            UpdateTableComment(context, tableType, out tableName);
            UpdateColumnComment(context, tableType, tableName);
        }

        public static void UpdateTableComment(MyAbpDbContext context,Type tableType,out string tableName)
        {
            tableName = context.GetTableName(tableType);
        }

        public static void UpdateColumnComment(MyAbpDbContext context,Type tableType,string tableName)
        {

            foreach (var prop in tableType.GetProperties())
            {
                var propDateType = string.Empty;

                if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
                {
                    continue;
                }
                var attrs = prop.GetCustomAttributes(typeof(CommentAttribute), false);
                var columnComment = attrs.Length > 0 ? ((CommentAttribute)attrs[0]).Comments : string.Empty;

                if (prop.PropertyType == typeof(string))
                {
                    var lengthAttrs = prop.GetCustomAttributes(typeof(MaxLengthAttribute), false);
                    if (lengthAttrs.Length == 0)
                    {
                        continue;
                    }
                    var length = ((MaxLengthAttribute)lengthAttrs[0]).Length;
                    propDateType = $"varchar({length})";
                }

                if (prop.PropertyType == typeof(DateTime?) || prop.PropertyType == typeof(DateTime))
                {
                    propDateType = "Datetime";
                }
                if (prop.PropertyType == typeof(long) || prop.PropertyType == typeof(long?))
                {
                    propDateType = "BIGINT(20)";
                }

                if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
                {
                    propDateType = "TINYINT(1)";
                }

                if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
                {
                    propDateType = "decimal(18, 2)";
                }

                if (prop.PropertyType.IsEnum || prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                {
                    var valueAttrs = prop.GetCustomAttributes(typeof(DefaultValueAttribute), false);
                    var defaultValue = valueAttrs.Length > 0 ? ((DefaultValueAttribute)valueAttrs[0]).Value.ToString() : string.Empty;
                    propDateType = string.IsNullOrEmpty(defaultValue) ? "int" : $"int default {defaultValue}";

                }

                if (prop.PropertyType == typeof(byte[]))
                {
                    propDateType = "longblob";
                }

                context.Database.UpdateColumnComment(tableName, prop.Name, propDateType.ToUpper(), columnComment);
            }
        }

        private static string GetComment(Type tableType)
        {
            var attrs = tableType.GetCustomAttributes(typeof(CommentAttribute), false);
            return attrs.Length > 0 ? ((CommentAttribute)attrs[0]).Comments : string.Empty;
        }
    }


    public static class DbContextExtension
    {
        public static string GetTableName<T>(this DbContext context) where T : class
        {
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;
            return objectContext.GetTableName<T>();
        }

        public static string GetTableName<T>(this ObjectContext context) where T : class
        {
            var sql = context.CreateObjectSet<T>().ToTraceString();
            var regex = new Regex("FROM (?<table>.*)AS");
            var match = regex.Match(sql);
            var table = match.Groups["table"].Value;
            return table;
        }

        public static string GetTableName(this DbContext context,Type tableType)
        {
            var methodInfo = typeof(DbContextExtension).GetMethod("GetTableName", new Type[] { typeof(MyAbpDbContext) });
            if (methodInfo == null)
            {
                return string.Empty;
            }
            var method = methodInfo.MakeGenericMethod(tableType);
            return (string)method.Invoke(context, new object[] { context });
        }
    }
}
