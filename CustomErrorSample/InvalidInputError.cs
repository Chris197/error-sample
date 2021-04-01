using System.Collections.Generic;

namespace CustomErrorSample
{
    public class InvalidInputError : Error
    {
        public InvalidInputError(string prefix, string code, string fieldName, Dictionary<string, object> properties = null)
        {
            Prefix = prefix;            
            Code = code;
            FieldName = fieldName;
            _properties = properties ?? new Dictionary<string, object>();
            _properties.Add("fieldName", fieldName);
        }

        public string Prefix { get; }
        private Dictionary<string, object> _properties;
        public IReadOnlyDictionary<string, object> Properties => _properties;
        public string Code { get; }
        public string FieldName { get; }
        public string FullCode => Prefix + Code;
    }



}


