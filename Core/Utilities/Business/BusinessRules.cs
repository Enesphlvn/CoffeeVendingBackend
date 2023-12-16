using Core.Utilities.Results;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        //public static IResult Run(params IResult[] logics)
        //{
        //    foreach (var logic in logics)
        //    {
        //        if (!logic.Success)
        //        {
        //            return logic;
        //        }
        //    }
        //    return null;
        //}

        public static IResult Run(params IResult[] logics)
        {
            var errorMessages = logics.Where(x => !x.Success).Select(x => x.Message).ToList();

            if (errorMessages.Any())
            {
                string combinedErrorMessage = string.Join(", ", errorMessages);
                return new ErrorResult(combinedErrorMessage);
            }

            return new SuccessResult();
        }
    }


}
