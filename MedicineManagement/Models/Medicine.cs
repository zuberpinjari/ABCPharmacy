﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManagement.Models
{
    public class Medicine : BaseModel
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Notes { get; set; }
    }
}
