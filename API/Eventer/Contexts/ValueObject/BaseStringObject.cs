namespace Eventer.Contexts.ValueObject
{
    public class BaseStringObject
    {
        public string Value { get; set; }

        public BaseStringObject(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new Exception("event's data must be filled");
            Value = value;
        }
    }
}
