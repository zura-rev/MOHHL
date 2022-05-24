import React, { useContext, useEffect } from 'react'
import { observer } from 'mobx-react-lite'
import { StoreContext } from '../../context/StoreProvider'
import { MatchCallItem } from './match-call-item'

export const MatchCalls = observer(() => {
    const { callsState: { matchCalls, setMatchCalls } } = useContext(StoreContext)

    useEffect(() => {
        setMatchCalls([])
    }, [])

    return <div className='match-calls-container'>
        {matchCalls.length > 0 ? matchCalls.map((call) => <MatchCallItem key={call.id} call={call} />) : <div className='text-center'>ჩანაწერი ვერ მოიძებნა!</div>}
    </div>
})

