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
  const { callsState, filterState } = useContext(StoreContext)
  const history = useHistory()
  const message = useMessage()

  const myclass = classNames(callList)

  const {
    filter: {
      fromDate,
      toDate,
      privateNumber,
      callAuthor,
      callNumber,
      category,
      phone,
      note,
    },
    submit,
    setSubmit,
  } = filterState

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
  } = callsState

  const { loading, request } = useHttp()

  const url = `/api/calls?pageIndex=${pageIndex}&pageSize=${pageSize}
      ${callNumber ? `&id=${callNumber}` : ''}
      ${phone ? `&phone=${phone}` : ''}
      ${privateNumber ? `&privateNumber=${privateNumber}` : ''}
      ${callAuthor ? `&callAuthor=${callAuthor}` : ''}
      ${category ? `&categoryId=${category.id}` : ''}
      ${note ? `&note=${note}` : ''}
      ${fromDate ? `&fromDate=${moment(fromDate).format('YYYY-MM-DD')}` : ''}
      ${toDate ? `&toDate=${moment(toDate).format('YYYY-MM-DD')}` : ''}`

  const fetchCalls = useCallback(async () => {
    try {
      const {
        data,
        headers: { totalcount, totalpages, pagesize, pageindex, hasnextpage },
      } = await request(url, 'GET', null, {
        Authorization: `Bearer ${user.token}`,
      })
      setCalls(data)
      setTotalCount(totalcount)
      setTotalPages(totalpages)
      setPageIndex(pageindex)
      setPageSize(pagesize)
      setHasNextPage(hasnextpage)
      setSubmit(false)
    } catch (error) { }
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

  return (
    <>
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
              <th>სტატუსი</th>
              <th>ოპერატორი</th>
            </tr>
          </thead>
          <tbody>
            {calls.map((call) => (
              <tr key={call.id} onClick={() => getCall(call.id)}>
                <td>
                  <FontAwesomeIcon
                    icon='flag'
                    color={
                      call.callStatus === 1
                        ? 'green'
                        : call.callStatus === 2
                          ? '#CCCC00'
                          : 'red'
                    }
                  />
                </td>
                <td>{call.privateNumber}</td>
                <td>{call.callAuthor}</td>
                <td>{call.phone}</td>
                <td>{moment(call.createDate).format('LLLL')}</td>
                <td>{call.category?.categoryName}</td>
                <td>
                  {call.callStatus === 1
                    ? 'დასრულებული'
                    : call.callStatus === 2
                      ? 'გასარკვევი'
                      : 'სასწრაფო'}
                </td>
                <td>
                  {call.user.firstName} {call.user.lastName}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </>
  )
})
