using AntiFraud.API.Dto;
using AntiFraud.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AntiFraud.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public PurchaseController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PurchaseDto>> GetPurchaseAsync(string id)
        {
            try
            {
                var purchase = await _dataContext.Purchases.FindAsync(id);
                if (purchase == null) return NotFound();
                return purchase.ToDto();
            }
            catch (Exception ex)
            {
                // todo add logging
                throw;
            }

        }


        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ModelStateDictionary))]
        public async Task<ActionResult<PurchaseDto>> CreatePurchaseAsync(PurchaseDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var purchaseEntry = await _dataContext.Purchases.AddAsync(dto.ToDomainObject());
                await _dataContext.SaveChangesAsync();

                var purchase = purchaseEntry.Entity;

                return CreatedAtAction(nameof(GetPurchaseAsync), new { id = purchase.Id }, purchase);
            }
            catch (Exception ex)
            {
                // todo add logging
                throw;
            }
        }
    }
}
