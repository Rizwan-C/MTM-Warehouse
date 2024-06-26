﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MTM_Warehouse.Entities
{
    public class LoginEmp
    {
        [Key]
        public int LoginEmpId { get; set; }

        [RegularExpression(@"^([A-Za-z][A-Za-z ]{3,30})$", ErrorMessage = "Invalid Name")]
        public string? Name { get; set; }

        [RegularExpression(@"^((?!\.)[\w\-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$", ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }
       
        public string? Role { get; set; }

        public string? Username { get; set; }

        public ICollection<Approvals>? Approvals { get; set; }

        public int? WarehouseInfoId { get; set; }
        public WarehouseInfo? WarehouseInfo { get; set; }

    }
}
