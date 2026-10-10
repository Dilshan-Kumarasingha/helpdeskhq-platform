import { useState } from 'react'
import { login } from '../services/authService'
import '../styles/LoginForm.css'

function LoginForm(props) {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [errorMessage, setErrorMessage] = useState('')

  function handleSubmit(event) {
    event.preventDefault()
    setErrorMessage('')

    login(email, password)
      .then(function (response) {
        if (response.ok) {
          response.json().then(function (data) {
            props.onLogin(data.token)
          })
        } else {
          setErrorMessage('Email or password is wrong.')
        }
      })
      .catch(function () {
        setErrorMessage('Cannot reach the server.')
      })
  }

  return (
    <form className="login-form" onSubmit={handleSubmit}>
      <h2>Login</h2>
      <input
        type="email"
        placeholder="Email"
        value={email}
        onChange={(event) => setEmail(event.target.value)}
      />
      <input
        type="password"
        placeholder="Password"
        value={password}
        onChange={(event) => setPassword(event.target.value)}
      />
      <button type="submit">Log in</button>
      <p className="error">{errorMessage}</p>
    </form>
  )
}

export default LoginForm