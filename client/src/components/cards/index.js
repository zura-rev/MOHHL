import React, { useEffect, useCallback, useContext } from 'react'
import { observer } from 'mobx-react-lite'
import { Link } from 'react-router-dom'
import moment from 'moment'
import { AuthContext } from '../../context/AuthProvider'
import { StoreContext } from '../../context/StoreProvider'
import { useHttp } from '../../hooks/http.hook'
import { Loader } from '../loader'
import classNames from 'classnames'
import { cardItem, cardList, border, numberArea, noRecord } from './style.module.css'
import { url } from '../../constants'

export const Cards = observer(() => {

    const cardItemClasses = classNames('card-body', cardItem)
    const { user } = useContext(AuthContext)
    const { cardsState } = useContext(StoreContext)
    const {
        cards,
        filter,
        submit,
        setSubmit,
        setCards,
        setTotalCount,
        setTotalPages,
        setPageIndex,
        setPageSize,
        setHasNextPage,
        pageIndex,
        pageSize
    } = cardsState

    const { loading, request } = useHttp()


    const uri = `/api/card?pageIndex=${pageIndex}&pageSize=${pageSize}
        ${filter.id ? `&id=${filter.id}` : ''}
        ${filter.callId ? `&callId=${filter.callId}` : ''}
        ${filter.userId ? `&userId=${filter.userId}` : ''}
        ${filter.status ? `&status=${filter.status}` : ''}
        ${filter.note ? `&note=${filter.note}` : ''}
        ${filter.category ? `&categoryId=${filter.category.id}` : ''}`

    const fetchCalls = useCallback(async () => {
        try {
            const {
                data,
                headers: { totalcount, totalpages, pagesize, pageindex, hasnextpage },
            } = await request(uri, 'GET', null, {
                Authorization: `Bearer ${user.token}`,
            })
            setCards(data)
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

    if (!cards.length) {
        return <div className={noRecord}>ჩანაწერი ვერ მოიძებნა!</div>
    }

    return (
        <div className={cardList}>
            {
                cards && cards.map(card => <div key={card.id} className='card mb-2'>
                    <Link to={`${url}/calls/${card.callId}`} className={cardItemClasses}>
                        <div className='d-flex justify-content-between'>
                            <div className='d-flex justify-content-start'>
                                <div className={numberArea}>{card.callId}</div>
                                <div className={border}></div>
                                <table>
                                    <tbody>
                                        <tr>
                                            <th>ბარათის N</th>
                                            <td>{card.id}</td>
                                        </tr>
                                        <tr>
                                            <th>თარიღი</th>
                                            <td>{moment(card.call.createDate).format('LLLL')}</td>
                                        </tr>
                                        <tr>
                                            <th>მოქალაქე</th>
                                            <td>{card.call.callAuthor}</td>
                                        </tr>
                                        <tr>
                                            <th>კატეგორია</th>
                                            <td>{card.call.category.categoryName}</td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                            <div className='flex-column align-items-end'>
                                <h6 className='text-end'><span className='badge rounded-pill bg-secondary text-dark'>სუპერვაიზერი {card?.call?.card?.user?.firstName}  {card?.call?.card?.user?.lastName}</span></h6>
                                <h6 className='text-end'><span className='badge rounded-pill bg-secondary text-dark'>ოპერატორი {card.call.user.firstName}  {card.call.user.lastName}</span></h6>
                                <div className='text-end mt-2'>
                                    <h6 style={{ marginBottom: 0 }}>{card.status === 1 ?
                                        <span className='badge rounded-pill bg-success'>დასრულებული</span> :
                                        <span className='badge rounded-pill bg-danger'>დამუშავების პროცესში</span>}
                                    </h6>
                                </div>
                            </div>
                        </div>
                    </Link>
                </div>)
            }
        </div>
    )
})