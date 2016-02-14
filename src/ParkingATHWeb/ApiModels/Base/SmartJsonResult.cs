using System.Collections.Generic;
using System.Linq;

namespace ParkingATHWeb.ApiModels.Base
{
    public class SmartJsonResult
    {
        public bool IsValid => ValidationErrors == null || !ValidationErrors.Any();

        protected SmartJsonResult()
        {

        }
        protected SmartJsonResult(IEnumerable<string> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public static SmartJsonResult Success()
        {
            return new SmartJsonResult();
        }

        public static SmartJsonResult Failure(IEnumerable<string> validationErrors)
        {
            return new SmartJsonResult(validationErrors);
        }

        public IEnumerable<string> ValidationErrors { get; }
    }


    public class SmartJsonResult<T> : SmartJsonResult
    {
        protected SmartJsonResult(T result)
        {
            Result = result;
        }

        private SmartJsonResult(IEnumerable<string> validationErrors) : base(validationErrors)
        {
        }

        private SmartJsonResult(T result, IEnumerable<string> validationErrors) : base(validationErrors)
        {
            Result = result;
        }

        public static SmartJsonResult<T> Failure(params string[] validationErrors)
        {
            var errors = new List<string>();
            errors.AddRange(validationErrors);
            return new SmartJsonResult<T>(errors);
        }

        public static SmartJsonResult<T> Failure(IEnumerable<string> validationErrors, T result)
        {
            return new SmartJsonResult<T>(result, validationErrors);
        }

        public static SmartJsonResult<T> Failure(T result)
        {
            return new SmartJsonResult<T>(result);
        }

        public new static SmartJsonResult<T> Failure(IEnumerable<string> validationErrors)
        {
            return new SmartJsonResult<T>(validationErrors);
        }

        public static SmartJsonResult<T> Success(T result)
        {
            return new SmartJsonResult<T>(result);
        }

        public T Result { get; set; }
    }


    public class SmartJsonResult<T, T2> : SmartJsonResult<T>
    {

        protected SmartJsonResult(T result, T2 secondResult) : base(result)
        {
            SecondResult = secondResult;
        }

        public static SmartJsonResult<T, T2> Success(T result, T2 secondResult)
        {
            return new SmartJsonResult<T, T2>(result, secondResult);
        }

        public T2 SecondResult { get; set; }
    }
}
