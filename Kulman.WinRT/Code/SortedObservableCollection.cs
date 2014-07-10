using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kulman.WinRT.Classes
{
    public class SortedObservableCollection<T> : ObservableCollection<T>
    {
        private readonly Func<T, int> _func;

        public SortedObservableCollection(Func<T, int> func)
        {
            _func = func;
        }

        public SortedObservableCollection(Func<T, int> func, IEnumerable<T> collection)
            : base(collection)
        {
            _func = func;
        }

        public SortedObservableCollection(Func<T, int> func, List<T> list)
            : base(list)
        {
            _func = func;
        }

        protected override void InsertItem(int index, T item)
        {
            bool added = false;
            for (int idx = 0; idx < Count; idx++)
            {
                if (_func(item) < _func(Items[idx]))
                {
                    base.InsertItem(idx, item);
                    added = true;
                    break;
                }
            }

            if (!added)
            {
                base.InsertItem(index, item);
            }
        }
    }
}
