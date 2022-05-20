import { makeAutoObservable } from 'mobx'

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

    constructor() {
        makeAutoObservable(this)
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

