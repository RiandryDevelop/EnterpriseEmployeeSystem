namespace EES.Application.Common.Models;

/// <summary>
/// A generic wrapper used to provide paginated data to the client.
/// This structure supports server-side pagination requirements for the frontend dashboard.
/// </summary>
/// <typeparam name="T">The type of the elements in the list.</typeparam>
public class PaginatedList<T>
{
    /// <summary>
    /// The collection of items for the current page.
    /// </summary>
    public List<T> Items { get; }

    /// <summary>
    /// The current page index (starting from 1).
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// The total number of pages available based on the PageSize.
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// The total count of records across all pages.
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    /// Initializes a new instance of the PaginatedList class.
    /// </summary>
    /// <param name="items">The list of items for the specific page.</param>
    /// <param name="count">Total number of items in the database.</param>
    /// <param name="pageNumber">The current page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    /// <summary>
    /// Indicates if there is a page available before the current one.
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// Indicates if there is a page available after the current one.
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;
}