using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            IEnumerable<MemberDto> allUsers
                = await _userRepository.GetMembersAsync();
            return Ok(allUsers);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AppUser>> GetUserById(int id)
        {
            AppUser user = await _userRepository.GetUserByIdAsync(id);
            MemberDto mappedUser = _mapper.Map<MemberDto>(user);
            return Ok(mappedUser);
        }

        [HttpGet("{username:alpha}")]
        public async Task<ActionResult<MemberDto>> GetUserByUserName(string username)
        {
            return await _userRepository
                .GetMemberByMemberNameAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUserNameAsync(userName ?? "");
            if (user == null) return NotFound();
            _mapper.Map(memberUpdateDto, user);
            if (await _userRepository.SaveAllAsync())
                return NoContent();
            return BadRequest("Failed to update user");
        }
    }
}