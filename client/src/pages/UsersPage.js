import React, { useContext } from 'react'
import { Users } from '../components/users'
import { Filter } from '../components/filter'
import { Paging } from '../components/paging'
import { StoreContext } from '../context/StoreProvider'

export function UsersPage() {
    const { usersState } = useContext(StoreContext)
    return (
        <>
            <div className='top-header'>
                <Filter filterProps={usersState} />
                <Paging pagerProps={usersState} />
            </div>
            <Users />
        </>
    )
}
