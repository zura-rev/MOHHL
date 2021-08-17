import { useState, useCallback, useContext } from 'react'
import { AuthContext } from '../context/AuthProvider'
import axios from 'axios'

export const useHttp = () => {
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState(null)
  const { logout } = useContext(AuthContext)

  const request = useCallback(
    async (url, method = 'GET', body = null, headers = {}) => {
      setLoading(true)
      if (!headers['content-type']) {
        headers['content-type'] = 'application/json'
      }
      try {
        const response = await axios({
          method,
          url,
          headers,
          data: { ...body },
        })
        setLoading(false)
        return response
      } catch (err) {
        if (err.response.status === 401) {
          logout()
        }
        if (err.response.status === 400) {
          setError(Object.values(err.response.data.errors).join(' '))
        }
      }
    },
    []
  )

  const clearError = useCallback(() => setError(error), [error])

  return { loading, request, error, clearError }
}
