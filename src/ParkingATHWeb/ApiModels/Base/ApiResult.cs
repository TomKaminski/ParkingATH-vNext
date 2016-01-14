using System.Collections.Generic;
using System.Linq;

namespace ParkingATHWeb.ApiModels.Base
{
    public class ApiResult
    {
        public bool IsValid => ValidationErrors == null || !ValidationErrors.Any();

        protected ApiResult()
        {

        }
        protected ApiResult(IEnumerable<string> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public static ApiResult Success()
        {
            return new ApiResult();
        }

        public static ApiResult Failure(IEnumerable<string> validationErrors)
        {
            return new ApiResult(validationErrors);
        }

        public IEnumerable<string> ValidationErrors { get; }
    }


    public class ApiResult<T> : ApiResult
    {
        protected ApiResult(T result)
        {
            Result = result;
        }

        private ApiResult(IEnumerable<string> validationErrors) : base(validationErrors)
        {
        }

        public static ApiResult<T> Failure(params string[] validationErrors)
        {
            var errors = new List<string>();
            errors.AddRange(validationErrors);
            return new ApiResult<T>(errors);
        }

        public new static ApiResult<T> Failure(IEnumerable<string> validationErrors)
        {
            return new ApiResult<T>(validationErrors);
        }

        public static ApiResult<T> Success(T result)
        {
            return new ApiResult<T>(result);
        }

        public T Result { get; set; }
    }


    public class ApiResult<T, T2> : ApiResult<T>
    {

        protected ApiResult(T result, T2 secondResult) : base(result)
        {
            SecondResult = secondResult;
        }

        public static ApiResult<T, T2> Success(T result, T2 secondResult)
        {
            return new ApiResult<T, T2>(result, secondResult);
        }

        public T2 SecondResult { get; set; }
    }
}
