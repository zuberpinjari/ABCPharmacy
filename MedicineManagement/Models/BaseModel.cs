using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManagement.Models
{
    public class BaseModel
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; } = "Admin";
        public string UpdatedBy { get; set; } = "Admin";
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
