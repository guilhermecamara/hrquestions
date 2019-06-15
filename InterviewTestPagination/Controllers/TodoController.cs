using System.Collections.Generic;
using System.Web.Http;
using InterviewTestPagination.Common;
using InterviewTestPagination.Models;
using InterviewTestPagination.Models.Todo;

namespace InterviewTestPagination.Controllers {    

    /// <summary>
    /// 'Rest' controller for the <see cref="Todo"/>
    /// model.
    /// 
    /// TODO: implement the pagination Action
    /// </summary>
    public class TodoController : ApiController {

        // TODO: [low priority] setup DI 
        private IModelService<Todo> _todoService;

        public TodoController(IModelService<Todo> todoService) {
            this._todoService = todoService;
        }

        [HttpPost]
        public PaginatedResult<IEnumerable<Todo>> Todos([FromBody] PaginatedRequest request) {            

            var result = _todoService.List(request);

            return result;            
        }
    }
}
