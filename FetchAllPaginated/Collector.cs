using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace FetchAllPaginated
{
    public class Collector
    {
        public Dictionary<int, string> FetchAllApiDataSequential()
        {
            List<Dictionary<int, string>> allData = new List<Dictionary<int, string>>();
            bool hasMoreItems = true;
            int page = 1;
            while(hasMoreItems)
            {
                var apiData = ApiDataProvider.Fetch(page);
                if(apiData.Count > 0)
                {
                    allData.Add(apiData);
                    page++;
                }
                else
                {
                    hasMoreItems = false;
                }
            }
            return allData.SelectMany(d => d).OrderBy(item => item.Key).ToDictionary(item => item.Key, item => item.Value);
        }

        public Dictionary<int, string> FetchAllApiDataParallel()
        {
            ConcurrentDictionary<int, string> allData = new ConcurrentDictionary<int, string>();
            bool hasMoreItems = true;
            int page = 1;

            ParallelOptions options = new ParallelOptions
            {
                MaxDegreeOfParallelism = 4
            };

            Parallel.ForEach(
                Partitioner.Create(0, int.MaxValue), options,
                (range, state) =>
                {
                    bool hasMoreItems = true;

                    while (hasMoreItems)
                    {
                        var apiData = ApiDataProvider.Fetch(page);
                        if (apiData.Count == 0)
                        {
                            hasMoreItems = false;
                        }
                        else
                        {
                            foreach (var item in apiData)
                            {
                                allData.TryAdd(item.Key, item.Value);
                            }
                            page++;
                        }
                    }
                });

            return allData.OrderBy(item => item.Key).ToDictionary(item => item.Key, item => item.Value);
        }
    }
}
