using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedAuthorizationAssignment.Models.DIO
{
    public class CartViewModel
    {
        public IEnumerable<Cart> ListCart { get; set; }

        public double CartTotal { get; set; }
    }
}
