namespace APIClient
{
    public partial class Pagination
    {
        public long Total { get; set; }

        public long Pages { get; set; }

        public long Page { get; set; }

        public long Limit { get; set; }

        public Links Links { get; set; }
    }
}
