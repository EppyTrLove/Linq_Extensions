using System;

namespace Extensions
{
    public static class LinqExtensions
    {
        public static bool Enshure<TSource>(this IEnumerable<TSource> source, Func<TSource,
            bool> predicate)
        {
            var list = new List<TSource>();
            if (source == null)
            {
                throw new ArgumentException();
            }

            if (predicate == null)
            {
                throw new ArgumentException();
            }

            foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    return true;
                }
            }

            return false;
        }
        public static long EnshureCount<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentException();
            }

            if (predicate == null)
            {
                throw new ArgumentException();
            }

            long count = 0;
            foreach (TSource element in source)
            {

                if (predicate(element))
                {
                    count++;
                }
            }
            return count;
        }
        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(
                  this IEnumerable<TSource> source, int size)
        {
            TSource[] bucket = null;
            var count = 0;

            foreach (var item in source)
            {
                if (bucket == null)
                    bucket = new TSource[size];

                bucket[count++] = item;
                if (count == size)
                    break;

                yield return bucket;
            }
        }
        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null || source.Count() == 0)
            {
                return true;
            }
            return false;
        }


    }
}