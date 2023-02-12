namespace HandbookActivity.Contracts.Responses;

/// <summary>
/// Represents base api response.
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Gets or sets api response data.
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// Gets or sets api response error.
    /// </summary>
    public string Error { get; set; }
}
