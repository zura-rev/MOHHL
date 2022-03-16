import React, { useState, useContext } from 'react'
import { Button, Modal, Form } from 'react-bootstrap'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSave } from '@fortawesome/fontawesome-free-solid'
import { useHttp } from '../../hooks/http.hook'
import { StoreContext } from '../../context/StoreProvider'

export const UserModal = ({ modalShow, setModalShow, state, dispatch, token }) => {

    const { request } = useHttp()
    const [validated, setValidated] = useState(false)
    const { usersState } = useContext(StoreContext)
    const { setSubmit } = usersState

    const handleChange = (event) => {
        const { name, value } = event.target
        if (name === 'resourceId') {
            dispatch({ type: 'CHANGE', payload: { ...state, resources: [{ id: value }] } })
        }
        else dispatch({ type: 'CHANGE', payload: { ...state, [name]: value } })
    }

    const handleSave = async (event) => {
        event.preventDefault()
        const form = event.currentTarget
        if (form.checkValidity() === false) {
            setValidated(true)
            event.stopPropagation()
        } else {
            try {
                const response = await request(
                    `/api/users`,
                    'PUT',
                    { ...state },
                    {
                        Authorization: `Bearer ${token}`,
                    }
                )
                if (response.status === 200) {
                    handleClose()
                    setSubmit(true)
                    alert('მომხმარებელი დაემატა/განახლდა')
                }

            } catch (error) {
                alert(error)
            }
        }
    }

    const handleClose = () => {
        dispatch({ type: 'CLEAR' })
        setValidated(false)
        setModalShow(false)
    }

    return <Modal show={modalShow} onHide={handleClose} backdrop="static">
        <Form onSubmit={handleSave} noValidate validated={validated}>
            <Modal.Header closeButton>
                <Modal.Title>მომხმარებლის {state.edit ? 'რედაქტირება' : 'დამატება'}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form.Group>
                    <Form.Control type='hidden' name='id' id='_id' />
                </Form.Group>
                <Form.Group>
                    <Form.Label>
                        <strong>სახელი</strong>
                    </Form.Label>
                    <Form.Control
                        id='firstName'
                        name='firstName'
                        type='text'
                        label='სახელი'
                        onChange={handleChange}
                        value={state.firstName}
                        required
                    />
                </Form.Group>
                <Form.Group>
                    <Form.Label>
                        <strong>გვარი</strong>
                    </Form.Label>
                    <Form.Control
                        id='lastName'
                        name='lastName'
                        type='text'
                        label='გვარი'
                        onChange={handleChange}
                        value={state.lastName}
                        required
                    />
                </Form.Group>
                <Form.Group>
                    <Form.Label>
                        <strong>პირადი ნომერი</strong>
                    </Form.Label>
                    <Form.Control
                        id='privateNumber'
                        name='privateNumber'
                        type='text'
                        label='პირადი ნომერი'
                        onChange={handleChange}
                        value={state.privateNumber}
                        required
                    />
                </Form.Group>
                <Form.Group>
                    <Form.Label>
                        <strong>მომხმარებელი</strong>
                    </Form.Label>
                    {!state.edit ?
                        <Form.Control
                            id='userName'
                            name='userName'
                            type='text'
                            label='მომხმარებელი'
                            onChange={handleChange}
                            value={state.userName}
                            required
                        /> :
                        <Form.Control
                            id='userName'
                            name='userName'
                            type='text'
                            label='მომხმარებელი'
                            defaultValue={state.userName}
                            readOnly />}
                </Form.Group>
                {!state.edit ?
                    <Form.Group>
                        <Form.Label>
                            <strong>პაროლი</strong>
                        </Form.Label>
                        <Form.Control
                            id='password'
                            name='password'
                            type='password'
                            label='მომხმარებელი'
                            onChange={handleChange}
                            value={state.password}
                            required
                        />
                    </Form.Group> : null}
                <Form.Group>
                    <Form.Label>
                        <strong>როლები</strong>
                    </Form.Label>
                    <Form.Select
                        id='resourceId'
                        name='resourceId'
                        required
                        as='select'
                        value={state.resources[0]?.id || ''}
                        onChange={handleChange}
                    >
                        <option value=''>აირჩეთ ...</option>
                        <option value='1'>ადმინისტრატორი</option>
                        <option value='2'>სუპერვაიზერი</option>
                        <option value='3'>ოპერატორი</option>

                    </Form.Select>
                </Form.Group>

            </Modal.Body>
            <Modal.Footer>
                {/* <Button variant='danger' size='sm float-right' onClick={handleClose} >
                    გამორთვა
                </Button> */}
                <Button type='submit' variant='primary' size='sm'>
                    <FontAwesomeIcon icon={faSave} className='me-2' />
                    შენახვა
                </Button>
            </Modal.Footer>
        </Form>
    </Modal>
}