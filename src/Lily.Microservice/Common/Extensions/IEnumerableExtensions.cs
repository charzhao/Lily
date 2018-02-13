using System.Collections;
using System.Linq;

namespace Lily.Microservice.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static TResult FirstOfType<TResult>(this IEnumerable list)
        where TResult : class
        {
            return list.OfType<TResult>().First();
        }
    }
}
