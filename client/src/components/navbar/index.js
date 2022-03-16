import React, { useContext } from 'react'
import { AuthContext } from '../../context/AuthProvider'
import { useHttp } from '../../hooks/http.hook'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSignOutAlt } from '@fortawesome/fontawesome-free-solid'

export const Navbar = () => {
  const {
    logout,
    user: { userName, firstName, lastName, resources },
  } = useContext(AuthContext)

  const { request } = useHttp()

  const logoutHandler = async (event) => {
    event.preventDefault()
    try {
      await request(`/api/Accounts/logOut?userName=${userName}`, 'POST')
    } catch (error) { }
    logout()
  }

  return (
    <nav className='navbar fixed-top navbar-expand-lg navbar-light bg-primary'>
      <div
        className='collapse navbar-collapse'
        id='navbarNav'
        style={{ marginLeft: '250px' }}
      >

      </div>
      <div>
        <a
          className='btn btn-sm btn-outline-dark me-3'
          href='/'
          onClick={logoutHandler}
        >
          <FontAwesomeIcon icon={faSignOutAlt} />
        </a>
      </div>
    </nav>
  )
}
