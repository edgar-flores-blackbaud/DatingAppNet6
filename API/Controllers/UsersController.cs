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

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUserById(int id)
        {
            AppUser user = await _userRepository.GetUserByIdAsync(id);
            MemberDto mappedUser = _mapper.Map<MemberDto>(user);
            return Ok(mappedUser);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUserByUserName(string username)
        {
            return await _userRepository
                .GetMemberByMemberNameAsync(username);
        }
    }
}