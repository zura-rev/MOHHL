import { useState, useCallback, useEffect } from 'react'
import jwt_decode from 'jwt-decode'

const storageName = 'userData'

export const useAuth = () => {

  const [user, setUser] = useState(null)

  const login = useCallback((jwtToken, id) => {

    const decode = jwt_decode(jwtToken)
    const {
      UserName: userName,
      FirstName: firstName,
      LastName: lastName,
      PrivateNumber: privateNumber,
      resources,
    } = decode

    localStorage.setItem(
      storageName,
      JSON.stringify({
        userId: id,
        userName,
        firstName,
        lastName,
        privateNumber,
        resources,
        token: jwtToken
      })
    )

    setUser({
      userId: id,
      userName,
      firstName,
      lastName,
      privateNumber,
      resources,
      token: jwtToken
    })

  }, [])

  const logout = useCallback(() => {
    setUser(null)
    localStorage.removeItem(storageName)
  }, [])

  useEffect(() => {
    const data = JSON.parse(localStorage.getItem(storageName))
    if (data && data.token) {
      login(data.token, data.userId)
    }
  }, [login])

  return { login, logout, user }
}
