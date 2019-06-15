using InterviewTestPagination.Common;
using InterviewTestPagination.Models;
using InterviewTestPagination.Models.Todo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace InterviewTestPagination.Tests.Models {
    /// <summary>
    /// Unity testing for <see cref="InterviewTestPagination.Models.Todo.TodoService" />
    /// TODO: implement tests you consider important
    /// </summary>
    [TestClass]
    public class TodoServiceTest {

        private IModelService<Todo> _service = null;
        private int SIZE = 50;

        [TestInitialize]
        public void Init() {
            var repository = new TodoMemoryRepository(SIZE);
            _service = new TodoService(repository);
        }

        [TestMethod]
        public void Todo_List() {
            // prepare
            var request = new PaginatedRequest() {
                CurrentPage = 1,
                NumPerPage = 5,
                OrderBy = "Id",
                Reverse = false
            };

            // act
            var result = _service.List(request);

            // assert            
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.Data.Count() <= 5);
        }

        [TestMethod]
        public void Todo_List_All() {
            // prepare
            var request = new PaginatedRequest() {
                CurrentPage = 1,
                NumPerPage = 0
            };

            // act
            var result = _service.List(request);

            // assert            
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Data.Count(), result.TotalItems);
            Assert.AreEqual(result.Data.Count(), SIZE);
        }
    }
}
