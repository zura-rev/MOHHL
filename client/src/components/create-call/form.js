import React, { useState, useContext, useEffect } from 'react'
import { observer } from 'mobx-react-lite'
import { Button, Form, InputGroup } from 'react-bootstrap'
import { useHistory } from 'react-router-dom'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { useMessage } from '../../hooks/message.hook'
import { AuthContext } from '../../context/AuthProvider'
import { StoreContext } from '../../context/StoreProvider'
import { CategorySelect } from '../category-select'
import { faSave, faSearch } from '@fortawesome/fontawesome-free-solid'
import { url } from '../../constants'


export const CreateCallFrom = observer(() => {

    const { user: { token } } = useContext(AuthContext)
    const { callsState: { getMatchCalls }, callState: { call, setCall, upsertCall, error } } = useContext(StoreContext)
    const history = useHistory()

    const [validated, setValidated] = useState(false)
    const message = useMessage()


    useEffect(() => {
        message(error)
    }, [error])

    const saveCall = async (event) => {
        event.preventDefault()
        const form = event.currentTarget
        if (!form.checkValidity()) {
            event.stopPropagation()
        } else {
            const callId = await upsertCall(token, call)
            callId && history.push(`${url}/calls/${callId}`)
        }
        setValidated(true)
    }

    const handleChange = (event) => {
        const { name, value } = event.target
        changeCall(name, value)
    }

    const changeCall = (name, value) => {
        setCall({ ...call, [name]: value })
    }

    const handleCheck = async (key, value) => {
        if (!value) {
            message('შეავსეთ აღნიშნული ველი!')
            return
        }
        try {
            await getMatchCalls(token, key, value)
        } catch (error) { }
    }

    return (
        <div className='create-call-form'>
            <h5>ზარის დამატება</h5>
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
                            value={call.phone}
                            onChange={handleChange}
                        />
                        <Button
                            variant='outline-secondary'
                            size='sm'
                            onClick={() => handleCheck('PHONE', call.phone)}
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
                            value={call.privateNumber}
                            onChange={handleChange}
                        />
                        <Button
                            variant='outline-secondary'
                            size='sm'
                            onClick={() => handleCheck('PN', call.privateNumber)}
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
                        value={call.callAuthor}
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
                            changeCall(nameOfComponent.name, { id: selected.value, categoryName: selected.label })
                        }}
                        value={call.category ? { value: call.category?.id, label: call.category?.categoryName } : null}
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
                        value={call.callType || ''}
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
                        value={call.note}
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
})
