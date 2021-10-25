import React, { useContext } from 'react'
import { Tasks } from '../components/tasks'
import { Paging } from '../components/paging'
import { StoreContext } from '../context/StoreProvider'

export function TasksPage() {
    const { tasksState } = useContext(StoreContext)
    return (
        <>
            <Paging pagerProps={tasksState} />
            <br />
            <Tasks />
        </>
        )
    }
