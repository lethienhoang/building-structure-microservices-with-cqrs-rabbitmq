using Newtonsoft.Json;
using System;

namespace Framework.Domain
{
    public class DomainException : Exception
    {
        public DomainException(string key, string message)
            : base(message)
        {
            Key = key;
        }

        public DomainException()
        {
        }

        public string Key { get; set; }
    }

    public static class Extensions
    {
        public static DomainException BuildNotExistException(this DomainException exception, string message)
        {
            return new DomainException("Not_Found", message);
        }
    }

    public class DomainExceptionContract
    {
        public string Key { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
