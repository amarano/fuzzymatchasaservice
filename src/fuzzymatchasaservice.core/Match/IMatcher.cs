﻿using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace fuzzymatchasaservice.core.Match
{
    public interface IMatcher<T>
    {
        IDictionary<string, MatchRule> Rules { get; set; }
        T Target { get; set; }
        float Score(T o);
    }
}
