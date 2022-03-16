import React, { useEffect, useCallback, useContext } from 'react'
import { StoreContext } from '../../context/StoreProvider'
import { useHttp } from '../../hooks/http.hook'
import { Loader } from '../loader'
import { observer } from 'mobx-react-lite'


export const UserTable = observer(({ setModalShow, dispatch, token }) => {
    const { usersState } = useContext(StoreContext)
    const { loading, request, error } = useHttp()
    const {
        users,
        pageIndex,
        pageSize,
        setUsers,
        setTotalCount,
        setTotalPages,
        setPageIndex,
        setPageSize,
        setHasNextPage,
        filter,
        submit,
        setSubmit,
    } = usersState

    const url = `/api/users?pageIndex=${pageIndex}&pageSize=${pageSize}
        ${filter.userName ? `&userName=${filter.userName}` : ''}
        ${filter.privateNumber ? `&privateNumber=${filter.privateNumber}` : ''}
        ${filter.firstName ? `&firstName=${filter.firstName}` : ''}
        ${filter.lastName ? `&lastName=${filter.lastName}` : ''}`

    const fetchUsers = useCallback(async () => {
        const {
            data,
            headers: { totalcount, totalpages, pagesize, pageindex, hasnextpage },
        } = await request(url, 'GET', null, { Authorization: `Bearer ${token}` })

        setUsers(data)
        setSubmit(false)
        setTotalCount(Number(totalcount))
        setTotalPages(Number(totalpages))
        setPageIndex(Number(pageindex))
        setPageSize(Number(pagesize))
        setHasNextPage(hasnextpage)
    }, [pageIndex, pageSize, submit])

    useEffect(() => {
        fetchUsers()
    }, [pageIndex, pageSize, submit])

    const editUser = (user) => {
        dispatch({ type: 'EDIT', payload: { ...user, edit: true } })
        setModalShow(true)
    }

    if (loading) {
        return <Loader />
    }

    return <table className='table table-hover'>
        <thead>
            <tr>
                {/* <th>id</th> */}
                <th>მომხმარებელი</th>
                <th>პირადი N</th>
                <th>სახელი</th>
                <th>გვარი</th>
                <th>რესურსები</th>
            </tr>
        </thead>
        <tbody>
            {users && users.map(user =>
                <tr key={user.id} onClick={() => editUser(user)}>
                    {/* <td>{user.id}</td> */}
                    <td>{user.userName}</td>
                    <td>{user.privateNumber}</td>
                    <td>{user.firstName}</td>
                    <td>{user.lastName}</td>
                    <td>
                        {user.resources?.map(item => item.name).join(', ')}
                    </td>
                </tr>
            )}
        </tbody>
    </table>
})