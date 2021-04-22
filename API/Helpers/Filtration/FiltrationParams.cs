using System;

namespace API.Helpers.Filtration
{
    public class FiltrationParams : IPagination
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        public int CurrentPage { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        internal DateTime _timeStamp = DateTime.UtcNow;

        public long TimeStampTicks
        {
            get => _timeStamp.Ticks;
            set => _timeStamp = new DateTime(value, DateTimeKind.Utc);
        }
    }
}