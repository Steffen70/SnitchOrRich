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
        private readonly MemberRepository _memberRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MembersController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _memberRepository = unitOfWork.GetRepo<MemberRepository>();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers([FromQuery] FiltrationParams filtrationParams)
        {
            var membersList = await _memberRepository.GetMembersAsync(filtrationParams);

            Response.AddFiltrationHeader(membersList);

            return membersList.Result;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MemberDto>> GetMemberById([FromRoute] int id)
        => await _memberRepository.GetMemberByIdAsync(id);

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetMemberByUsername([FromRoute] string username)
        => await _memberRepository.GetMemberByUsernameAsync(username);
    }
}