import React, { useContext } from 'react'
import { Cards } from '../components/cards'
import { Paging } from '../components/paging'
import { StoreContext } from '../context/StoreProvider'
import { Filter } from '../components/filter'
import { filterControls } from '../store/cards'

export function CardsPage() {
    const { cardsState } = useContext(StoreContext)
    return (
        <>
            <div className='top-header'>
                <Filter filterProps={cardsState} filterControls={filterControls} />
                <Paging pagerProps={cardsState} />
            </div>
            <Cards />
        </>
    )
}
