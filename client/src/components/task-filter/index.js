import React, { useState, useContext } from 'react'
import { observer } from 'mobx-react-lite'
import DatePicker, { registerLocale } from 'react-datepicker'
import { format } from 'date-fns'
import ka from 'date-fns/locale/ka'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faTimes, faSearch, faBroom } from '@fortawesome/fontawesome-free-solid'
import { StoreContext } from '../../context/StoreProvider'
import { CategorySelect } from '../category-select'
import 'react-datepicker/dist/react-datepicker.css'
import {
  searchInput,
  searchPanel,
  searchBadge,
  removeFilterItem,
  searchTitle,
  searchValues,
  dateControl
} from './style.module.css'

registerLocale('ka', ka)

const labelKa = {
  id: 'ბარათის ნომერი',
  callId: 'ზარის ნომერი',
  userId: 'მომხმარებელი',
  status: 'სტატუსი',
  category: 'კატეგორია',
  note: 'შინაარსი',
}

export const Filter = observer(() => {

  const { tasksState } = useContext(StoreContext)
  const { filter, changeFilter, setSubmit, clearFilter } = tasksState
  const [showSearchBar, setShowSearchBar] = useState(false)

  const handleFilterChange = (event) => {
    const { name, value } = event.target
    changeFilter({ ...filter, [name]: value })
  }

  const handleSelectChange = (selected, nameOfComponent) => {
    changeFilter({
      ...filter,
      category: { id: selected.value, label: selected.label },
    })
  }

  const filterArray = Object.entries(filter).filter((item) => item[1] !== '')

  const close = () => {
    setSubmit(true)
    setShowSearchBar(false)
  }

  const clear = () => {
    clearFilter()
    setSubmit(true)
  }

  const search = () => {
    setShowSearchBar(false)
    setSubmit(true)
  }

  const removeSearchItem = (event, filter) => {
    event.stopPropagation()
    changeFilter(filter)
    setSubmit(true)
  }

  const createSearchBadge = (item, index) => {
    let label = item[0]
    let value = item[1]

    if (label === 'fromDate' || label === 'toDate') {
      value = format(new Date(value), 'dd/MM/yyyy')
    }

    if (label === 'category') {
      value = item[1].label
    }

    return (
      <span key={index} className={searchBadge}>
        <label>{labelKa[label]}</label> = <span>{value.toString()}</span>
        <FontAwesomeIcon
          icon={faTimes}
          color='red'
          className={removeFilterItem}
          onClick={(event) =>
            removeSearchItem(event, { ...filter, [item[0]]: '' })
          }
        />
      </span>
    )
  }

  return (
    <>
      <div
        className={searchInput}
        onClick={() => setShowSearchBar(!showSearchBar)}
      >
        <div className={searchTitle}>
          <FontAwesomeIcon icon={faSearch} className='pr-2' />
          ფილტრი
        </div>
        <div className={searchValues}>
          {filterArray.map((item, index) => createSearchBadge(item, index))}
        </div>
      </div>
      {showSearchBar ? (
        <div className={searchPanel}>
          <div className='form-group mb-3'>
            <div className='row'>
              <div className='col'>
                <input
                  id='callNumber'
                  className='form-control'
                  type='number'
                  name='id'
                  value={filter.id}
                  onChange={handleFilterChange}
                  placeholder='ბარათის ნომერი'
                />
              </div>
              <div className='col'>
                <input
                  id='privateNumber'
                  className='form-control'
                  type='number'
                  name='callId'
                  value={filter.callId}
                  onChange={handleFilterChange}
                  placeholder='ზარის ნომერი'
                />
              </div>
              <div className='col'>
                <select name='status' className='form-select' onChange={handleFilterChange} value={filter.status}>
                  <option>აირჩიეთ</option>
                  <option value='-1'>დამუშავების პროცესში</option>
                  <option value='1'>დასრულებული</option>
                </select>
              </div>
            </div>
          </div>
          <div className='form-group'>
            <div className='row'>
              <div className='col'>
                <CategorySelect
                  required
                  name='category'
                  onChange={handleSelectChange}
                  value={filter.category}
                />
              </div>
              <div className='col'>
                <input
                  id='phone'
                  className='form-control'
                  type='text'
                  name='note'
                  value={filter.note}
                  onChange={handleFilterChange}
                  placeholder='შინაარსი'
                />
              </div>
            </div>
          </div>
          <hr />
          <div className='float-end'>
            <button
              type='button'
              className='btn btn-sm btn-outline-success ms-2 '
              onClick={search}
            >
              <FontAwesomeIcon icon={faSearch} />
            </button>
            <button
              className='btn btn-sm btn-outline-primary ms-2'
              onClick={clear}
            >
              <FontAwesomeIcon icon={faBroom} />
            </button>
            <button
              className='btn btn-sm btn-outline-danger ms-2'
              onClick={close}
            >
              <FontAwesomeIcon icon={faTimes} />
            </button>
          </div>
        </div>
      ) : null}
    </>
  )
})
