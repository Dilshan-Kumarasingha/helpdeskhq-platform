function TicketForm(props) {
  function handleSubmit(event) {
    event.preventDefault()
    props.onSubmit()
  }

  return (
    <form onSubmit={handleSubmit}>
      <h2>New ticket</h2>
      <input
        placeholder="Title"
        value={props.title}
        onChange={(event) => props.onTitleChange(event.target.value)}
      />
      <textarea
        placeholder="Description"
        value={props.description}
        onChange={(event) => props.onDescriptionChange(event.target.value)}
      />
      <button type="submit">Create ticket</button>
    </form>
  )
}

export default TicketForm