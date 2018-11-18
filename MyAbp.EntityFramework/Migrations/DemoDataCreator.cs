
using MyAbp.EntityFramework;
using MyAbp.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAbp.Migrations
{
    public class DemoDataCreator
    {
        protected readonly MyAbpDbContext _context;

        public DemoDataCreator(MyAbpDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            var productModels = new List<ProductModels>
            {
                new ProductModels(1,"111111"),
                new ProductModels(2,"222222"),
                new ProductModels(3,"333333"),
                new ProductModels(4,"444444")
            };
            _context.ProductModels.AddRange(productModels);
            _context.SaveChanges();


            var productList = new List<Products>
            {
                new Products(1,"产品1",1),
                new Products(2,"产品2",2),
                new Products(3,"产品3",3),
                new Products(4,"产品4", 4)
            };
            _context.Products.AddRange(productList);
            _context.SaveChanges();


            var products1 = new List<Products>
            {
                productList.FirstOrDefault(e=>e.Id==1)
            };
            //var products2 = productList.Select(e => e.Id > 1 && e.Id < 4).ToList();
            var products2 = new List<Products>
            {
                 productList.FirstOrDefault(e=>e.Id==3),
                 productList.FirstOrDefault(e=>e.Id==2),
            };
            var products3 = new List<Products>
            {
                productList.FirstOrDefault(e => e.Id == 4)
            };

            var orders = new List<Orders>
            {
                new Orders(1,"订单1",products1),
                new Orders(2,"订单2",products2),
                new Orders(3,"订单3",products3)
            };
            _context.Orders.AddRange(orders);
            _context.SaveChanges();

        }

    }
}
