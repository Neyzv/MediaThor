namespace MediaThor.Sandbox.Services.Validation;

public interface IRequestValidationService
{
    void ValidateRequest<TRequest>(TRequest request);
}