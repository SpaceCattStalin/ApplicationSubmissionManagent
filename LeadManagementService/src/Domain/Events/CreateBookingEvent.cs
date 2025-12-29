using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class CreateBookingEvent
    {
        public string UserEmail { get; set; } = default!;
        public string UserFullName { get; set; } = default!;
        public string UserPhoneNumber { get; set; } = default!;
    }
}
