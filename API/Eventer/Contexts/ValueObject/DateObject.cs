namespace Eventer.Contexts.ValueObject
{
    public class DateObject
    {
        private DateTime now = DateTime.Now;

        public DateObject(DateTime date)
        {
            if (date < now) throw new Exception($"Date must be past current date ({now})");
        }
    }
}
