import { useState } from 'react'
import LoginForm from './components/LoginForm'
import TicketsPage from './pages/TicketsPage'

function App() {
  // The token stays in localStorage so a refresh does not log the user out.
  const [token, setToken] = useState(localStorage.getItem('token'))

  function handleLogin(newToken) {
    localStorage.setItem('token', newToken)
    setToken(newToken)
  }

  function handleLogout() {
    localStorage.removeItem('token')
    setToken(null)
  }

  if (token === null) {
    return (
      <div className="page">
        <h1>HelpDeskHQ</h1>
        <LoginForm onLogin={handleLogin} />
      </div>
    )
  }

  return (
    <div className="page">
      <h1>HelpDeskHQ</h1>
      <button onClick={handleLogout}>Log out</button>
      <TicketsPage token={token} onLogout={handleLogout} />
    </div>
  )
}

export default App