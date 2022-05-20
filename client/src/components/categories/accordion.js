import React, { useContext, useEffect, useCallback } from 'react'
import { observer } from 'mobx-react-lite'
import { Accordion, ListGroup } from 'react-bootstrap'
import { useHttp } from '../../hooks/http.hook'
import { AuthContext } from '../../context/AuthProvider'
import { StoreContext } from '../../context/StoreProvider'
import { Loader } from '../loader'
import { AccordionItem } from './accordion-item'
import { accordionHeader, noRecord } from './style.module.css'

export const CategoryAccordion = observer(() => {

    const { loading, request } = useHttp()
    const { user } = useContext(AuthContext)
    const { categoriesState } = useContext(StoreContext)

    const { groupedCategories, setCategories } = categoriesState
    
    const fetchCategories = useCallback(async () => {
        try {
            const response = await request(`/api/categories`, 'GET', null, {
                Authorization: `Bearer ${user.token}`,
            })
            console.log('category data', response.data)
            setCategories(response && response.data)
        } catch (error) { }
    }, [])

    useEffect(() => {
        fetchCategories()
    }, [])


    if (loading) {
        return <Loader />
    }

    if (!groupedCategories.length) {
        return <div className={noRecord}>ჩანაწერი ვერ მოიძებნა!</div>
    }

    return <Accordion>
        {groupedCategories.map((item, i) => {
            return (item.options && item.options.lenght === 0) ?
                <div className='card card-danger'>ჩანაწერი ვერ მოიძებნა</div> :
                <Accordion.Item eventKey={item.label} key={item.label}>
                    <Accordion.Header as='div' bsPrefix={accordionHeader}>{item.label}</Accordion.Header>
                    <Accordion.Body>
                        <ListGroup variant="flush">
                            {
                                item.options.map((category) => <AccordionItem ctg={category} />)
                            }
                        </ListGroup>
                    </Accordion.Body>
                </Accordion.Item>
        })}
    </Accordion>
})