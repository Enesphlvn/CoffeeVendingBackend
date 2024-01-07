using Core.Utilities.Results;
using Entities.DTOs.Statistics;

namespace Business.Abstract
{
    public interface IStatisticsService
    {
        IDataResult<StatisticsDto> GetAllStatistics();
    }
}
