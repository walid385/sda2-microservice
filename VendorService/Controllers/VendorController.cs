using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VendorService.DTOs;
using VendorService.Models;
using VendorService.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class VendorController : ControllerBase
{
    private readonly IVendorRepository _vendorRepository;
    private readonly IMapper _mapper;

    public VendorController(IVendorRepository vendorRepository, IMapper mapper)
    {
        _vendorRepository = vendorRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VendorDto>>> GetAllVendors()
    {
        var vendors = await _vendorRepository.GetAllVendorsAsync();
        return Ok(_mapper.Map<IEnumerable<VendorDto>>(vendors));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VendorDto>> GetVendor(int id)
    {
        var vendor = await _vendorRepository.GetVendorByIdAsync(id);
        if (vendor == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<VendorDto>(vendor));
    }

    [HttpPost]
    public async Task<ActionResult<VendorDto>> CreateVendor(CreateVendorDto createVendorDto)
    {
        var vendor = _mapper.Map<Vendor>(createVendorDto);
        await _vendorRepository.AddVendorAsync(vendor);
        
        return CreatedAtAction(nameof(GetVendor), new { id = vendor.VendorId }, _mapper.Map<VendorDto>(vendor));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVendor(int id, UpdateVendorDto updateVendorDto)
    {
        if (id != updateVendorDto.VendorId)
        {
            return BadRequest("Vendor ID mismatch.");
        }

        var existingVendor = await _vendorRepository.GetVendorByIdAsync(id);
        if (existingVendor == null)
        {
            return NotFound("Vendor not found.");
        }

        _mapper.Map(updateVendorDto, existingVendor);
        await _vendorRepository.UpdateVendorAsync(existingVendor);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVendor(int id)
    {
        var vendor = await _vendorRepository.GetVendorByIdAsync(id);
        if (vendor == null)
        {
            return NotFound("Vendor not found.");
        }

        await _vendorRepository.DeleteVendorAsync(id);
        return NoContent();
    }
}
