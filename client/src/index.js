import React from 'react'
import ReactDOM from 'react-dom'
import { AuthContext, AuthProvider } from './context/AuthProvider'
import { StoreProvider } from './context/StoreProvider'
import App from './App'
import 'bootstrap/dist/css/bootstrap.min.css'
import './index.css'

ReactDOM.render(
  <AuthProvider>
    <StoreProvider>
      <App />
    </StoreProvider>
  </AuthProvider>,
  document.getElementById('root')
)


