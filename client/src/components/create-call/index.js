import React from 'react'
import { CreateCallFrom } from './form'
import { MatchCalls } from './match-calls'
import './style.css'

export function CreateCall() {
  return <div className='create-call'>
    <CreateCallFrom />
    <div className='match-calls-container'>
      <MatchCalls />
    </div>
  </div>
}