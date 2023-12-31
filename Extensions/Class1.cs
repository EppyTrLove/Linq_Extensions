﻿using System;
using System.Drawing;
using static Extensions.LinqExtensions;

namespace Extensions
{
    public static class LinqExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"> source or predicate is null</exception>
        public static IEnumerable<TSource> Enshure<TSource>(this IEnumerable<TSource> source, Func<TSource,
            bool> predicate)
        {
            if (source == null)
                throw new ArgumentException();

            if (predicate == null)
                throw new ArgumentException();

            foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    yield return element;
                }
            }
        }
        public static IEnumerable<TSource> EnshureCount<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate, long count)
        {
            if (source == null)
                throw new ArgumentException();

            if (predicate == null)
                throw new ArgumentException();

            if (source.Count(predicate) != count)
                throw new Exception();

            return source;
        }
        public static bool Any<TSource>(this IEnumerable<TSource> source, Predicate<TSource> getter)
        {
            foreach (var item in source)
            {
                if(getter(item)) return true;
            }      
            return false;
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
        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source) => source == null || source.Any();

        public static IEnumerable<TSource> Apply<TSource>(this IEnumerable<TSource> source, Action<TSource> predicate)
        {
            foreach (TSource element in source)
            {
                predicate(element);
                yield return element;
            }

        }
        public static IEnumerable<TSource> Insert<TSource>(this IEnumerable<TSource> source, TSource obj,
            int position)
        {
            source.Insert(obj, position);
            yield return (TSource)source;
        }
        public static IEnumerable<TSource> Disorder<TSource>(this IEnumerable<TSource> source, Random rnd)

        //Fisher and Yates' original method
        {
            var buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = rnd.Next(i, buffer.Count);
                yield return buffer[j];
                buffer[j] = buffer[i];
            }
        }
        public static IEnumerable<IEnumerable<TSource>> Split<TSource>(this IEnumerable<TSource> source,
            TSource separator)
        {
            var list = new List<TSource>();
            var count = 0;
            foreach (var item in source)
            {
                if (list == null)
                    list = new List<TSource>();
                list[count++] = item;
                if (count.Equals(separator))
                    break;

                yield return list;
            }
        }
        public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int minIndex, int maxIndex)
        {
            var buffer = source.ToList();
            var list = new List<TSource>();
            for (int i = 0; i < buffer.Count; i++)
            {
                if (buffer.IndexOf(buffer[i]) <= maxIndex && buffer.IndexOf(buffer[i]) >= minIndex) { list.Add(buffer[i]); }
            }
            return list;
        }
        public static IEnumerable<TSource> Trace<TSource>(this IEnumerable<TSource> source, string line, 
            Action<TSource> predicate)
        {
            static void MyDelegeteMethod(TSource line)
            {
                Console.WriteLine(line);
            }
            predicate = MyDelegeteMethod;
            foreach (TSource element in source)
            {
                predicate(element);
                yield return element;
            }
        }
    }
}

        

