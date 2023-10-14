using System.Text;

namespace FetchAllPaginated
{
    public static class ApiDataProvider
    {
        private const int TotalCount = 33;
        private const int ItemsPerPage = 10;

        // For Tests only
        public static int GetTotalCount()
        {
            return TotalCount;
        }

        public static Dictionary<int, string> Fetch(int page)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            if (page > 0 && page * ItemsPerPage < TotalCount + ItemsPerPage)
            {
                int ItemsNumber = (page * ItemsPerPage < TotalCount) ? ItemsPerPage : TotalCount%ItemsPerPage;
                for (int i = 0; i < ItemsNumber; i++)
                {
                    dict.Add((i+1) + (page-1)* ItemsPerPage, GetRandomString(10));
                }
            }
            return dict;
        }
        private static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                stringBuilder.Append(chars[index]);
            }

            return stringBuilder.ToString();
        }
    }
}
