namespace Eventer.Domain.ValueObject
{
    public class DateObject
    {
        private DateTime now = DateTime.Now;

        public DateObject(DateTime date)
        {
            if (date < now) throw new Exception($"Date cannot be in the past (now: {now})");
        }
    }
}
