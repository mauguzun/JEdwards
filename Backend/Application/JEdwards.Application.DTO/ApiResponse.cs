namespace JEdwards.Application.DTO
{
    public record ApiResponse<T>
    {
        public T? Data { get; init; }
        public string? ResponseErrorMessage { get; init; }
        public string? ApiExceptionMessage { get; init; }

        public void Deconstruct(out T? data, out string? errorMessage, out string? apiExceptionMessage)
        {
            data = Data;
            errorMessage = ResponseErrorMessage;
            apiExceptionMessage = ApiExceptionMessage;
        }
    }
}
