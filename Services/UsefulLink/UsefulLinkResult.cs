using System.Collections.Generic;

namespace Services
{
    public class UsefulLinkResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
        public UsefulLinkResult()
        {
            Errors = new List<string>();
        }
    }
}
