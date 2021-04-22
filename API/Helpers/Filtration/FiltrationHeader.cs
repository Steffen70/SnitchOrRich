namespace API.Helpers.Filtration
{
    public class FiltrationHeader : IPagination
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public long TimeStampTicks { get; set; }

        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
    }
}