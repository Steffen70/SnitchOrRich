using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Extensions;
using API.Helpers.Filtration;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MembersController : BaseApiController
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MembersController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers([FromQuery] FiltrationParams filtrationParams)
        {
            var membersList = await _unitOfWork.MemberRepository.GetMembersAsync(filtrationParams);

            Response.AddFiltrationHeader(membersList);

            return membersList.Result;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MemberDto>> GetMemberById([FromRoute] int id)
        => await _unitOfWork.MemberRepository.GetMemberByIdAsync(id);

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetMemberByUsername([FromRoute] string username)
        => await _unitOfWork.MemberRepository.GetMemberByUsernameAsync(username);
    }
}