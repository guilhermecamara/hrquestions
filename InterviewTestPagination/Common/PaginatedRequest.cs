using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTestPagination.Common {

    public class PaginatedRequest {

        public int NumPerPage { get; set; }

        public int CurrentPage { get; set; }

        public string OrderBy { get; set; }

        public bool Reverse { get; set; }
    }
}