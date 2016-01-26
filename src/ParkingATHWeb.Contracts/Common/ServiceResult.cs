using System.Collections.Generic;
using System.Linq;

namespace ParkingATHWeb.Contracts.Common
{

    public class ServiceResult
    {
        public bool IsValid => ValidationErrors == null || !ValidationErrors.Any();

        protected ServiceResult()
        {
            ValidationErrors = new List<string>();
        }
        protected ServiceResult(List<string> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public static ServiceResult Success()
        {
            return new ServiceResult();
        }

        public static ServiceResult Failure(List<string> validationErrors)
        {
            return new ServiceResult(validationErrors);
        }

        public List<string> ValidationErrors { get; set; }
    }


    public class ServiceResult<T>:ServiceResult
    {
        protected ServiceResult(T result)
        {
            Result = result;
            ValidationErrors = new List<string>();
        }

        private ServiceResult(List<string> validationErrors):base(validationErrors)
        {

        }

        public static ServiceResult<T> Failure(params string[] validationErrors)
        {
            var errors = new List<string>();
            errors.AddRange(validationErrors);
            return new ServiceResult<T>(errors);
        }  

        public new static ServiceResult<T> Failure(List<string> validationErrors)
        {
            return new ServiceResult<T>(validationErrors);
        }

        public static ServiceResult<T> Success(T result)
        {
            return new ServiceResult<T>(result);
        }

        public T Result { get; set; }
    }


    public class ServiceResult<T,T2> : ServiceResult<T>
    {

        protected ServiceResult(T result, T2 secondResult) : base(result)
        {
            SecondResult = secondResult; ValidationErrors = new List<string>();
        }

        public static ServiceResult<T, T2> Success(T result, T2 secondResult)
        {
            return new ServiceResult<T, T2>(result, secondResult);
        }

        public T2 SecondResult { get; set; }
    }
}
