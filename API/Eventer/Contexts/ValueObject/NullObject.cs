namespace Eventer.Contexts.ValueObject
{
    public class NullObject<T>
    {
        public NullObject(T? objectToCheck)
        {
            if (objectToCheck == null) throw new Exception("Unknown object");
        }
    }
}
