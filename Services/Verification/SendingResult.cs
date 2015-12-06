using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Verification
{
    public class SendingResult
    {
        public SendingResult()
        {
            Errors = new List<string>();
        }
        public bool Succeded { get; set; }
        public List<string> Errors { get; set; }
    }
}
