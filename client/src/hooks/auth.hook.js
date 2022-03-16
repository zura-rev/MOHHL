import { useState } from 'react'
import jwt_decode from 'jwt-decode'
import { useLocalStorage } from './localStorage.hook'
const storageName = 'userData'

export const useAuth = () => {

  //const userData = JSON.parse(localStorage.getItem(storageName))
  const [user, setUser] = useLocalStorage(storageName)
  //console.log('user', user)
  //const [user, setUser] = useState(userData)

  const login = (token, userId) => {

    const decode = jwt_decode(token)

    const {
      UserName: userName,
      FirstName: firstName,
      LastName: lastName,
      PrivateNumber: privateNumber,
      resources,
    } = decode

    setUser({
      userId,
      userName,
      firstName,
      lastName,
      privateNumber,
      resources,
      token
    })
  }

  const logout = () => {
    localStorage.removeItem(storageName)
    setUser(null)
  }

  return { login, logout, user }
}
