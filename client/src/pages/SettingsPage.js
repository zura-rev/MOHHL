import React, { useContext } from 'react'
import { Users } from '../components/users'
import { Filter } from '../components/filter'
import { Paging } from '../components/paging'
import { StoreContext } from '../context/StoreProvider'
import { filterControls } from '../store/users'

export function SettingsPage() {
    const { usersState } = useContext(StoreContext)
    console.log('usersState', usersState)
    return (
        <>
            <div className='top-header'>
                <Filter filterProps={usersState} filterControls={filterControls} />
                <Paging pagerProps={usersState} />
            </div>
            <Users />
        </>
    )
}
