using System.Collections.Generic;
using System.Linq;

namespace InterviewTestPagination.Models {

    /// <summary>
    /// Datasource/Driver layer's main entry-point.
    /// TODO: create appropriate method signatures
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IModelRepository<T> {
        /// <summary>
        /// Returns the total number of T records
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Example of method signature: lists all entries of type T
        /// </summary>
        /// <returns></returns>
        IQueryable<T> All();
    }
}
