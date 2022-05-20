import React, { useContext } from 'react'
import { Button } from 'react-bootstrap'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faPlus } from '@fortawesome/fontawesome-free-solid'
import { StoreContext } from '../../context/StoreProvider'
import { CategoryAccordion } from './accordion'
import { AddCategoryModal } from './modal'


export const Categories = () => {

  const { categoriesState } = useContext(StoreContext)
  const { setCategoryModalShow, setCategoryEdit } = categoriesState
  const handleShow = () => {
    setCategoryModalShow(true)
    setCategoryEdit(false)
  }

  return (
    <>
      <Button size='sm' variant='outline-primary' onClick={handleShow}>
        <FontAwesomeIcon icon={faPlus} />
      </Button>
      <hr />
      <CategoryAccordion />
      <AddCategoryModal />
    </>
  )

}
