using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.ProductContent;

namespace DataAccess.Abstract
{
    public interface IProductContentDal : IEntityRepository<ProductContent>
    {
        List<GetAllProductContentDto> GetProductContentDetails();
    }
}
