import React, { useState } from 'react'

export function PersonsInfo() {
  const [firstName, setFirstName] = useState('')
  const [lastName, setLastName] = useState('')
  const [position, setPositions] = useState('')
  const [privateNumber, setPrivateNumber] = useState('')
  const [persons, setPersons] = useState([])

  const inputChange = (event) => {
    const { name, value } = event.target
    if (name == 'firstName') {
      setFirstName(value)
    }
    if (name == 'lastName') {
      setLastName(value)
    }
    if (name == 'position') {
      setPositions(value)
    }
    if (name == 'privateNumber') {
      setPrivateNumber(value)
    }
  }

  const addPerson = (event) => {
    event.preventDefault()
    const person = { firstName, lastName, position, privateNumber }
    let personsList = persons.concat(person)
    setPersons(personsList)
    clearForm()
  }

  const validateForm = () => {
    if (firstName && lastName && position && privateNumber) {
      return false
    }
    return true
  }

  const clearForm = () => {
    setFirstName('')
    setLastName('')
    setPositions('')
    setPrivateNumber('')
  }

  const removePerson = (privateNumber) => {
    const result = persons.filter((x) => x.privateNumber !== privateNumber)
    setPersons(result)
  }

  return (
    <div className='card'>
      <div className='card-header'>
        <form onSubmit={addPerson} className='row'>
          <div className='col'>
            <input
              name='firstName'
              value={firstName}
              onChange={inputChange}
              className='form-control'
              placeholder='სახელი'
            />
          </div>
          <div className='col'>
            <input
              name='lastName'
              value={lastName}
              onChange={inputChange}
              className='form-control col'
              placeholder='გვარი'
            />
          </div>
          <div className='col'>
            <input
              name='position'
              value={position}
              onChange={inputChange}
              className='form-control col'
              placeholder='პოზიცია'
            />
          </div>
          <div className='col'>
            <input
              name='privateNumber'
              value={privateNumber}
              onChange={inputChange}
              className='form-control col'
              placeholder='პირადი ნომერი'
            />
          </div>
          <div className='col'>
            <button
              className='btn btn-primary btn-block'
              disabled={validateForm()}
            >
              დამატება
            </button>
          </div>
        </form>
      </div>
      <table className='table table-hower'>
        <tbody>
          {persons.map((item, index) => (
            <tr key={index}>
              <td>{item.firstName}</td>
              <td>{item.lastName}</td>
              <td>{item.position}</td>
              <td>{item.privateNumber}</td>
              <td>
                <button
                  className='btn btn-sm btn-danger float-right'
                  onClick={() => removePerson(item.privateNumber)}
                >
                  x
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}
