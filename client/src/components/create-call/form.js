import React, { useState, useContext, useEffect } from 'react'
import { Button, Form, InputGroup } from 'react-bootstrap'
import { useHistory } from 'react-router-dom'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { useHttp } from '../../hooks/http.hook'
import { useMessage } from '../../hooks/message.hook'
import { AuthContext } from '../../context/AuthProvider'
import { StoreContext } from '../../context/StoreProvider'
import { CategorySelect } from '../category-select'
import classNames from 'classnames'
//import { createCallFrom } from './style.module.css'
import { faSave, faSearch } from '@fortawesome/fontawesome-free-solid'
import { useCreateCallReducer } from './reducer'
import { url } from '../../constants'



export function CreateCallFrom() {
    const { user: { token } } = useContext(AuthContext)
    const { callsState } = useContext(StoreContext)
    const history = useHistory()
    //const [state, dispatch] = useReducer(createCallReducer, initialState)
    const [state, dispatch] = useCreateCallReducer({
        privateNumber: '',
        callAuthor: '',
        category: null,
        phone: '',
        note: '',
        callType: null,
        //userId: 1
    })
    const { request, error, clearError } = useHttp()
    const message = useMessage()
    const { setMatchCalls } = callsState
    const [validated, setValidated] = useState(false)


    useEffect(() => {
        message(error)
        clearError()
    }, [error])

    const saveCall = async (event) => {
        event.preventDefault()
        const form = event.currentTarget
        if (!form.checkValidity()) {
            event.stopPropagation()
        } else {
            const response = await request(
                `/api/calls`,
                'POST',
                { ...state },
                {
                    Authorization: `Bearer ${token}`,
                }
            )
            response && history.push(`${url}/calls/${response.data}`)
        }
        setValidated(true)
    }

    const handleChange = (event) => {
        changeDispatch(event.target.name, event.target.value)
    }

    const changeDispatch = (name, value) => {
        dispatch({
            type: 'CHANGE',
            payload: { ...state, [name]: value },
        })
    }

    const handleCheck = async (key, value, top = 5) => {
        // `/api/calls/matchcalls?phone=${state.phone}&privateNumber=${state.privateNumber}&topValue=10`,
        if (!value) {
            //alert('შეავსეთ აღნიშნული ველი!')
            message('შეავსეთ აღნიშნული ველი!')
            return
        }
        const url = `/api/calls/matchcalls?${(key === 'PHONE') ? `phone=${value}` : (key === 'PN') ? `privateNumber=${state.privateNumber}` : null}&topValue=${top}`
        try {
            const response = await request(
                url,
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
        <div className='create-call-form'>
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
                            onClick={() => handleCheck('PHONE', state.phone)}
                        >
                            <FontAwesomeIcon icon={faSearch} />
                        </Button>
                    </InputGroup>
                </Form.Group>
                <Form.Group className='mb-3'>
                    <Form.Label>
                        <strong>პირადი ნომერი</strong>
                    </Form.Label>
                    <InputGroup>
                        <Form.Control
                            type='text'
                            placeholder='პირადი ნომერი'
                            id='privateNumber'
                            name='privateNumber'
                            value={state.privateNumber}
                            onChange={handleChange}
                        />
                        <Button
                            variant='outline-secondary'
                            size='sm'
                            onClick={() => handleCheck('PN', state.privateNumber)}
                        >
                            <FontAwesomeIcon icon={faSearch} />
                        </Button>
                    </InputGroup>
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
                </Form.Group>
                <Form.Group className='mb-3'>
                    <Form.Label>
                        <strong>კატეგორია</strong>
                    </Form.Label>
                    <CategorySelect
                        required
                        name='category'
                        onChange={(selected, nameOfComponent) => {
                            changeDispatch(nameOfComponent.name, { id: selected.value })
                        }}
                    />
                </Form.Group>
                <Form.Group className='mb-3'>
                    <Form.Label>
                        <strong>ზარის ტიპი</strong>
                    </Form.Label>
                    <Form.Select
                        required
                        as='select'
                        id='callType'
                        name='callType'
                        value={state.callType || ''}
                        onChange={handleChange}
                    >
                        <option value=''>აირჩეთ ...</option>
                        <option value='1'>კონსულტაცია</option>
                        <option value='2'>ბარათი</option>
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
    )
}
