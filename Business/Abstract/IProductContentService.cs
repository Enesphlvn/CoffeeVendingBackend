﻿using Core.Utilities.Results;
using Entities.DTOs.ProductContent;

namespace Business.Abstract
{
    public interface IProductContentService
    {
        IDataResult<List<GetAllProductContentDto>> GetAll();
        IResult Add(CreateProductContentDto productContentDto);
        IResult Update(UpdateProductContentDto productContentDto);
        IResult Delete(int productContentId);
        IResult HardDelete(int productContentId);
        IDataResult<GetProductContentByIdDto> GetById(int productContentId);
    }
}
