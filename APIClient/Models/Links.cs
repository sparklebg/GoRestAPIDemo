using System;

namespace APIClient
{
    public partial class Links
    {
        public object Previous { get; set; }

        public Uri Current { get; set; }

        public Uri Next { get; set; }
    }
}
