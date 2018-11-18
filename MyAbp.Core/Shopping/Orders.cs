
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MyAbp.Attributes;
using System.Collections.Generic;

namespace MyAbp.Shopping
{
    [Comment("订单表")]
    public class Orders: FullAuditedAggregateRoot<long>
    {

        public Orders(long id,string orderNumber,ICollection<Products> products)
        {
            Id = id;
            OrderNumber = orderNumber;
            ProductsList = products;
        }

        [Comment("订单号")]
        public string OrderNumber { get; private set; }

        //public List<Products> ProductsList { get;private set; }
        public ICollection<Products> ProductsList { get; private set; }

    }
}
