using System.Collections.Generic;

namespace AlgorithmIntroductionCsharp
{
    public static class ObjectExtensions
    {
        public static bool GreaterThan<T>(this T x, T y, IComparer<T> comparer) => comparer.Compare(x, y) > 0;
        public static bool LessThan<T>(this T x, T y, IComparer<T> comparer) => comparer.Compare(x, y) < 0;
        public static bool Equals<T>(this T x, T y, IComparer<T> comparer) => comparer.Compare(x, y) == 0;
        
        public static bool GreaterThan<T>(this T x, T y) => GreaterThan(x, y, Comparer<T>.Default);
        public static bool LessThan<T>(this T x, T y) => LessThan(x, y, Comparer<T>.Default);
        public static bool Equals<T>(this T x, T y) => Equals(x, y, Comparer<T>.Default);
    }
}