export function getTickets(token) {
  return fetch('/api/tickets', {
    headers: { Authorization: 'Bearer ' + token }
  })
}

export function createTicket(token, title, description) {
  const newTicket = { title: title, description: description }

  return fetch('/api/tickets', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + token
    },
    body: JSON.stringify(newTicket)
  })
}