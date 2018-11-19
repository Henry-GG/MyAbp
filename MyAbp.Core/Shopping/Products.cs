using Abp.Domain.Entities.Auditing;
using MyAbp.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace MyAbp.Shopping
{
    [Comment("产品表")]
    public class Products: FullAuditedAggregateRoot<long>
    {
        public Products(long id,string name,long? productModelFk)
        {
            Id = id;
            Name = name;
            ProductModelsIdFK = productModelFk;
        }

        [Comment("产品名称")]
        public string Name { get;private set; }

        [Comment("产品型号外键")]
        [ForeignKey("ProductModels")]
        public long? ProductModelsIdFK { get; set; }

        //virtual 用于Lazy loading
        public virtual ProductModels ProductModels { get; set; }

        //public List<Orders>  OrderList { get;  set; }
        public virtual ICollection<Orders>  OrderList { get;  set; }//实现懒加载

        public class ProductsConfiguration : EntityTypeConfiguration<Products>
        {
            public ProductsConfiguration()
            {
                Configure(this);
            }
            
            public void Configure(EntityTypeConfiguration<Products> configuration)
            {
                configuration.ToTable("Products");
                configuration.Property(e => e.Name).HasMaxLength(64);

            }
        }

        public class ProductsToOrdersMapping
        {
            public void AddModelBuilder(DbModelBuilder modelBuilder)
            {
                #region  产品表和订单表多对多关系配置

                modelBuilder.Entity<Orders>()
                    .HasMany(g => g.ProductsList)
                    .WithMany(g => g.OrderList);

                modelBuilder.Entity<Products>()
                    .HasMany(g => g.OrderList)
                    .WithMany(g => g.ProductsList);

                #endregion
            }
        }
    }
}
