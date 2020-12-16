using System.Collections.Generic;

namespace Day16
{
    internal class InputData
    {
		public Ticket MyTicket { get; } = new Ticket();
		public List<Ticket> NearbyTickets { get; } = new List<Ticket>();
		internal List<Field> Fields { get; } = new List<Field>();
    }
}