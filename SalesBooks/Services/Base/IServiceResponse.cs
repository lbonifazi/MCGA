using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Base
{
    public interface IServiceResponse<T> : IServiceResponse
    {
        T Data { get; set; }
    }

    public interface IServiceResponse
    {
        List<ServiceError> Errors { get; set; }
        int ReturnValue { get; set; }
        string ReturnCode { get; set; }
        string ReturnName { get; set; }
        bool Status { get; }
        string WarningMessage { get; set; }

    }
}
