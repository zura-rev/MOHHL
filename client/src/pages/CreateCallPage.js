import React from 'react'
import { CreateCall } from '../components/create-call'
import { MatchCall } from '../components/match-call'

export const CreateCallPage = () => {
  return (
    <div className='row'>
      <CreateCall />
      <MatchCall />
    </div>
  )
}
