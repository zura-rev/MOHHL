import React, { useContext } from 'react'
import { CallList } from '../components/call-list'
import { Filter } from '../components/filter'
import { Paging } from '../components/paging'
import { StoreContext } from '../context/StoreProvider'


export const CallsPage = () => {
  const { callsState } = useContext(StoreContext)
  return (
    <>
      <div
        style={{
          position: 'fixed',
          right: 0,
          left: 0,
          top: 45,
          marginLeft: '250px',
          background: '#f5f5f5',
          padding: '15px',
        }}
      >
        <Filter />
        <Paging pagerProps={callsState} />
      </div>
      <CallList />
    </>
  )
}
