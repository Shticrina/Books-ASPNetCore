using Client.Exceptions;
using Shared.Responses;
using System.Net;
using System.Net.Http.Json;

namespace Client.Services.Api;

public abstract class BaseApiService
{
    protected static async Task<T> ReadResponseAsync<T>(HttpResponseMessage response)
    {
        ApiResponse<T>? apiResponse = null;

        if (response.IsSuccessStatusCode)
        {
            apiResponse = await response.Content
                .ReadFromJsonAsync<ApiResponse<T>>();
        }

        await HandleResponseAsync(response, apiResponse);

        return apiResponse!.Data!;
    }

    protected static async Task HandleResponseAsync<T>(
        HttpResponseMessage response,
        ApiResponse<T>? apiResponse)
    {
        if (response.IsSuccessStatusCode)
        {
            if (apiResponse is null)
                throw new Exception("Unexpected response from server.");

            return;
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var validation = await response.Content
                .ReadFromJsonAsync<ValidationProblem>();

            if (validation is not null)
                throw new ApiValidationException(validation);
        }

        // Business errors returned by middleware
        if (response.Content.Headers.ContentLength > 0)
        {
            apiResponse ??= await response.Content
                .ReadFromJsonAsync<ApiResponse<T>>();

            if (apiResponse is not null)
                throw new Exception(apiResponse.Message);
        }

        throw new Exception("An unknown error occurred.");
    }

    protected async Task<T> GetAsync<T>(HttpClient http, string url)
    {
        var response = await http.GetAsync(url);
        return await ReadResponseAsync<T>(response);
    }

    protected async Task<TResponse> PostAsync<TRequest, TResponse>(
        HttpClient http,
        string url,
        TRequest request)
    {
        var response = await http.PostAsJsonAsync(url, request);

        return await ReadResponseAsync<TResponse>(response);
    }

    protected async Task PutAsync<TRequest>(
        HttpClient http,
        string url,
        TRequest request)
    {
        var response = await http.PutAsJsonAsync(url, request);

        await ReadResponseAsync<bool>(response);
    }

    protected async Task DeleteAsync(
        HttpClient http,
        string url)
    {
        var response = await http.DeleteAsync(url);

        await ReadResponseAsync<bool>(response);
    }
}