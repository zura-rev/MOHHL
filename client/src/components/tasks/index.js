import React, { useEffect, useCallback, useContext } from 'react'
import { observer } from 'mobx-react-lite'
import { Link } from 'react-router-dom'
import moment from 'moment'
import { AuthContext } from '../../context/AuthProvider'
import { StoreContext } from '../../context/StoreProvider'
import { useHttp } from '../../hooks/http.hook'
import { Loader } from '../loader'
import { useHistory } from 'react-router-dom'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { useMessage } from '../../hooks/message.hook'
import classNames from 'classnames'
import { taskItem } from './style.module.css'

export const Tasks = observer(() => {

    const taskItemClasses = classNames('card-body d-flex justify-content-between', taskItem)
    const { user } = useContext(AuthContext)
    const { tasksState } = useContext(StoreContext)

    const {
        tasks,
        filter,
        submit,
        setSubmit,
        setTasks,
        setTotalCount,
        setTotalPages,
        setPageIndex,
        setPageSize,
        setHasNextPage,
        pageIndex,
        pageSize
    } = tasksState

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
            setTasks(data)
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

    if (!tasks.length) {
        return <div>ჩანაწერი ვერ მოიძებნა!</div>
    }

    return (
        <>
            {
                tasks && tasks.map(task => <div key={task.id} className='card mb-2'>
                    <Link to={`/calls/${task.callId}`} className={taskItemClasses}>
                        <ul>
                            <li><strong>N</strong>:{task.callId}</li>
                            <li><strong>სტატუსი</strong>:{task.status}</li>
                            <li><strong>თარიღი</strong>:{moment(task.call.createDate).format('LLLL')}</li>
                            <li><strong>მოქალაქე</strong>: {task.call.callAuthor}</li>
                            <li><strong>კარეგორია</strong>: {task.call.category.categoryName}</li>
                        </ul>
                        <div>{task.call.user.firstName}  {task.call.user.lastName}</div>
                    </Link>
                </div>)
            }
        </>
    )
})