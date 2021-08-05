import React, { useEffect, useCallback, useContext } from 'react'
import { observer } from 'mobx-react-lite'
import moment from 'moment'
import { AuthContext } from '../../context/AuthProvider'
import { StoreContext } from '../../context/StoreProvider'
import { useHttp } from '../../hooks/http.hook'
import { Loader } from '../loader'
import { useHistory } from 'react-router-dom'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { useMessage } from '../../hooks/message.hook'
import classNames from 'classnames'

export const ExecutableCalls = observer(() => {

    const { user } = useContext(AuthContext)
    const { performerState } = useContext(StoreContext)

    const {
        performers,
        filter,
        submit,
        setSubmit,
        setPerformers,
        setTotalCount,
        setTotalPages,
        setPageIndex,
        setPageSize,
        setHasNextPage,
        pageIndex,
        pageSize
    } = performerState



    const { loading, request } = useHttp()

    const url = `/api/performers?pageIndex=${pageIndex}&pageSize=${pageSize}
        ${filter.id ? `&id=${filter.id}` : ''}
        ${filter.callId ? `&callId=${filter.callId}` : ''}
        ${filter.userId ? `&userId=${filter.userId}` : ''}
        ${filter.status ? `&status=${filter.status}` : ''}
        ${filter.note ? `&note=${filter.note}` : ''}`

    const fetchCalls = useCallback(async () => {
        try {
            const {
                data,
                headers: { totalcount, totalpages, pagesize, pageindex, hasnextpage },
            } = await request(url, 'GET', null, {
                Authorization: `Bearer ${user.token}`,
            })
            setPerformers(data)
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

    if (!performers.length) {
        return <div>ჩანაწერი ვერ მოიძებნა!</div>
    }

    return (
        <div className='card'>
            {
                performers && performers.map(p => <div>{p.callId}</div>)
            }
        </div>
    )
})