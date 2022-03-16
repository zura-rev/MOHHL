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
  fromDate: 'თარიღიდან',
  toDate: 'თარიღამდე',
  privateNumber: 'პირადი N',
  callAuthor: 'ზარის ავტორი',
  callNumber: 'ზარის ნომერი',
  phone: 'ტელეფონი',
  category: 'კატეგორია',
  note: 'ზარის შინაარსი',
}

export const Filter = observer(() => {

  const { callsState } = useContext(StoreContext)
  const { filter, changeFilter, setSubmit, clearFilter } = callsState
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
    // clearFilter()
    setSubmit(true)
    setShowSearchBar(false)
  }

  const clear = () => {
    clearFilter()
    setSubmit(true)
    //setPageIndex(1);
    //setPageSize(5);
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
              <div className='col-3 d-flex justify-content-between'>
                <div className='pe-4'>
                  <DatePicker
                    id='fromDate'
                    locale='ka'
                    placeholderText='თარიღიდან'
                    dateFormat='P'
                    selected={filter.fromDate}
                    value={filter.fromDate}
                    name='fromDate'
                    className={dateControl}
                    onChange={(date) =>
                      changeFilter({ ...filter, ['fromDate']: date })
                    }
                  />
                </div>
                <div>
                  <DatePicker
                    id='toDate'
                    locale='ka'
                    placeholderText='თარიღამდე'
                    dateFormat='P'
                    selected={filter.toDate}
                    value={filter.toDate}
                    name='toDate'
                    className={dateControl}
                    onChange={(date) =>
                      changeFilter({ ...filter, ['toDate']: date })
                    }
                  />
                </div>
              </div>
              <div className='col-3'>
                <input
                  id='callNumber'
                  className='form-control'
                  type='number'
                  name='callNumber'
                  value={filter.callNumber}
                  onChange={handleFilterChange}
                  placeholder='ზარის ნომერი'
                />
              </div>
              <div className='col'>
                <CategorySelect
                  required
                  name='category'
                  onChange={handleSelectChange}
                  value={filter.category}
                />
              </div>
            </div>
          </div>
          <div className='form-group'>
            <div className='row'>
              <div className='col'>
                <input
                  id='privateNumber'
                  className='form-control'
                  type='text'
                  name='privateNumber'
                  value={filter.privateNumber}
                  onChange={handleFilterChange}
                  placeholder='პირადი ნომერი'
                />
              </div>
              <div className='col'>
                <input
                  id='callAuthor'
                  className='form-control'
                  type='text'
                  name='callAuthor'
                  value={filter.callAuthor}
                  onChange={handleFilterChange}
                  placeholder='სახელი, გვარი'
                />
              </div>
              <div className='col'>
                <input
                  id='phone'
                  className='form-control'
                  type='text'
                  name='phone'
                  value={filter.phone}
                  onChange={handleFilterChange}
                  placeholder='ტელეფონი'
                />
              </div>
              <div className='col'>
                <input
                  id='note'
                  className='form-control'
                  type='text'
                  name='note'
                  value={filter.note}
                  onChange={handleFilterChange}
                  placeholder='აღწერა'
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
