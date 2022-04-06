import React, { useContext } from 'react'
import { CallList } from '../components/call-list'
import { Filter } from '../components/filter'
import { Paging } from '../components/paging'
import { StoreContext } from '../context/StoreProvider'
import { filterControls } from '../store/calls'

export const CallsPage = () => {
  const { callsState } = useContext(StoreContext)
  return (
    <>
      <div className='top-header'>
        <Filter filterProps={callsState} filterControls={filterControls} />
        <Paging pagerProps={callsState} />
      </div>
      <CallList />
    </>
  )
}
