
using Abp.Domain.Entities.Auditing;
using MyAbp.Attributes;
using System.Collections.Generic;

namespace MyAbp.Shopping
{
    [Comment("产品型号表")]
    public class ProductModels:FullAuditedAggregateRoot<long>
    {

        public ProductModels(long id,string modelNumber)
        {
            Id = id;
            ModelNumber = modelNumber;
        }

        [Comment("产品型号")]
        public string ModelNumber { get; private set; }

        public List<Products> ProductsList { get; set; }

    }
}
