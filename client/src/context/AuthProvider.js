import React, { createContext } from 'react'
import { useAuth } from '../hooks/auth.hook'
export const AuthContext = createContext(null)

export const AuthProvider = ({ children }) => {
  const { login, logout, user } = useAuth()
  return (
    <AuthContext.Provider
      value={{
        login,
        logout,
        user
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}
