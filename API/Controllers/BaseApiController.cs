using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Helpers;
using AutoMapper;

namespace API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BaseApiController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly UnitOfWork _unitOfWork;

        public BaseApiController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}