using System;
using System.Collections.Generic;
using System.Linq;

namespace DispatcherResultConceptProof.Dispatchers
{
    public class DispatcherResult<T>
    {
        public T Data { get; set; }
        public bool HasErrors => ErrorMessages?.Any() ?? false;
        public List<string> SuccessfulMessages { get; set; }
        public List<string> ErrorMessages { get; set; }
        public List<string> InfoMessages { get; set; }

        public string GetErrorMessagesAsString()
        {
            return string.Join(Environment.NewLine, ErrorMessages);
        }
    }
}
