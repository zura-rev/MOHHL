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

    const taskItemClasses = classNames('card-body', taskItem)
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

    //console.log('userId', filter.userId)

    const url = `/api/card?pageIndex=${pageIndex}&pageSize=${pageSize}
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
                        <div className='d-flex justify-content-between'>
                            <div className='d-flex justify-content-start'>
                                <div className='pe-3'><h4>{task.callId}</h4></div>
                                <table>
                                    <tbody>
                                        <tr>
                                            <th>თარიღი</th>
                                            <td>  {moment(task.call.createDate).format('LLLL')}</td>
                                        </tr>
                                        <tr>
                                            <th>მოქალაქე</th>
                                            <td>  {task.call.callAuthor}</td>
                                        </tr>
                                        <tr>
                                            <th>კარეგორია</th>
                                            <td>  {task.call.category.categoryName}</td>
                                        </tr>
                                        <tr>
                                            <th>ოპერატორი</th>
                                            <td>  {task.call.user.firstName}  {task.call.user.lastName}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div className='flex-column align-items-end'>
                                <div className='text-end'>{task?.call?.card?.user?.firstName}  {task?.call?.card?.user?.lastName}</div>
                                <div className='mt-5'>
                                    <h6 style={{ marginBottom: 0 }}>{task.status === 1 ?
                                        <span className='badge rounded-pill bg-success'>დასრულებული</span> :
                                        <span className='badge rounded-pill bg-danger'>დამუშავების პროცესში</span>}
                                    </h6>
                                </div>
                            </div>
                        </div>
                    </Link>
                </div>)
            }
        </>
    )
})