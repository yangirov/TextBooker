using System;
using System.Linq;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace TextBooker.BusinessLogic.Infrastructure
{
	public class GenericValidator<T> : AbstractValidator<T>
	{
		public static Result Validate(Action<GenericValidator<T>> action, T entity)
		{
			var validator = new GenericValidator<T>();
			action(validator);
			return validator.GetValidationResult(entity);
		}

		private Result GetValidationResult(T entity)
		{
			var validationResult = Validate(entity);

			return validationResult.IsValid
				? Result.Ok()
				: Result.Combine(validationResult
					.Errors
					.Select(e => Result.Failure(e.ErrorMessage))
					.ToArray());
		}
	}
}
