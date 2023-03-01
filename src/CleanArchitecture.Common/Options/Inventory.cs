
using System;
namespace CleanArchitecture.Common.Options
{
    public class Inventory
    {
        public string UrlBase { get; set; } = string.Empty;
        public string PathNotifySale { get; set; } = string.Empty;

        public Uri GetUrlNotifySale(long productId)
        {
            var pathFormatted = string.Format(
                PathNotifySale,
                productId);

            var url = string.Concat(UrlBase, pathFormatted);

            return new Uri(url);
        }
    }
}

