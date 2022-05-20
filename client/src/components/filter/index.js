import React, { useState } from 'react'
import { observer } from 'mobx-react-lite'
import DatePicker, { registerLocale } from 'react-datepicker'
import { format } from 'date-fns'
import ka from 'date-fns/locale/ka'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faTimes, faSearch, faBroom } from '@fortawesome/fontawesome-free-solid'
import { FilterDropdown } from './filter-dropdown'
import { FilterSelect } from './filter-select'
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

export const Filter = observer(({ filterProps, filterControls }) => {

    const { filter, changeFilter, setSubmit, clearFilter } = filterProps

    const [showSearchBar, setShowSearchBar] = useState(false)

    const handleFilterChange = (event) => {
        const { name, value } = event.target
        changeFilter({ ...filter, [name]: value })
    }

    const handleSelectChange = (selected, nameOfComponent) => {
        changeFilter({
            ...filter,
            [nameOfComponent.name]: { id: selected.value, label: selected.label },
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

        const controlType = filterControls.find(x => x.field === label)?.type

        if (controlType === 'CALENDAR') {
            value = format(new Date(value), 'dd/MM/yyyy')
        }

        if (controlType === 'SELECT') {
            value = item[1].label
        }

        if (controlType === 'DROPDOWN') {
            value = filterControls.find(x => x.field === label).data.find(x => x.id === Number(item[1]))?.name
        }

        return (
            <span key={index} className={searchBadge}>
                <label>{filterControls.find(x => x.field === label).placeholder}</label> = <span>{value.toString()}</span>
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

    const createSearchInputs = () => {
        return (
            <div className='form-group'>
                <div className='row'>
                    {
                        filterControls && filterControls.length > 0 && filterControls.map((item, index) => <div className='col-3 mb-3' key={index}>
                            {renderSearchBar(item)}
                        </div>)
                    }
                </div>
            </div>
        )
    }

    const createSearchTextInput = (item) => {
        return (
            <input
                id={item.field}
                className='form-control'
                type='text'
                name={item.field}
                value={filter[item.field]}
                onChange={handleFilterChange}
                placeholder={item.placeholder}
                autoComplete="off"
            />
        )
    }

    const createSearchCalendarInput = (item) => {
        return (
            <DatePicker
                id={item.field}
                locale='ka'
                placeholderText={item.placeholder}
                dateFormat='P'
                selected={filter[item.field]}
                value={filter[item.field]}
                name={item.field}
                className={dateControl}
                onChange={(date) =>
                    changeFilter({ ...filter, [item.field]: date })
                }
            />
        )
    }

    const createSearchSelectInput = (item) => {
        return (
            <FilterSelect
                id={item.field}
                name={item.field}
                value={filter[item.field]}
                url={item.url}
                onChange={handleSelectChange}
                placeholder={item.placeholder}
            />
        )
    }

    const createSearchDropdownInput = (item) => {
        return (
            <FilterDropdown
                id={item.field}
                name={item.field}
                value={filter[item.field]}
                data={item.data}
                onChange={handleFilterChange}
                placeholder={item.placeholder}
            />
        )
    }

    const renderSearchBar = (item) => {
        switch (item.type) {
            case 'TEXT':
                return createSearchTextInput(item)
            case 'SELECT':
                return createSearchSelectInput(item)
            case 'DROPDOWN':
                return createSearchDropdownInput(item)
            case 'CALENDAR':
                return createSearchCalendarInput(item)
            default:
        }
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
                    {createSearchInputs()}
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
