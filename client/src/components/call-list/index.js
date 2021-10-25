import React, { useCallback, useContext, useEffect } from 'react'
import { observer } from 'mobx-react-lite'
import { AuthContext } from '../../context/AuthProvider'
import { StoreContext } from '../../context/StoreProvider'
import moment from 'moment'
import { useHttp } from '../../hooks/http.hook'
import { Loader } from '../loader'
import { useHistory } from 'react-router-dom'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { useMessage } from '../../hooks/message.hook'
import classNames from 'classnames'
import { callList, noRecord } from './style.module.css'



export const CallList = observer(() => {

  const { user } = useContext(AuthContext)
  const { callsState } = useContext(StoreContext)
  const history = useHistory()
  const message = useMessage()

  const myclass = classNames(callList)

  const {
    calls,
    pageIndex,
    pageSize,
    setCalls,
    setTotalCount,
    setTotalPages,
    setPageIndex,
    setPageSize,
    setHasNextPage,
    filter,
    submit,
    setSubmit,
  } = callsState

  const { loading, request } = useHttp()

  const url = `/api/calls?pageIndex=${pageIndex}&pageSize=${pageSize}
      ${filter.callNumber ? `&id=${filter.callNumber}` : ''}
      ${filter.phone ? `&phone=${filter.phone}` : ''}
      ${filter.privateNumber ? `&privateNumber=${filter.privateNumber}` : ''}
      ${filter.callAuthor ? `&callAuthor=${filter.callAuthor}` : ''}
      ${filter.category ? `&categoryId=${filter.category.id}` : ''}
      ${filter.note ? `&note=${filter.note}` : ''}
      ${filter.fromDate ? `&fromDate=${moment(filter.fromDate).format('YYYY-MM-DD')}` : ''}
      ${filter.toDate ? `&toDate=${moment(filter.toDate).format('YYYY-MM-DD')}` : ''}`

  const fetchCalls = useCallback(async () => {
    const {
      data,
      headers: { totalcount, totalpages, pagesize, pageindex, hasnextpage },
    } = await request(url, 'GET', null, { Authorization: `Bearer ${user.token}` })

    //const p = { totalCount: totalcount, totalPages: totalpages, pageSize: pagesize, pageIndex: pageindex, hasNextPage: hasnextpage }

    setCalls(data)
    setSubmit(false)
    setTotalCount(Number(totalcount))
    setTotalPages(Number(totalpages))
    setPageIndex(Number(pageindex))
    setPageSize(Number(pagesize))
    setHasNextPage(hasnextpage)

  }, [pageIndex, pageSize, submit])

  useEffect(() => {
    fetchCalls()
  }, [pageIndex, pageSize, submit])

  if (loading) {
    return <Loader />
  }

  if (!calls.length) {
    return <div className={noRecord}>ჩანაწერი ვერ მოიძებნა!</div>
  }

  const getCall = (id) => {
    history.push(`/calls/${id}`)
  }

  const getCallStatus = (call) => {
    if (call.callType === 2) {
      return <FontAwesomeIcon
        icon='flag'
        color={call?.card?.status === 0 ? 'red' : 'green'}
      />
    }
    return null
  }
  return (
    <div className={myclass}>
      <table className='table table-hover'>
        <thead>
          <tr>
            <th></th>
            <th>პირადი ნომერი</th>
            <th>სახელი, გვარი</th>
            <th>ტელეფონი</th>
            <th>თარიღი</th>
            <th>კატეგორია</th>
            <th>ზარის ტიპი</th>
            <th>ოპერატორი</th>
          </tr>
        </thead>
        <tbody>
          {calls.map((call) => (
            <tr key={call.id} onClick={() => getCall(call.id)}>
              <td>
                {getCallStatus(call)}
              </td>
              <td>{call.privateNumber}</td>
              <td>{call.callAuthor}</td>
              <td>{call.phone}</td>
              <td>{moment(call.createDate).format('LLLL')}</td>
              <td>{call.category?.categoryName}</td>
              <td>
                {call.callType === 1
                  ? 'კონსულტაცია'
                  : 'ბარათი'}
              </td>
              <td>
                {call.user.firstName} {call.user.lastName}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )

})

