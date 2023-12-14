using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.Product;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<GetProductDto>> GetAll();
        IResult Add(CreateProductDto productDto);
        IResult Update(UpdateProductDto productDto);
        IResult Delete(int productId);
        IResult HardDelete(int productId);
        IDataResult<GetProductByIdDto> GetById(int productId);
    }
}
