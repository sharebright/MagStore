using System.Collections.Generic;

namespace MagStore.Web.Infrastructure
{
    public static class Extensions
    {
        public static List<T> SortAtoZ<T>(this List<T> list)
        {
            list.Sort();
            return list;
        }
    }
}