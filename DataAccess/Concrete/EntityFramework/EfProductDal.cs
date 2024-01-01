using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Product;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, CoffeeVendingContext>, IProductDal
    {
        public List<GetGeneralContentIdDto> GetByGeneralContentId(int generalContentId)
        {
            using (CoffeeVendingContext context = new CoffeeVendingContext())
            {
                var result = from pc in context.ProductContents
                             join p in context.Products
                             on pc.ProductId equals p.Id
                             join gc in context.GeneralContents
                             on pc.GeneralContentId equals gc.Id
                             where pc.GeneralContentId == generalContentId
                             select new GetGeneralContentIdDto
                             {
                                 GeneralContentId = gc.Id,
                                 ProductId = p.Id,
                                 ProductName = p.Name,
                                 ImagePath = p.ImagePath,
                                 Description = p.Description,
                                 Price = p.Price
                             };
                return result.ToList();
            }
        }
    }
}