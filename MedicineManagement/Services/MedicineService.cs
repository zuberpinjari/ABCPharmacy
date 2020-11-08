using MedicineManagement.Data;
using MedicineManagement.Interfaces;
using MedicineManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManagement.Services
{
    public class MedicineService : IMedicine
    {
        private readonly MedicineContext _context;
        private readonly ILogger _logger;
        public MedicineService(MedicineContext medicineContext, ILogger logger)
        {
            _context = medicineContext;
            _logger = logger;
        }

        public async Task<List<Medicine>> GetMedicines()
        {
            return await _context.Medicine.ToListAsync();
        }

        public async Task<Medicine> GetMedicine(string id)
        {
            var medicine = await _context.Medicine.FindAsync(id);

            if (medicine == null)
            {
                _logger.LogInformation("No medicine found for given id");
                return null;
            }

            return medicine;
        }

        public async Task EditMedicine(string id, Medicine medicine)
        {
            _context.Entry(medicine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(id))
                {
                    _logger.LogError("Medicine not found");
                    throw new Exception("Not Found");
                }
                else
                {
                    throw;
                }
            }

            return;
        }

         private bool MedicineExists(string id)
        {
            return _context.Medicine.Any(e => e.Id == id);
        }
        public async Task<Medicine> AddMedicine(Medicine medicine)
        {
            _context.Medicine.Add(medicine);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MedicineExists(medicine.Id))
                {
                    _logger.LogError("Medicine already exists");
                    throw new Exception("Already exists");
                }
                else
                {
                    throw;
                }
            }
            return medicine;
        }
        public async Task<Medicine> DeleteMedicine(string id)
        {
            var medicine = await _context.Medicine.FindAsync(id);
            if (medicine == null)
            {
                _logger.LogInformation("No medicine found for given id");
                throw new Exception("Not Found");
            }

            _context.Medicine.Remove(medicine);
            await _context.SaveChangesAsync();

            return medicine;
        }
    }
}
