using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ProductContent;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductContentDal : EfEntityRepositoryBase<ProductContent, CoffeeVendingContext>, IProductContentDal
    {
        public List<GetProductContentDetailDto> GetProductContentDetails()
        {
            using (CoffeeVendingContext context = new CoffeeVendingContext())
            {
                var result = from pc in context.ProductContents
                             join p in context.Products
                             on pc.ProductId equals p.Id
                             join gc in context.GeneralContents
                             on pc.GeneralContentId equals gc.Id
                             select new GetProductContentDetailDto
                             {
                                 Id = pc.Id,
                                 ProductName = p.Name,
                                 GeneralContentName = gc.Name,
                                 Unit = pc.Unit
                             };
                return result.ToList();
            }
        }
    }
}
