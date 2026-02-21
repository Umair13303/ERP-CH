using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Models.Responses
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? Data { get; set; }
        public int StatusCode { get; set; }

        public static ServiceResult Success(string response, int requestStatus) =>
            new ServiceResult { IsSuccess = true, Message = response, StatusCode = requestStatus };

        public static ServiceResult Failure(string response, int requestStatus) =>
            new ServiceResult { IsSuccess = false, Message = response, StatusCode = requestStatus };

    }
}
