# HelpDeskHQ Frontend

React frontend for the HelpDeskHQ helpdesk app. It is built with Vite.

## What it does

- Login page
- Create a ticket
- Show the list of tickets

## Run it locally

The API must be running on port 8080 first.

```bash
npm install
npm run dev
```

Vite forwards every `/api` request to `http://localhost:8080`, so the code only uses relative paths like `/api/tickets`.

## Build

```bash
npm run build
```

The build output goes to the `dist` folder. In Docker, nginx serves it (see `deploy/docker/Dockerfile.web`).

## Folder structure

- `src/components` - LoginForm, TicketForm, TicketList
- `src/pages` - TicketsPage
- `src/services` - all `fetch` calls (authService, ticketService)
- `src/styles` - CSS files
