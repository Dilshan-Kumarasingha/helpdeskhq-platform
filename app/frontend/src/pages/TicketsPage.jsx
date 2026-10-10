import { useState, useEffect } from 'react'
import TicketForm from '../components/TicketForm'
import TicketList from '../components/TicketList'
import { getTickets, createTicket } from '../services/ticketService'

function TicketsPage(props) {
  const [tickets, setTickets] = useState([])
  const [title, setTitle] = useState('')
  const [description, setDescription] = useState('')
  const [errorMessage, setErrorMessage] = useState('')

  function loadTickets() {
    getTickets(props.token)
      .then(function (response) {
        // 401 means the token is old or wrong, so the user has to log in again.
        if (response.status === 401) {
          props.onLogout()
        } else {
          response.json().then(function (data) {
            setTickets(data)
          })
        }
      })
      .catch(function () {
        setErrorMessage('Cannot reach the server.')
      })
  }

  useEffect(function () {
    loadTickets()
  }, [])

  function handleSubmit() {
    setErrorMessage('')

    createTicket(props.token, title, description)
      .then(function (response) {
        if (response.ok) {
          setTitle('')
          setDescription('')
          loadTickets()
        } else {
          setErrorMessage('Could not create the ticket. Check the title and description.')
        }
      })
      .catch(function () {
        setErrorMessage('Cannot reach the server.')
      })
  }

  return (
    <div>
      <TicketForm
        title={title}
        description={description}
        onTitleChange={setTitle}
        onDescriptionChange={setDescription}
        onSubmit={handleSubmit}
      />
      <p className="error">{errorMessage}</p>
      <TicketList tickets={tickets} />
    </div>
  )
}

export default TicketsPage