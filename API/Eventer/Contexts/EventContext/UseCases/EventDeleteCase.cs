using Eventer.Contexts.EventContext.Interfaces;

namespace Eventer.Contexts.EventContext.UseCases
{
    public class EventDeleteCase : IBaseCase
    {
        private readonly IEventRepository _eventRepository;

        public EventDeleteCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void Execute(int id)
        {
            try
            {
                if (!_eventRepository.IsInDatabase(id)) throw new Exception("could not find event!");

                _eventRepository.Delete(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
