using Microsoft.AspNetCore.Mvc;
using LeadService.Domain.Store;
using static Domain.Constant.Constants;

namespace Domain.Common
{
    public class CustomControllerBase : ControllerBase
    {
        public IActionResult OkResponse<T>(T? data)
        {
            BaseResponse<T> rs = new(
                statusCode: StatusCodeHelper.OK,
                code: ResponseCodeConstants.SUCCESS,
                data: data,
                message: MessagesConstants.SUCCESS
            );
            return base.Ok(rs);
        }
    }
}
