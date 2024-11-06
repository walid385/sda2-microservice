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
            // Retrieve tax rate based on state (region)
            var taxRate = await _taxRepository.GetTaxRateByStateAsync(calculationDto.Region);
            if (taxRate == null)
                return NotFound("Tax rate for the specified region not found.");

            // Calculate total tax amount
            float totalTaxRate = taxRate.StateTax + taxRate.CountyTax + taxRate.CityRate;
            float taxAmount = calculationDto.Amount * totalTaxRate;
            float totalAmount = calculationDto.Amount + taxAmount;

            return Ok(new TaxResultDto { TaxAmount = taxAmount, TotalAmount = totalAmount, TaxRates = totalTaxRate });
        }


        // GET: api/tax/rate/{state}
        [HttpGet("rate/{state}")]
        public async Task<ActionResult<TaxRateDto>> GetTaxRateByState(string state)
        {
            if (string.IsNullOrEmpty(state))
            {
                state = "DEFAULT";
            }

            // Retrieve the tax rate for the specified state
            var taxRate = await _taxRepository.GetTaxRateByStateAsync(state);

            // If not found, use the 'DEFAULT' rate
            if (taxRate == null)
            {
                taxRate = await _taxRepository.GetTaxRateByStateAsync("DEFAULT");
                if (taxRate == null)
                {
                    return NotFound("No applicable tax rate found, and 'DEFAULT' rate is missing.");
                }
            }

            // Calculate the TotalTaxRate
            float totalTaxRate = taxRate.StateTax + taxRate.CountyTax + taxRate.CityRate;

            // Return the TaxRateDto with TotalTaxRate included
            return Ok(new TaxRateDto
            {
                StateTax = taxRate.StateTax,
                CountyTax = taxRate.CountyTax,
                CityRate = taxRate.CityRate,
                State = taxRate.State,
            });
        }





    }
}
