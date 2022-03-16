import React, { useContext } from 'react'
import { Tasks } from '../components/tasks'
import { Paging } from '../components/paging'
import { StoreContext } from '../context/StoreProvider'
//import { Filter } from '../components/task-filter'
import { Filter } from '../components/filter'
//import { topHeader } from './style.module.css'
import { filterControls } from '../store/tasks'

export function TasksPage() {
    const { tasksState } = useContext(StoreContext)
    return (
        <>
            <div className='top-header'>
                <Filter filterProps={tasksState} filterControls={filterControls} />
                <Paging pagerProps={tasksState} />
            </div>
            <Tasks />
        </>
    )
}
