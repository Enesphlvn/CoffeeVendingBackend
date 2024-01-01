using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.GeneralContent;
using Entities.DTOs.Product;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        List<GetGeneralContentIdDto> GetByGeneralContentId(int generalContentId);
    }
}
