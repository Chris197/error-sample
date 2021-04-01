using System.Collections.Generic;

namespace CustomErrorSample
{
    public static class MainErrors
    {
        private const string Prefix = "Main.";

        public static InvalidInputError RequiredFieldError(string fieldName)
            => new InvalidInputError(
                prefix: Prefix,
                code: "RequiredField",
                fieldName: fieldName);

        public static InvalidInputError MaxLengthExceededError(string fieldName, int maxLength)
            => new InvalidInputError(
                prefix: Prefix,
                code: "MaxLengthExceeded",
                fieldName: fieldName,
                properties: new Dictionary<string, object>
                {
                    ["maxLength"] = maxLength
                });
    }



}


