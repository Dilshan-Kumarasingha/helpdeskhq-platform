import '../styles/TicketList.css'

function TicketList(props) {
  return (
    <div>
      <h2>Tickets</h2>
      {props.tickets.map(function (ticket) {
        return (
          <div className="ticket" key={ticket.id}>
            <strong>{ticket.title}</strong>
            <p>{ticket.description}</p>
          </div>
        )
      })}
    </div>
  )
}

export default TicketList