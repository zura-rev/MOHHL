import { useState, useCallback, useContext } from 'react'
import { AuthContext } from '../context/AuthProvider'
import axios from 'axios'
import { apiurl } from '../constants'


export const useHttp = () => {

  const [loading, setLoading] = useState(false)
  const [error, setError] = useState('')
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
          url: `${apiurl}${url}`,
          headers,
          data: { ...body },
        })
        return response
      } catch (err) {
        console.log('err', err)
        if (err.response.status === 401) {
          logout()
        }
        if (err.response.status === 400) {
          setError(Object.values(err.response.data.errors).join(' '))
        }
      } finally {
        setLoading(false)
      }
    },
    []
  )

  const clearError = useCallback(() => setError(error), [error])

  return { loading, request, error, clearError }
}
