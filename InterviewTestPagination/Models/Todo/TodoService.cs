using InterviewTestPagination.Common;
using InterviewTestPagination.Utils.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTestPagination.Models.Todo {
    /// <summary>
    /// TODO: Implement methods that enable pagination
    /// </summary>
    public class TodoService : IModelService<Todo> {

        private IModelRepository<Todo> _repository;

        public IModelRepository<Todo> Repository {
            get { return _repository; }
            set { _repository = value; }
        }

        public TodoService(IModelRepository<Todo> repository) {
            this._repository = repository;
        }

        /// <summary>
        /// Example implementation of List method: lists all entries of type <see cref="Todo"/>
        /// </summary>
        /// <returns></returns>
        public PaginatedResult<IEnumerable<Todo>> List(PaginatedRequest request) {

            var totalPages = 1;

            var totalItems = Repository.Count();

            var result = Repository.All();

            if (!string.IsNullOrEmpty(request.OrderBy)) {
                result = result.OrderByMember(request.OrderBy, request.Reverse);
            }
            else {
                result = result.OrderByDescending(t => t.CreatedDate);
            }

            if (request.NumPerPage > 0) {

                totalPages = (result.Count() + request.NumPerPage - 1) / request.NumPerPage;

                result = result.Skip(request.NumPerPage * (request.CurrentPage - 1)).Take(request.NumPerPage);
            }

            return new PaginatedResult<IEnumerable<Todo>>() {
                Data = result,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }
    }
}
