import React, { useContext } from 'react'
import { CallList } from '../components/call-list'
import { Link } from 'react-router-dom'
import { Search } from '../components/search-bar'
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
        <Search />
        <Paging pagerProps={callsState} />
      </div>
      <CallList />
    </>
  )
}
