using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using FSA.Core.Utils;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RazorClassLibrary1.Helpers
{
    /// <summary>
    /// Class <see cref="ResultSO"/>: Extends <see cref="Result"/> to deserialize an entity T type into corresponding class.
    /// </summary>
    /// <typeparam name="TData">Generic type</typeparam>
    public class ResultSO<TData> : Result
    {
        public List<TData> Data { get; set; }

        public ResultSO()
        {
        }

        public ResultSO(bool succeeded, List<TData> data, List<string> errors, Pagination pagination, CustomStatusCode statusCode)
            : base(succeeded, errors, pagination, statusCode)
        {
            Data = data;
        }

        public ResultSO(bool succeeded, List<TData> data, List<string> errors, CustomStatusCode statusCode)
            : base(succeeded, errors, statusCode)
        {
            Data = data;
        }

        public static ResultSO<TData> SuccessWith(TData data, CustomStatusCode statusCode)
        {
            return new ResultSO<TData>(succeeded: true, null!, new List<string>(), statusCode);
        }

        public static ResultSO<TData> SuccessWith(TData data, Pagination pagination, CustomStatusCode statusCode)
        {
            return new ResultSO<TData>(succeeded: true, null!, new List<string>(), pagination, statusCode);
        }

        public new static ResultSO<TData> Failure(IEnumerable<string> errors, CustomStatusCode statusCode)
        {
            return new ResultSO<TData>(succeeded: false, null!, errors.ToList(), statusCode);
        }

        public static Result<TData> Failure(IEnumerable<string> errors, TData data, CustomStatusCode statusCode)
        {
            return new Result<TData>(succeeded: false, data, errors.ToList(), statusCode);
        }

        public static implicit operator ResultSO<TData>(string error)
        {
            return Failure(new List<string> { error }, CustomStatusCode.StatusBadRequest);
        }

        public static implicit operator ResultSO<TData>(List<string> errors)
        {
            return Failure(errors, CustomStatusCode.StatusBadRequest);
        }

        public static implicit operator ResultSO<TData>(CustomStatusCode statusCode)
        {
            return Failure(new List<string>(), statusCode);
        }

        public static implicit operator ResultSO<TData>(TData data)
        {
            return SuccessWith(data, CustomStatusCode.StatusOk);
        }

        public static implicit operator bool(ResultSO<TData> result)
        {
            return result.Succeeded;
        }
    }
}
