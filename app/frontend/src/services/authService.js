// The address has no host name, so the Vite proxy (and nginx later) can forward it.
export function login(email, password) {
  const loginData = { email: email, password: password }

  return fetch('/api/auth/login', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(loginData)
  })
}