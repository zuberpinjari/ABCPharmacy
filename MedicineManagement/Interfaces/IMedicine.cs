using MedicineManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManagement.Interfaces
{
    public interface IMedicine
    {
        Task<Medicine> AddMedicine(Medicine medicine);
        Task<Medicine> DeleteMedicine(string id);
        Task EditMedicine(string id, Medicine medicine);
        Task<Medicine> GetMedicine(string id);
        Task<List<Medicine>> GetMedicines();

    }
}
