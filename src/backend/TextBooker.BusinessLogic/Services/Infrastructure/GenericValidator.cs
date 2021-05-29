using System;
using System.Linq;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace TextBooker.BusinessLogic.Services.Infrastructure
{
	public class GenericValidator<T> : AbstractValidator<T>
	{
		public static Result Validate(Action<GenericValidator<T>> configureAction, T entity)
		{
			var validator = new GenericValidator<T>();
			configureAction(validator);
			return validator.GetValidationResult(entity);
		}

		private Result GetValidationResult(T entity)
		{
			var validationResult = base.Validate(entity);
			return validationResult.IsValid
				? Result.Ok()
				: Result.Combine(validationResult
					.Errors
					.Select(e => Result.Failure(e.ErrorMessage))
					.ToArray());
		}
	}
}
