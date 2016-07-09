using System;
using System.Collections.Generic;

namespace fuzzymatchasaservice.core.Match
{
    public abstract class DataSource<T>
    {
        public abstract IEnumerable<T> GetItems(Func<T, bool> predicate);
    }
}