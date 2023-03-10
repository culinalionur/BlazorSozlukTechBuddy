using BlazorSozluk.Common.ViewModels.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateUserCommand command)
        {
            var guid = await _mediator.Send(command);

            return Ok(guid);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromBody]UpdateUserCommand command)
        {
            var guid = await _mediator.Send(command);
            return Ok(guid);
        }
    }
}
