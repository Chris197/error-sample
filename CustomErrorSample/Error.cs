using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace CustomErrorSample
{
    public abstract class Error : IErrorMessage
    {
        private List<ICombine> _childeren = new List<ICombine>();

        public ICombine Combine(ICombine value)
        {
            _childeren.Add(value);
            return this;
        }

        public IEnumerable<ICombine> AllErrors => new ICombine[] { this }.Concat(_childeren);
    }

}


