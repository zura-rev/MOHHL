import React, { useContext, useEffect } from 'react'
import { observer } from 'mobx-react-lite'
import { StoreContext } from '../../context/StoreProvider'
import { MatchCallItem } from './match-call-item'

export const MatchCalls = observer(() => {
    const { callsState: { matchCalls, setMatchCalls } } = useContext(StoreContext)

    useEffect(() => {
        setMatchCalls([])
    }, [])

    const renderMatchItems = () => {
        return <div>
            {matchCalls.length > 0 ? matchCalls.map((call) => <MatchCallItem call={call} />) : <div className='text-center'>ჩანაწერი ვერ მოიძებნა!</div>}
        </div>
    }

    return <div>{renderMatchItems()}</div>
})

