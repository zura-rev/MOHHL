import React, { useContext } from 'react'
import { NavLink } from 'react-router-dom'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

import { AuthContext } from '../../context/AuthProvider'
import { routes } from '../../routes'

export const Sidebar = () => {
  const { user } = useContext(AuthContext)
  return (
    <div className='sidebar'>
      <div className='logo'>
        {/* <img src={require('../../static/moh_logo.png')} width='80px' /> */}
        {/* <img src={require('../../static/hotline.png')} /> */}
        <img src={require("../../static/headset.jpg")} />
        <span>{user.firstName} {user.lastName}</span>
      </div>
      <hr />
      <ul>
        {
          routes.map(route => {
            if (route.permissons.includes(user.resources) && route.name) {
              return <li key={route.id}>
                <NavLink to={route.path}>
                  <FontAwesomeIcon icon={route.icon} className='me-2' />
                  {route.name}
                </NavLink>
              </li>
            }
          })
        }
      </ul>
    </div>
  )
}
