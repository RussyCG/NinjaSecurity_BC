using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    class Event
    {
        private string message;
        private EventTypes eventType;
        private DateTime dateTimeEventOccured;

        public Event(string messageParam, EventTypes eventTypeParam, DateTime dateTimeEventOccuredParam)
        {
            this.Message = messageParam;
            this.EventType = eventTypeParam;
            this.DateTimeEventOccured = dateTimeEventOccuredParam;
        }

        public DateTime DateTimeEventOccured
        {
            get { return dateTimeEventOccured; }
            set { dateTimeEventOccured = value; }
        }

        public EventTypes EventType
        {
            get { return eventType; }
            set { eventType = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public override string ToString()
        {
            return string.Format("{0}: {1} @ {2}", this.EventType.ToString().ToUpper(), this.Message, this.DateTimeEventOccured);
        }
    }
}
