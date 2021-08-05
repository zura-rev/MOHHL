import React from 'react'

export const Loader = () => (
  <div
    style={{
      display: 'flex',
      justifyContent: 'center',
      paddingTop: '15%',
    }}
  >
    <div className='spinner-border' role='status'>
      <span className='sr-only'>Loading...</span>
    </div>
  </div>
)
