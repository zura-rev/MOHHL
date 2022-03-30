import React, { useCallback, useContext, useEffect, useState } from 'react'
import { useHistory, useParams, useRouteMatch } from 'react-router-dom'
import { useHttp } from '../hooks/http.hook'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faEdit, faPrint, faCheck, faChevronLeft, faTimes } from '@fortawesome/fontawesome-free-solid'
import { AuthContext } from '../context/AuthProvider'
import { Loader } from '../components/loader'
import { CallCard } from '../components/call-card'
import { Button, Modal, Form } from 'react-bootstrap'
import { faSave } from '@fortawesome/fontawesome-free-solid'


export const CallPage = () => {

  const { user } = useContext(AuthContext)
  const { request, loading } = useHttp()
  const [call, setCall] = useState(null)
  const callId = useParams().id
  const history = useHistory()
  const [show, setShow] = useState(false)
  const [validated, setValidated] = useState(false)
  const [note, setNote] = useState('')

  const getCall = useCallback(async () => {
    try {
      const { data } = await request(`/api/calls/${callId}`, 'GET', null, {
        Authorization: `Bearer ${user.token}`,
      })
      setCall(data)
    } catch (error) { }
  }, [user, callId, request])

  useEffect(() => {
    getCall()
  }, [getCall])

  const handleShow = () => setShow(true)

  const handleSave = async (event) => {
    event.preventDefault()
    const form = event.currentTarget
    if (form.checkValidity() === false) {
      setValidated(true)
      event.stopPropagation()
    } else {
      try {
        const response = await request(
          `/api/card`,
          'PUT',
          { id: callId, note },
          {
            Authorization: `Bearer ${user.token}`,
          }
        )
        if (response.status === 200) {
          //console.log('_response-exec_', response.data)
          const card = { ...call.card, ...response.data }
          setCall({ ...call, card })
          handleClose()
        }
      } catch (error) { }
    }
  }

  const handleClose = () => {
    //dispatch({ type: 'CLEAR' })
    setValidated(false)
    setShow(false)
  }

  if (loading) {
    return <Loader />
  }
  //console.log(user && user.userId, call && call.card.user.id)

  return (
    <>
      <div className='row'>
        <div className='col-md-4'>
          <button
            className='btn btn-sm btn-outline-dark'
            onClick={() => history.goBack()}
          >
            <FontAwesomeIcon icon={faChevronLeft} />
          </button>
        </div>
        <div
          className='col-md-8'
          style={{ display: 'flex', justifyContent: 'flex-end' }}
        >

          {user && user.resources === 'ROLE.SUPERVAISER' && user.userId === call?.card?.user.id && call?.card?.status === -1 ?
            <button
              className='btn  btn-sm btn-outline-success me-2'
              onClick={handleShow}
            >
              <FontAwesomeIcon icon={faCheck} />
            </button> : null}
          <button
            className='btn btn-sm btn-outline-primary'
            //onClick={() => history.push('/contracts')}
            type='button'
          >
            <FontAwesomeIcon icon={faPrint} />
          </button>
          {/* {user && user.resources === 'ROLE.ADMIN' ? */}
          {/* <button
            className='btn  btn-sm btn-outline-warning me-2'
            onClick={() => history.push('/createCall')}
          >
            <FontAwesomeIcon icon={faEdit} className='me-1' />
            რედაქტირება
          </button>
          <button
            className='btn  btn-sm btn-outline-danger '
            onClick={() => history.push('/contracts')}
          >
            <FontAwesomeIcon icon={faTimes} className='me-1' />
            წაშლა
          </button> */}
          {/* : null} */}
        </div>
      </div>
      <hr />
      {!loading && call && <CallCard call={call} />}
      <Modal show={show} onHide={handleClose}>
        <Form onSubmit={handleSave} noValidate validated={validated}>
          <Modal.Header closeButton>
            <Modal.Title>შესრულება</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form.Group>
              <Form.Control type='hidden' name='id' id='_id' />
            </Form.Group>

            <Form.Group className='mb-3'>
              <Form.Label>
                <strong>კომენტარი</strong>
              </Form.Label>
              <Form.Control as="textarea" rows={3}
                id='categoryName'
                name='note'
                required
                placeholder='კომენტარი'
                value={note}
                onChange={(e) => setNote(e.target.value)}
              />
            </Form.Group>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="outline-success" type='submit' size='sm'>
              <FontAwesomeIcon icon={faSave} className='me-2' />
              შენახვა
            </Button>
          </Modal.Footer>
        </Form>
      </Modal>
    </>
  )
}
