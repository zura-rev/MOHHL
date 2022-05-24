import { makeAutoObservable } from 'mobx'
import apiClient from '../apiClient'

const callState = {
    id: 0,
    privateNumber: '',
    callAuthor: '',
    category: null,
    phone: '',
    note: '',
    callType: null,
}

export class CallState {

    _call = callState
    _error = null

    constructor() {
        makeAutoObservable(this)
    }

    upsertCall = async (token, call) => {
        try {
            const callId = await apiClient.call.upsert(token, call)
            return callId
        } catch (error) {
            this._error = error
        }
    }

    setCall = (call) => {
        this._call = call
    }

    clearCall = () => {
        this._call = callState
    }

    get call() {
        return this._call
    }

}

