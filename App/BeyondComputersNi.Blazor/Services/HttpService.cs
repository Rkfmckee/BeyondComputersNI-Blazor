﻿using System.Net.Http.Json;

namespace BeyondComputersNi.Blazor.Services;

public abstract class HttpService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
{
    private HttpClient httpClient => httpClientFactory.CreateClient(configuration["Api:HttpClient"] ?? "");

    public abstract string BaseUrl { get; }

    protected async Task<T> GetAsync<T>(string url)
    {
        var response = await httpClient.GetAsync($"{BaseUrl}/{url}");
        if (!response.IsSuccessStatusCode) await ThrowErrorMessage(response);

        var content = await response.Content.ReadFromJsonAsync<T>();
        if (content is null) throw new InvalidDataException();

        return content;
    }

    protected async Task<T> PostAsync<T>(string url, object requestBody)
    {
        var response = await httpClient.PostAsync($"{BaseUrl}/{url}", JsonContent.Create(requestBody));
        if (!response.IsSuccessStatusCode) await ThrowErrorMessage(response);

        var content = await response.Content.ReadFromJsonAsync<T>();
        if (content is null) throw new InvalidDataException();

        return content;
    }

    protected async Task PostAsync(string url, object requestBody)
    {
        var response = await httpClient.PostAsync($"{BaseUrl}/{url}", JsonContent.Create(requestBody));
        if (!response.IsSuccessStatusCode) await ThrowErrorMessage(response);
    }

    protected async Task DeleteAsync(string url)
    {
        var response = await httpClient.DeleteAsync($"{BaseUrl}/{url}");
        if (!response.IsSuccessStatusCode) await ThrowErrorMessage(response);
    }

    private async Task ThrowErrorMessage(HttpResponseMessage response)
    {
        var responseBodyMessage = await response.Content.ReadAsStringAsync();

        var errorMessage = string.IsNullOrEmpty(responseBodyMessage) ?
            $"{response.StatusCode}" : 
            $"{response.StatusCode}: {responseBodyMessage}";

        throw new Exception(errorMessage);
    }
}
