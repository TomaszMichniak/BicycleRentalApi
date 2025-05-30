using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTO.Payment
{
    public class PayMethod
    {
        public string Type { get; set; } = default!;
        public string Value { get; set; } = default!;
    }
}
