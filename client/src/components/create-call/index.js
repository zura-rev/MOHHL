import React, { useState, useContext, useReducer, useEffect } from 'react'
import { Button, Form, InputGroup, ButtonGroup, ToggleButton } from 'react-bootstrap'
import { useHistory } from 'react-router-dom'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { useHttp } from '../../hooks/http.hook'
import { useMessage } from '../../hooks/message.hook'
import { AuthContext } from '../../context/AuthProvider'
import { StoreContext } from '../../context/StoreProvider'
import { CategorySelect } from '../category-select'
import classNames from 'classnames'
import { createCallFrom } from './style.module.css'
import { faSave } from '@fortawesome/fontawesome-free-solid'

const initialState = {
  privateNumber: '',
  callAuthor: '',
  category: null,
  phone: '',
  note: '',
  callStatus: null,
}

function reducer(state, action) {
  const { type, payload } = action
  switch (type) {
    case 'change':
      return { ...state, ...payload }
  }
}

export function CreateCall() {
  const { token } = useContext(AuthContext)
  const { callsState } = useContext(StoreContext)
  const history = useHistory()
  const [state, dispatch] = useReducer(reducer, initialState)
  const { request, error, clearError } = useHttp()
  const message = useMessage()
  const { setMatchCalls } = callsState
  const [validated, setValidated] = useState(false)

  const [radioValue, setRadioValue] = useState('1');

  const radios = [
    { name: 'შესრულებული', value: '1' },
    { name: 'გასარკვევი', value: '2' },
    { name: 'საჩქარო', value: '3' },
  ];

  const mystyles = classNames('p-3', 'mr-2', 'card', 'col-md-6', createCallFrom)

  useEffect(() => {
    message(error)
    clearError()
  }, [error])

  const saveCall = async (event) => {
    event.preventDefault()
    const form = event.currentTarget
    if (form.checkValidity() === false) {
      event.stopPropagation()
    } else {
      const response = await request(
        '/api/calls',
        'POST',
        { ...state },
        {
          Authorization: `Bearer ${token}`,
        }
      )
      response && history.push(`/calls/${response.data}`)
    }
    setValidated(true)
  }

  const handleChange = (event) => {
    changeDispatch(event.target.name, event.target.value)
  }

  const handleSelectChange = (selected, nameOfComponent) => {
    changeDispatch(nameOfComponent.name, { id: selected.value })
  }

  const changeDispatch = (name, value) => {
    dispatch({
      type: 'change',
      payload: { ...state, [name]: value },
    })
  }

  const handleCheck = async () => {
    try {
      const response = await request(
        `/api/calls/matchcalls?phone=${state.phone}&privateNumber=${state.privateNumber}`,
        'GET',
        null,
        {
          Authorization: `Bearer ${token}`,
        }
      )
      setMatchCalls(response ? response.data : [])
    } catch (error) { }
  }

  return (
    <div className='col-md-6'>
      <div className='card'>
        <div className='card-body'>
          <h5>
            ზარის დამატება
          </h5>
          <hr />
          <Form onSubmit={saveCall} noValidate validated={validated}>
            <Form.Group className='mb-3'>
              <Form.Label>
                <strong>ტელეფონი</strong>
              </Form.Label>
              <InputGroup>
                <Form.Control
                  required
                  type='text'
                  placeholder='ტელეფონი'
                  id='phone'
                  name='phone'
                  value={state.phone}
                  onChange={handleChange}
                />
                <Button
                  variant='outline-secondary'
                  size='sm'
                  onClick={handleCheck}
                >
                  შემოწმება
                </Button>
              </InputGroup>
            </Form.Group>

            <Form.Group className='mb-3'>
              <Form.Label>
                <strong>პირადი ნომერი</strong>
              </Form.Label>
              <Form.Control
                type='text'
                placeholder='პირადი ნომერი'
                id='privateNumber'
                name='privateNumber'
                value={state.privateNumber}
                onChange={handleChange}
              />
              <Form.Text className='text-muted'></Form.Text>
            </Form.Group>

            <Form.Group className='mb-3'>
              <Form.Label>
                <strong>სახელი, გვარი</strong>
              </Form.Label>
              <Form.Control
                type='text'
                placeholder='სახელი, გვარი'
                id='callAuthor'
                name='callAuthor'
                value={state.callAuthor}
                onChange={handleChange}
              />
              <Form.Text className='text-muted'></Form.Text>
            </Form.Group>

            <Form.Group className='mb-3'>
              <Form.Label>
                <strong>კატეგორია</strong>
              </Form.Label>
              <CategorySelect
                required
                name='category'
                onChange={handleSelectChange}
              />
            </Form.Group>

            <Form.Group className='mb-3'>
              <Form.Label>
                <strong>სტატუსი</strong>
              </Form.Label>
              <Form.Select
                required
                as='select'
                id='callStatus'
                name='callStatus'
                value={state.callStatus || ''}
                onChange={handleChange}
              >
                <option value=''>აირჩეთ ...</option>
                <option value='1'>შესრულებული</option>
                <option value='2'>გასარკვევი</option>
                <option value='3'>საჩქარო</option>
              </Form.Select>
            </Form.Group>
            <Form.Group className='mb-4'>
              <Form.Label>
                <strong>აღწერა</strong>
              </Form.Label>
              <Form.Control
                required
                as='textarea'
                rows={3}
                type='textarea'
                placeholder='აღწერა'
                id='note'
                name='note'
                value={state.note}
                onChange={handleChange}
              />
              <Form.Text className='text-muted'></Form.Text>
            </Form.Group>

            <div className="d-grid gap-2">
              <Button variant="outline-success" type='submit'>
                <FontAwesomeIcon icon={faSave} className='me-2' />
                შენახვა
              </Button>
            </div>

          </Form>
        </div>
      </div>
    </div>
  )
}
