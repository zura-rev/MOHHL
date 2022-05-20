import React, { useContext, useEffect } from 'react'
import { CreateCall } from '../components/create-call'
import { useHistory } from 'react-router-dom'
import { StoreContext } from '../context/StoreProvider'

export const CreateCallPage = () => {
  const history = useHistory()
  const { callState } = useContext(StoreContext)
  const { setCall, clearCall } = callState

  useEffect(() => {
    if (history.location.state) {
      const { id, privateNumber, callAuthor, category, phone, note, callType } = history.location.state
      setCall({ id, privateNumber, callAuthor, category, phone, note, callType })
    } else {
      clearCall()
    }
  }, [])

  return (
    <div className='row'>
      <CreateCall />
    </div>
  )

}
