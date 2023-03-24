﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedAuthorizationAssignment.Models
{
    public class Product
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Required(ErrorMessage = "Enter the Product Image")]

        public string Product_Image { get; set; }

        [Required(ErrorMessage = "Enter the Product Name")]
        public string Product_Name { get; set; }

        [Required(ErrorMessage = "Enter the Product Description")]
        public string Product_Description { get; set; }

        [Required(ErrorMessage = "Enter the Product Price")]
        public double Product_Price { get; set; }

      
        public int CategoryId { get; set; }
        public  Category Categories { get; set; }
    }
}
