namespace Lup.Software.Engineering.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ModelStateMessage
    {
        public ModelStateMessage()
        {
            this.Errors = new List<string>();
        }

        public string Key { get; set; }

        public string AttemptedValue { get; set; }

        public object RawValue { get; set; }

        public ICollection<string> Errors { get; set; }
    }
}