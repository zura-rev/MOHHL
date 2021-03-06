import React from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faBackward, faForward, faFileExcel } from '@fortawesome/fontawesome-free-solid'
import { observer } from 'mobx-react-lite'

export const Paging = observer(({ pagerProps }) => {
  const {
    totalCount,
    totalPages,
    hasNextPage,
    pageIndex,
    pageSize,
    setPageSize,
    setPageIndex,
  } = pagerProps

  //console.log('hasNextPage', hasNextPage)

  return (
    <div className='d-flex justify-content-between'>
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
          style={{ width: '60px' }}
        >
          {
            [5, 10, 20, 50, 100, 200].map(item => {
              return <option key={item} value={item}>{item}</option>
            })
          }
        </select>
        {pageIndex > 1 ? (
          <button
            className='btn btn-outline-secondary '
            type='button'
            onClick={() => setPageIndex(Number(pageIndex) - 1)}
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
        {hasNextPage ? (
          <button
            className='btn btn-outline-secondary'
            type='button'
            onClick={() => setPageIndex(Number(pageIndex) + 1)}
          >
            <FontAwesomeIcon icon={faForward} />
          </button>
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


