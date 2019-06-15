using InterviewTestPagination.Models;
using InterviewTestPagination.Models.Todo;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTestPagination.Tests.Models {   
    public class TodoMemoryRepository : IModelRepository<Todo> {

        /// <summary>
        /// Example in-memory model datasource 'indexed' by id.
        /// </summary>
        private static readonly IDictionary<long, Todo> DataSource = new ConcurrentDictionary<long, Todo>();

        public TodoMemoryRepository(int size) {
            // initializing inmemory datasource
            var startDate = DateTime.Today;
            for (var i = 1; i <= size; i++) {
                var createdDate = startDate.AddDays(i);
                DataSource[i] = new Todo(id: i, task: "Dont forget to do " + i, createdDate: createdDate);
            }
        }

        public int Count() {
            return DataSource.Count();
        }

        public IQueryable<Todo> All() {
            return DataSource.Values.AsQueryable();
        }

    }
}
