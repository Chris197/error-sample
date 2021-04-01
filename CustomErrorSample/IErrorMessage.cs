using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace CustomErrorSample
{
    public interface IErrorMessage : ICombine
    {
        IEnumerable<ICombine> AllErrors { get; }
    }



}


