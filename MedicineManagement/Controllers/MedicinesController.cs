using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicineManagement.Data;
using MedicineManagement.Models;
using MedicineManagement.Interfaces;
using Microsoft.Extensions.Logging;

namespace MedicineManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly MedicineContext _context;
        private readonly IMedicine _medicineService;
        private readonly ILogger _logger;
        public MedicinesController(MedicineContext context, IMedicine medicineService, ILogger logger)
        {
            _context = context;
            _medicineService = medicineService;
            _logger = logger;
        }

        // GET: api/Medicines
        [HttpGet]
        public async Task<ActionResult<List<Medicine>>> GetMedicine()
        {
            try
            {
               List<Medicine> medicines = await _medicineService.GetMedicines();
                return Ok(medicines);
            }
            catch(Exception ex)
            {
                _logger.LogError(1000, ex, "Error occurred in api/medicine/get"); //TODO : Need to define error events
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Medicines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicine>> GetMedicine(string id)
        {
            try
            {
                var medicine = await _medicineService.GetMedicine(id);
                if (medicine == null)
                {
                    return NotFound();
                }
                return Ok(medicine);
            }
            catch(Exception ex)
            {
                _logger.LogError(1000, ex, "Error occurred in api/medicine/get/id"); //TODO : Need to define error events
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Medicines/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicine(string id, Medicine medicine)
        {
            if (id != medicine.Id)
            {
                return BadRequest();
            }

            try
            {
                await _medicineService.EditMedicine(id, medicine);
            }
            catch (Exception ex)
            {
                _logger.LogError(1000, ex, "Error occurred in api/medicine/"); //TODO : Need to define error events
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        // POST: api/Medicines
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Medicine>> PostMedicine(Medicine medicine)
        {
            try
            {
                await _medicineService.AddMedicine(medicine);
            }
            catch(Exception ex)
            {
                _logger.LogError(1000, ex, "Error occurred in api/vehicle/post"); //TODO : Need to define error events
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
            return CreatedAtAction("GetMedicine", new { id = medicine.Id }, medicine);
        }

        // DELETE: api/Medicines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Medicine>> DeleteMedicine(string id)
        {
            try
            {
                var medicine = await _medicineService.DeleteMedicine(id);
                return medicine;
            }
            catch(Exception ex)
            {
                _logger.LogError(1000, ex, "Error occurred in api/vehicle/delete"); //TODO : Need to define error events
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
       
    }
}
