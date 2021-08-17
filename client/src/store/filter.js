import { makeAutoObservable } from 'mobx'

const initialState = {
  fromDate: '',
  toDate: '',
  privateNumber: '',
  callAuthor: '',
  callNumber: '',
  phone: '',
  category: '',
  note: '',
}

export class FilterState {
  _filter = initialState
  _submit = false

  constructor() {
    makeAutoObservable(this)
  }

  changeFilter = (filter) => {
    this._filter = filter
  }

  clearFilter = () => {
    this._filter = initialState
  }

  setSubmit = (value) => {
    this._submit = value
  }

  get filter() {
    return this._filter
  }

  get submit() {
    return this._submit
  }
}


