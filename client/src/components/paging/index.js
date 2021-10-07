import React, { useContext } from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faBackward, faForward, faFileExcel } from '@fortawesome/fontawesome-free-solid'
import { observer } from 'mobx-react-lite'
import { StoreContext } from '../../context/StoreProvider'


export const Paging = observer(() => {
  const { callsState } = useContext(StoreContext)
  const {
    totalCount,
    totalPages,
    hasNextPage,
    pageIndex,
    pageSize,
    setPageSize,
    setPageIndex,
  } = callsState

  return (
    <div className='d-flex justify-content-between '>
      <div
        className='input-group input-group-sm mt-3'
        disabled
        style={{ width: '470px', zIndex: 0 }}
      >
        <div className='form-control text-center' style={{ width: '270px' }}>
          <b>რაოდენობა</b> {totalCount} <b>/ გვერდი</b> {totalPages}
        </div>
        <select
          className='form-select'
          value={pageSize}
          onChange={(event) => setPageSize(event.target.value)}
          //onChange={(event) => setPager({ ...pager, pageSize: event.target.value })}
          style={{ width: '60px' }}
        >
          <option value='5'>5</option>
          <option value='10'>10</option>
          <option value='20'>20</option>
          <option value='50'>50</option>
          <option value='100'>100</option>
        </select>
        {pageIndex > 1 ? (
          <button
            className='btn btn-outline-secondary '
            type='button'
            onClick={() => setPageIndex(Number(pageIndex) - 1)}
          //onClick={() => setPager({ ...pager, pageIndex: Number(pageIndex) - 1 })}
          >
            <FontAwesomeIcon icon={faBackward} />
          </button>
        ) : null}
        <span
          className='form-control'
          style={{ width: '50px', textAlign: 'center' }}
        >
          {pageIndex}
        </span>
        {hasNextPage === 'True' ? (
          // <div className='input-group-append'>
          <button
            className='btn btn-outline-secondary'
            type='button'
            onClick={() => setPageIndex(Number(pageIndex) + 1)}
          //onClick={() => setPager({ ...pager, pageIndex: Number(pageIndex) + 1 })}
          >
            <FontAwesomeIcon icon={faForward} />
          </button>
          // </div>
        ) : null}
      </div>
      <div>
        <button className='btn btn-sm btn-outline-primary mt-3'>
          <FontAwesomeIcon icon={faFileExcel} className='me-1' />
          ექსელი
        </button>
      </div>
    </div>
  )
})
