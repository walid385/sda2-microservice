using Microsoft.AspNetCore.Mvc;
using TaxService.DTOs;
using TaxService.Models;
using TaxService.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace TaxService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxRepository _taxRepository;

        public TaxController(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }

        // GET: api/tax
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxRate>>> GetAllTaxRates()
        {
            var taxRates = await _taxRepository.GetAllTaxRatesAsync();
            return Ok(taxRates);
        }

        // POST: api/tax
        [HttpPost]
        public async Task<ActionResult> CreateTaxRate(TaxRate taxRate)
        {
            await _taxRepository.AddTaxRateAsync(taxRate);
            return CreatedAtAction(nameof(GetAllTaxRates), new { id = taxRate.TTID }, taxRate);
        }

        // PUT: api/tax/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTaxRate(int id, TaxRate taxRate)
        {
            if (id != taxRate.TTID)
                return BadRequest();

            await _taxRepository.UpdateTaxRateAsync(taxRate);
            return NoContent();
        }

        // DELETE: api/tax/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTaxRate(int id)
        {
            await _taxRepository.DeleteTaxRateAsync(id);
            return NoContent();
        }

        // POST: api/tax/calculate
        [HttpPost("calculate")]
        public async Task<ActionResult<TaxResultDto>> CalculateTax(TaxCalculationDto calculationDto)
        {
            // Define a default tax year or logic to determine the applicable tax year.
            int taxYear = 2024;  // Replace this with a dynamic year selection if needed

            // Retrieve the applicable tax rate based on the defined year
            var taxRate = await _taxRepository.GetTaxRateAsync(taxYear);  // Using an integer year here
            if (taxRate == null)
                return NotFound("Tax rate not found.");

            var taxAmount = calculationDto.Amount * taxRate.TaxRates;
            var totalAmount = calculationDto.Amount + taxAmount;

            return Ok(new TaxResultDto { TaxAmount = taxAmount, TotalAmount = totalAmount });
        }

    }
}
