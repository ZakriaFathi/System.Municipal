using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal.Utils.Vm
{
    public class ResultDecorator<T> : IResult<T>
    {
        public bool IsFailed { get; } = true;
        public bool IsSuccess { get; } = false;
        public List<IReason> Reasons { get; }
        public List<IError> Errors { get; }
        public List<ISuccess> Successes { get; }
        public T Value { get; }
        public T ValueOrDefault { get; }

        public static Result<TValue> FailWithValue<TValue>(TValue value, IError error)
        {
            Result<TValue> result = new Result<TValue>();
            result.WithError(error);

            result.WithValue(value);

            return result;
        }
    }
}
