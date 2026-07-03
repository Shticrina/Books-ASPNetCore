using Shared.Responses;

namespace Client.Services;

public abstract class BaseApiService
{
    protected static void HandleResponse<T>(ApiResponse<T>? response)
    {
        if (response is null)
            throw new Exception("Unexpected response from server.");

        if (!response.Success)
            throw new Exception(response.Message ?? "An unknown error occurred.");
    }
}