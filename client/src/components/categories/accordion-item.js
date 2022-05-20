import React, { useContext } from 'react'
import { ListGroup, Form } from 'react-bootstrap'
import { useHttp } from '../../hooks/http.hook'
import { AuthContext } from '../../context/AuthProvider'
import { StoreContext } from '../../context/StoreProvider'


export const AccordionItem = ({ ctg }) => {

    const { request } = useHttp()
    const { user } = useContext(AuthContext)
    const { categoriesState } = useContext(StoreContext)
    const { category, setCategory, categories, setCategories, setCategoryModalShow, setCategoryEdit } = categoriesState

    const chechStatus = (param) => {
        const _category = categories.map(category => {
            return (category.id === param.id) ? param : category
        })
        setCategories(_category)
    }

    const handleChange = async (id, status) => {
        console.log(id, status)
        const ctg = categories.find(category => category.id === id)
        const _category = { ...ctg, status: status ? -1 : 1 }
        //chechStatus(_category)
        try {
            const response = await request(`/api/categories`, 'PUT',
                { ..._category },
                {
                    Authorization: `Bearer ${user.token}`,
                }
            )
            chechStatus(response.data)
        } catch (error) { }
    }

    const handleClick = (event, id) => {
        if (event.target.type !== 'checkbox') {
            const _category = categories.find(category => category.id === id)
            setCategory(_category)
            setCategoryModalShow(true)
            setCategoryEdit(true)
        }
    }

    return <ListGroup.Item key={ctg.label} className='d-flex justify-content-between' onClick={(event) => handleClick(event, ctg.value)}>
        <label>{ctg.label}</label>
        <Form.Check
            type="switch"
            id={ctg.label}
            checked={Number(ctg.checked) === 1}
            onChange={() => handleChange(ctg.value, ctg.checked)}
        />
    </ListGroup.Item>
}