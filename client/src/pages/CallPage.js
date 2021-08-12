import React, { useCallback, useContext, useEffect, useState } from 'react'
import { useHistory, useParams, useRouteMatch } from 'react-router-dom'
import { useHttp } from '../hooks/http.hook'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faEdit, faPrint, faCheck, faChevronLeft, faTimes } from '@fortawesome/fontawesome-free-solid'
import { AuthContext } from '../context/AuthProvider'
import { Loader } from '../components/loader'
import { CallCard } from '../components/call-card'

export const CallPage = () => {
  const { user } = useContext(AuthContext)
  const { request, loading } = useHttp()
  const [call, setCall] = useState(null)
  const callId = useParams().id
  const history = useHistory()

  const getCall = useCallback(async () => {
    try {
      const { data } = await request(`/api/calls/${callId}`, 'GET', null, {
        Authorization: `Bearer ${user.token}`,
      })
      setCall(data)
    } catch (error) { }
  }, [user, callId, request])

  useEffect(() => {
    getCall()
  }, [getCall])

  if (loading) {
    return <Loader />
  }

  return (
    <>
      <div className='row'>
        <div className='col-md-4'>
          <button
            className='btn btn-sm btn-outline-dark'
            onClick={() => history.goBack()}
          >
            <FontAwesomeIcon icon={faChevronLeft} className='me-1' />
            უკან
          </button>
        </div>
        <div
          className='col-md-8'
          style={{ display: 'flex', justifyContent: 'flex-end' }}
        >
          <button
            className='btn btn-sm btn-outline-primary me-2'
            onClick={() => history.push('/contracts')}
          >
            <FontAwesomeIcon icon={faPrint} className='me-1' />
            ბეჭდვა
          </button>
          <button
            className='btn  btn-sm btn-outline-success me-2'
            onClick={() => history.push('/contracts')}
          >
            <FontAwesomeIcon icon={faCheck} className='me-1' />
            შესრულება
          </button>
          <button
            className='btn  btn-sm btn-outline-warning me-2'
            onClick={() => history.push('/contracts')}
          >
            <FontAwesomeIcon icon={faEdit} className='me-1' />
            რედაქტირება
          </button>
          {/* {user && user.resources === 'ROLE.ADMIN' ? */}
          <button
            className='btn  btn-sm btn-outline-danger '
            onClick={() => history.push('/contracts')}
          >
            <FontAwesomeIcon icon={faTimes} className='me-1' />
            წაშლა
          </button>
          {/* : null} */}
        </div>
      </div>
      <hr />
      {!loading && call && <CallCard call={call} />}
    </>
  )
}
