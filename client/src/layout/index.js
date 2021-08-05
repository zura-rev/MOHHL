import React from 'react'
import { Navbar } from '../components/navbar'
import { Sidebar } from '../components/sidebar'

export function Layout({ children }) {
  return (
    <div className='content'>
      <Navbar />
      <Sidebar />
      {children}
    </div>
  )
}
