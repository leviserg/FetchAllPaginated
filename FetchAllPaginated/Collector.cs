using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace FetchAllPaginated
{
    public class Collector
    {
        public Dictionary<int, string> FetchAllApiDataSequential(int items)
        {
            List<Dictionary<int, string>> allData = new List<Dictionary<int, string>>();

            if (items <= 0)
                return new Dictionary<int, string>();

            bool hasMoreItems = true;
            int page = 1;
            int pageSize = 0;
            int pagesToRead = 1;

            while (hasMoreItems)
            {
                var apiData = ApiDataProvider.Fetch(page);
                if(page == 1)
                {
                    pageSize = apiData.Count;
                    pagesToRead += items / pageSize;
                }
                if (apiData.Count > 0 && page <= pagesToRead)
                {
                    allData.Add(apiData);
                    page++;
                }
                else
                {
                    hasMoreItems = false;
                }
            }

            return allData.SelectMany(d => d).OrderBy(item => item.Key).Take(items).ToDictionary(item => item.Key, item => item.Value);
        }

        public Dictionary<int, string> FetchAllApiDataParallel(int items)
        {
            ConcurrentDictionary<int, string> allData = new ConcurrentDictionary<int, string>();

            if (items <= 0) 
                return new Dictionary<int, string>();

            ParallelOptions options = new ParallelOptions
            {
                MaxDegreeOfParallelism = 4
            };

            Parallel.ForEach(
                Partitioner.Create(0, int.MaxValue), options,
                (range, state) =>
                {
                    bool hasMoreItems = true;
                    int page = 1;
                    int pageSize = 0;
                    int pagesToRead = 1;

                    while (hasMoreItems)
                    {
                        var apiData = ApiDataProvider.Fetch(page);
                        if (page == 1)
                        {
                            pageSize = apiData.Count;
                            pagesToRead += items / pageSize;
                        }
                        if (apiData.Count > 0 && page <= pagesToRead)
                        {
                            foreach (var item in apiData)
                            {
                                allData.TryAdd(item.Key, item.Value);
                            }
                            page++;
                        }
                        else
                        {
                            hasMoreItems = false;
                        }
                    }

                });

            return allData.OrderBy(item => item.Key).Take(items).ToDictionary(item => item.Key, item => item.Value);
        }
    }
}
