using System.Collections.Generic;

namespace LCARS.Models
{
    public class ApiError
    {
        public List<ApiErrorItem> Errors { get; set; }
    }
}