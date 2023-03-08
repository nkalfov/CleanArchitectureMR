using System;
namespace CleanArchitecture.Common.Options
{
    // TODO: Move to infrastructure
    public class InventoryOptions
    {
        public const string SettingKey = "Inventory";

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

