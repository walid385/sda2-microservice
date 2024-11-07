using Microsoft.AspNetCore.Mvc;
using SalesService.Models;
using SalesService.Repositories;
using SalesService.DTOs;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistersController : ControllerBase
    {
        private readonly IRegistersRepository _registersRepository;
        private readonly IMapper _mapper;

        public RegistersController(IRegistersRepository registersRepository, IMapper mapper)
        {
            _registersRepository = registersRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegistersTableDto>> GetRegisterById(int id)
        {
            var register = await _registersRepository.GetRegisterByIdAsync(id);
            if (register == null)
                return NotFound("Register not found.");

            return Ok(_mapper.Map<RegistersTableDto>(register));
        }

        [HttpPost("open")]
        public async Task<ActionResult> OpenRegister([FromBody] RegistersTableDto registerDto)
        {
            var register = _mapper.Map<RegistersTable>(registerDto);
            await _registersRepository.OpenRegisterAsync(register);
            return Ok("Register opened successfully.");
        }

        [HttpPut("close/{id}")]
        public async Task<ActionResult> CloseRegister(int id, [FromBody] RegistersTableDto registerDto)
        {
            var register = await _registersRepository.GetRegisterByIdAsync(id);
            if (register == null)
                return NotFound("Register not found.");

            register.CloseTotal = registerDto.CloseTotal;
            register.CloseTime = registerDto.CloseTime;
            register.Note = registerDto.Note;
            await _registersRepository.CloseRegisterAsync(register);
            return Ok("Register closed successfully.");
        }
    }
}
