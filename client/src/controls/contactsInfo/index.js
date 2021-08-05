import React, { useState } from 'react'

export function ContactsInfo() {
  const [contactType, setContactType] = useState('')
  const [contactValue, setContactValue] = useState('')
  const [contacts, setContracts] = useState([])

  const inputChange = (event) => {
    if (event.target.name === 'contactType') {
      setContactType(event.target.value)
    }
    if (event.target.name === 'contactValue') {
      setContactValue(event.target.value)
    }
  }

  const addContact = () => {
    if (validateForm) {
      let contact = { contactType, contactValue }
      let contactsList = contacts.concat(contact)
      setContracts(contactsList)
      clearForm()
    }
  }

  const validateForm = () => {
    if (contactType && contactValue) {
      if (
        (contactType === 'მობილური ტელეფონი' ||
          contactType === 'ქალაქის ტელეფონი') &&
        vaildatePhoneNumber(contactValue)
      ) {
        return false
      }
      if (contactType === 'ელ.ფოსტა' && validateEmail(contactValue)) {
        return false
      }
      return true
    }
    return true
  }

  function validateEmail(email) {
    const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    return re.test(String(email).toLowerCase())
  }

  function vaildatePhoneNumber(phone) {
    const re = /^[-]?\d+$/
    return re.test(Number(phone))
  }

  const clearForm = () => {
    setContactType('')
    setContactValue('')
  }

  const removeContact = (contactValue) => {
    const result = contacts.filter((x) => x.contactValue !== contactValue)
    setContracts(result)
  }

  return (
    <>
      <div className='input-group'>
        <div className='input-group-prepend'>
          <select
            className='form-control'
            onChange={inputChange}
            name='contactType'
            value={contactType}
          >
            <option value='' selected>
              აირჩიეთ
            </option>
            <option value='ელ.ფოსტა'>ელ.ფოსტა</option>
            <option value='მობილური ტელეფონი'>მობილური ტელეფონი</option>
            <option value='ქალაქის ტელეფონი'>ქალაქის ტელეფონი</option>
          </select>
        </div>
        <input
          className='form-control'
          id='email'
          type='text'
          onChange={inputChange}
          name='contactValue'
          value={contactValue}
        />
        <div className='input-group-append'>
          <button
            type='button'
            className='btn btn-outline-secondary btn-sm'
            onClick={addContact}
            disabled={validateForm()}
          >
            დამატება
          </button>
        </div>
      </div>
      <table className='table'>
        <tbody>
          {contacts.map((item, index) => (
            <tr key={index}>
              <td>{item.contactType}</td>
              <td>{item.contactValue}</td>
              <td>
                <button
                  className='btn btn-sm btn-danger float-right'
                  onClick={() => removeContact(item.contactValue)}
                >
                  x
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  )
}
