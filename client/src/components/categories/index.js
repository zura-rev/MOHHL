import React, {
  useContext,
  useState,
  useEffect,
  useCallback,
  useReducer,
} from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { useHttp } from '../../hooks/http.hook'
import { AuthContext } from '../../context/AuthProvider'
import { StoreContext } from '../../context/StoreProvider'
import { observer } from 'mobx-react-lite'
import { Accordion, ListGroup, Button, Modal, Form } from 'react-bootstrap'
import { Loader } from '../loader'
import { accordionHeader, noRecord } from './style.module.css'
import { faPlus, faSave } from '@fortawesome/fontawesome-free-solid'


const initialState = {
  id: 0,
  categoryName: '',
  parentId: 0,
}

export function reducer(state = initialState, action) {
  const { type, payload } = action
  switch (type) {
    case 'CHANGE':
      return { ...state, ...payload }
    case 'CLEAR':
      return initialState
    default:
      break
  }
}

export const Categories = observer(() => {
  const { loading, request } = useHttp()
  const { user } = useContext(AuthContext)
  const { categoriesState } = useContext(StoreContext)
  const [show, setShow] = useState(false)
  const [check, setCheck] = useState(true)
  const [validated, setValidated] = useState(false)
  const [state, dispatch] = useReducer(reducer, initialState)

  const { categories, parentCategories, groupedCategories, setCategories } = categoriesState

  const fetchCategories = useCallback(async () => {
    try {
      const response = await request(`/api/categories`, 'GET', null, {
        Authorization: `Bearer ${user.token}`,
      })
      setCategories(response && response.data)
    } catch (error) { }
  }, [])

  useEffect(() => {
    fetchCategories()
  }, [])

  const handleChange = (event) => {
    const { name, value } = event.target
    dispatch({ type: 'CHANGE', payload: { ...state, [name]: value } })
  }

  const handleCheck = () => {
    setCheck(!check)
    dispatch({ type: 'CHANGE', payload: { ...state, parentId: 0 } })
  }

  const handleClose = () => {
    dispatch({ type: 'CLEAR' })
    setValidated(false)
    setShow(false)
    setCheck(true)
  }

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
          `/api/categories`,
          'POST',
          { ...state },
          {
            Authorization: `Bearer ${user.token}`,
          }
        )
        const res = categories.concat(response.data)
        setCategories(res)
        handleClose()
      } catch (error) { }
    }
  }

  if (loading) {
    return <Loader />
  }

  return (
    <>
      <Button size='sm' variant='outline-primary' onClick={handleShow}>
        <FontAwesomeIcon icon={faPlus} />
      </Button>
      <hr />
      {!groupedCategories.length ? <div className={noRecord}>ჩანაწერი ვერ მოიძებნა!</div> :
        <Accordion>
          {groupedCategories.map((item, i) => {
            return (item.options && item.options.lenght === 0) ?
              <div className='card card-danger'>ჩანაწერი ვერ მოიძებნა</div> :
              <Accordion.Item eventKey={item.label} key={item.label}>
                <Accordion.Header as='div' bsPrefix={accordionHeader}>{item.label}</Accordion.Header>
                <Accordion.Body>
                  <ListGroup variant="flush">
                    {
                      item.options.map((c) => <ListGroup.Item key={c.label}>{c.label}</ListGroup.Item>)
                    }
                  </ListGroup>
                </Accordion.Body>
              </Accordion.Item>
          })}
        </Accordion>}
      <Modal show={show} onHide={handleClose}>
        <Form onSubmit={handleSave} noValidate validated={validated}>
          <Modal.Header closeButton>
            <Modal.Title>კატეგორიის დამატება</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form.Group>
              <Form.Control type='hidden' name='id' id='_id' />
            </Form.Group>
            <Form.Group>
              <Form.Check
                type='checkbox'
                id='checkbox'
                label='მთავარი კატეგორიის შექმნა'
                onChange={handleCheck}
                value={check}
              />
            </Form.Group>
            {check ? (
              <Form.Group className='mb-3'>
                <Form.Group>
                  <Form.Label>
                    <strong>მთავარი კატეგორია</strong>
                  </Form.Label>
                  <Form.Control
                    id='parentId'
                    name='parentId'
                    required
                    as='select'
                    value={state.parentId || ''}
                    onChange={handleChange}
                  >
                    <option value=''>აირჩეთ ...</option>
                    {parentCategories &&
                      parentCategories.map((item) => (
                        <option key={item.id} value={item.id}>
                          {item.categoryName}
                        </option>
                      ))}
                  </Form.Control>
                </Form.Group>
              </Form.Group>
            ) : null}
            <Form.Group className='mb-3'>
              <Form.Label>
                <strong>კატეგორია</strong>
              </Form.Label>
              <Form.Control
                id='categoryName'
                name='categoryName'
                type='text'
                required
                placeholder='კატეგორიის დასახელება'
                value={state.categoryName}
                onChange={handleChange}
              />
            </Form.Group>
          </Modal.Body>
          <Modal.Footer>
            <Button type='submit' variant='primary' size='sm'>
              <FontAwesomeIcon icon={faSave} className='me-2' />
              შენახვა
            </Button>
          </Modal.Footer>
        </Form>
      </Modal>
    </>
  )
})
