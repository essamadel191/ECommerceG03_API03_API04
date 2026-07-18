using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Common
{
    public class Result // Void
    {
        public bool IsSuccess { get; }
        public IReadOnlyList<Error> Errors { get; set; }

        public Result(bool isSuccess, IReadOnlyList<Error> errors = null)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result Ok() => new Result(true, Array.Empty<Error>());
        public static Result Fail(Error error) => new Result(false, new List<Error> { error });
        public static Result Fail(IReadOnlyList<Error> errors) => new Result(false, errors);

    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;
        public TValue data => IsSuccess ? _value : throw new InvalidOperationException("Failed To Get Error");
        public Result(TValue value) : base(true, Array.Empty<Error>())
        {
            _value = value;
        }
        public Result(Error errors) : base(false, new List<Error> { errors })
        {
            _value = default!;
        }
        public Result(IReadOnlyList<Error> errors) : base(false, errors)
        {
            _value = default!;
        }

        public static Result<TValue> Ok(TValue value) => new Result<TValue>(value);
        public static new Result<TValue> Fail(Error error) => new Result<TValue>(error); // Extented Method
        public static new Result<TValue> Fail(IReadOnlyList<Error> errors) => new Result<TValue>(errors); // Extented Method

    }
}