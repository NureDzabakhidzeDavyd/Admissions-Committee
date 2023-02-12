namespace HandbookActivity.Core.Domain;

/// <summary>
///     One page data set
///     <typeparam name="T">Type of entity</typeparam>
/// </summary>
public class Page<T>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Page{T}" /> class.
    /// </summary>
    public Page()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Page{T}" /> class.
    /// </summary>
    /// <param name="data">Data from current page</param>
    /// <param name="total">Total count of items by filter</param>
    public Page(IEnumerable<T> data, int total)
    {
        Data = data;
        TotalCount = total;
    }

    /// <summary>
    ///     Gets or sets data from current page
    /// </summary>
    public IEnumerable<T> Data { get; set; }

    /// <summary>
    ///     Gets or sets total count of items by filter
    /// </summary>
    public int TotalCount { get; set; }
}