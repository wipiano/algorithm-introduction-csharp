using System;
using System.Collections.Generic;
using Xunit;

namespace AlgorithmIntroductionCsharp.GettingStarted
{
    /// <summary>
    /// 挿入ソート
    /// </summary>
    public class InsertionSort
    {
        [Fact(DisplayName = "挿入ソート 昇順")]
        public void OrderIntSeqAsc()
        {
            var source = new[] {1, 5, 3, 0, 2, 1, 3};
            Sort(source.AsSpan(), Order.Ascending);
            
            source.Is(new [] {0, 1, 1, 2, 3, 3, 5});
        }
        
        [Fact(DisplayName = "挿入ソート 降順")]
        public void OrderIntSeqDesc()
        {
            var source = new[] {1, 5, 3, 0, 2, 1, 3};
            Sort(source.AsSpan(), Order.Descending);
            
            source.Is(new [] {5, 3, 3, 2, 1, 1, 0});
        }

        private void Sort<T>(Span<T> sequence, Order order)
        {
            new SortMachine<T>(sequence, order).Sort();
        }
        
        private void Sort<T>(Span<T> sequence, Order order, IComparer<T> comparer)
        {
            new SortMachine<T>(sequence, order, comparer).Sort();
        }
        
        private ref struct SortMachine<T>
        {
            private readonly Span<T> _sequence;
            private readonly Order _order;
            private readonly IComparer<T> _comparer;
            private int _index;

            public SortMachine(Span<T> sequence, Order order) 
                : this(sequence, order, Comparer<T>.Default)
            {
            }
            
            public SortMachine(Span<T> sequence, Order order, IComparer<T> comparer) 
            {
                _sequence = sequence;
                _order = order;
                _comparer = comparer;
                _index = default; // てきとうでよい
            }

            public void Sort()
            {
                // 最初の要素は比較対象がないのでスキップして良い
                for (_index = 1; _index < _sequence.Length; _index++)
                {
                    // target は一時的にコピーしておかないと， _sequence[_index] は書き換わる可能性がある
                    var target = _sequence[_index];
                    var insertAt = ShiftAndSearchIndex();
                    _sequence[insertAt] = target;
                }
            }

            /*
             * 挿入場所を確保しつつ，挿入場所を探す
             * 昇順の場合，今回挿入する要素と(同じ|小さくなる)直前の点を探し，そこを挿入場所とする
             * 降順の場合，今回挿入する要素と(同じ|大きくなる)直前の点を探し，そこを挿入場所とする
             */
            private int ShiftAndSearchIndex()
            {
                var target = _sequence[_index];
                int i;
                for (i = _index - 1; i >= 0; i--)
                {
                    if (_order == Order.Ascending && !_sequence[i].GreaterThan(target, _comparer))
                    {
                        return i + 1;
                    }
                    
                    if (_order == Order.Descending && !_sequence[i].LessThan(target, _comparer))
                    {
                        return i + 1;
                    }
                    
                    ShiftElementToNext(i);
                }

                return 0;
            }

            private void ShiftElementToNext(int i)
            {
                _sequence[i + 1] = _sequence[i];
            }
        }

        private enum Order
        {
            Ascending,
            Descending
        }
    }
}