using System.Text.Json;
using API.Helpers.Filtration;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static FilteredList<TList> AddFiltrationHeader<TList>(this HttpResponse response, FilteredList<TList> filteredList)
        {
            AddFiltrationHeader(response, filteredList.Header);

            return filteredList;
        }

        public static FilteredList<TList, THeader> AddFiltrationHeader<TList, THeader>(this HttpResponse response, FilteredList<TList, THeader> filteredList)
            where THeader : FiltrationHeader
        {
            AddFiltrationHeader(response, filteredList.Header);

            return filteredList;
        }

        private static void AddFiltrationHeader<THeader>(HttpResponse response, THeader header)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            response.Headers.Add("Filtration", JsonSerializer.Serialize(header, options));
            response.Headers.Add("Access-Control-Expose-Headers", "Filtration");
        }
    }
}