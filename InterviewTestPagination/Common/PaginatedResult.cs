
namespace InterviewTestPagination.Common
{
    public class PaginatedResult<T>
    {
        public T Data { get; set; }

        public int TotalPages { get; set; }

        public int TotalItems { get; set; }
    }
}