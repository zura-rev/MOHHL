import React, { useContext, useEffect, useState } from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { useHttp } from '../hooks/http.hook'
import { useMessage } from '../hooks/message.hook'
import { AuthContext } from '../context/AuthProvider'
import { faSignInAlt } from '@fortawesome/fontawesome-free-solid'

export const AuthPage = () => {
  const { login } = useContext(AuthContext)
  const message = useMessage()
  const { loading, request, error, clearError } = useHttp()

  //console.log('login')

  const [form, setForm] = useState({
    userName: '',
    password: '',
  })

  useEffect(() => {
    message(error)
    clearError()
  }, [error, message, clearError])

  const changeHandler = (event) => {
    const { name, value } = event.target
    setForm({ ...form, [name]: value })
  }

  const loginHandler = async () => {
    try {
      const { headers, data } = await request('/api/Accounts/logIn', 'POST', {
        ...form,
      })
      login(headers.accesstoken, data.id)
    } catch (error) { }
  }

  return (
    <div className='login'>
      <div className='card'>
        <div className='card-body'>
          <h3 className='text-center'>ავტორიზაცია</h3>
          <hr />
          <div className='form-group pt-3 pb-3'>
            {/* <label htmlFor='userName'>მომხმარებელი</label> */}
            <input
              placeholder='მომხმარებელი'
              id='userName'
              type='text'
              name='userName'
              className='form-control'
              value={form.userName}
              onChange={changeHandler}
            />
          </div>
          <div className='form-group pt-3 pb-3'>
            {/* <label htmlFor='email'>პაროლი</label> */}
            <input
              placeholder='პაროლი'
              id='password'
              type='password'
              name='password'
              className='form-control'
              value={form.password}
              onChange={changeHandler}
            />
          </div>
          <hr />
          <div className="d-grid gap-2">
            <button
              className='btn btn-sm btn-outline-primary'
              //disabled={loading}
              onClick={loginHandler}
            >
              <FontAwesomeIcon icon={faSignInAlt} className='me-2' />
              შესვლა
            </button>
          </div>
        </div>
      </div>
    </div>
  )
}
