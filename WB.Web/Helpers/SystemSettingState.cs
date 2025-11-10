namespace WB.Web.Helpers
{
    public class SystemSettingState
    {
        public int QuickAccessItemsLimit { get; set; } = 2;
        public int RecentItemsLimit { get; set; } = 2;
        public int FavoriteItemsLimit { get; set; } = 2;
        public int PageSize { get; set; } = 10;
        public List<int> PageSizeOptions { get; set; } = new List<int> { 10, 25, 50, 100 };
    }
}
