using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace fuzzymatchasaservice.core.Match
{
    public class MatchConfiguration
    {
        public MatcherCollection Matchers { get; set; }
    }

    public class MatcherCollection : Collection<IMatcher>
    {
        public void Add(IMatcher matcher)
        {
            this.Items.Add(matcher);
        }

        public IEnumerable<IMatcher<T>> GetMatchers<T>()
        {
            var matchers = this.Items.OfType<IMatcher<T>>();
            return matchers;
        }


        public IEnumerable<IMatcher> GetMatchers(Type t)
        {
            return this.Items.Where(t.IsInstanceOfType);
        }

    }

}