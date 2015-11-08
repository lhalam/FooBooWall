using System.Collections.Generic;

namespace Services.Registration
{
    public class RegistrationResult
    {
        public RegistrationResult()
        {
            Errors = new List<string>();
        }
        public bool Succeded { get; set; }
        public List<string> Errors { get; set; }
    }
}
