import React, { useState, useContext } from 'react'
import { observer } from 'mobx-react-lite'
import { Modal, Form, Button } from 'react-bootstrap'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSave, faTrash } from '@fortawesome/fontawesome-free-solid'
import { AuthContext } from '../../context/AuthProvider'
import { StoreContext } from '../../context/StoreProvider'
import { useHttp } from '../../hooks/http.hook'

export const AddCategoryModal = observer(() => {

    const { request } = useHttp()
    const { user } = useContext(AuthContext)
    const { categoriesState } = useContext(StoreContext)

    const {
        category,
        setCategory,
        categories,
        setCategories,
        parentCategories,
        categoryModalShow,
        setCategoryModalShow,
        clearCategory,
        isCategoryEdit
    } = categoriesState

    const [check, setCheck] = useState(true)
    const [validated, setValidated] = useState(false)

    const handleChange = (event) => {
        const { name, value } = event.target
        setCategory({ ...category, [name]: value })
    }

    const handleCheck = () => {
        setCheck(!check)
        setCategory({ ...category, parentId: 0 })
    }

    const toggleStatus = () => {
        setCategory({ ...category, status: Number(!category.status) })
    }

    const handleClose = () => {
        clearCategory()
        setValidated(false)
        setCategoryModalShow(false)
        setCheck(true)
    }

    const handleSave = async (event) => {
        event.preventDefault()
        const form = event.currentTarget
        if (form.checkValidity() === false) {
            setValidated(true)
            event.stopPropagation()
        } else {
            await sendRequest(category)
        }
    }

    const handleRemove = async () => {
        const _category = { ...category, status: -1 }
        await sendRequest(_category)
    }

    const sendRequest = async (params) => {
        try {
            const response = await request(`/api/categories`, 'PUT',
                { ...params },
                {
                    Authorization: `Bearer ${user.token}`,
                }
            )
            const _category = categories.map(category => {
                return (category.id === response.data.id) ? response.data : category
            })
            setCategories(_category)
            handleClose()
        } catch (error) { }
    }

    return <Modal show={categoryModalShow} onHide={handleClose}>
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
                        type='switch'
                        id='parent-checkbox'
                        label='მთავარი კატეგორიის შექმნა'
                        onChange={handleCheck}
                        value={check}
                        disabled={isCategoryEdit}
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
                                value={category.parentId || ''}
                                onChange={handleChange}
                                className='form-select'
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
                        value={category.categoryName}
                        onChange={handleChange}
                        autoComplete='off'
                    />
                </Form.Group>
                <Form.Group className='mb-3'>
                    <Form.Label>
                        <strong>აღწერა</strong>
                    </Form.Label>
                    <Form.Control
                        id='note'
                        as='textarea'
                        name='note'
                        rows={3}
                        type='textarea'
                        placeholder='კატეგორიის აღწერა'
                        value={category.note}
                        onChange={handleChange}
                    />
                </Form.Group>
                <Form.Check
                    id='status'
                    name='status'
                    type="switch"
                    label='კატეგორიის გააქტიურება'
                    onChange={toggleStatus}
                    value={Number(category.status) === 1}
                    checked={Number(category.status) === 1}
                />
            </Modal.Body>
            <Modal.Footer>
                {/* {
                    isCategoryEdit ? <Button type='button' variant='danger' size='sm' onClick={handleRemove}>
                        <FontAwesomeIcon icon={faTrash} className='me-2' />
                        წაშლა
                    </Button> : null
                } */}
                <Button type='submit' variant='primary' size='sm'>
                    <FontAwesomeIcon icon={faSave} className='me-2' />
                    შენახვა
                </Button>
            </Modal.Footer>
        </Form>
    </Modal>
})