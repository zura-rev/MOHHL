import { makeAutoObservable } from 'mobx'

const filterState = {
  fromDate: '',
  toDate: '',
  privateNumber: '',
  callAuthor: '',
  callNumber: '',
  phone: '',
  category: '',
  note: '',
}

export class CallsState {

  _calls = []
  _totalCount = null
  _totalPages = null
  _pageIndex = 1
  _pageSize = 10
  _hasNextPage = false
  _matchCalls = []
  _filter = filterState
  _submit = false

  constructor() {
    makeAutoObservable(this)
  }

  setCalls = (calls) => {
    this._calls = calls
  }

  setMatchCalls = (matchCalls) => {
    this._matchCalls = matchCalls
  }

  setTotalCount = (totalCount) => {
    this._totalCount = totalCount
  }

  setTotalPages = (totalPages) => {
    this._totalPages = totalPages
  }

  setPageIndex = (pageIndex) => {
    this._pageIndex = pageIndex
  }

  setPageSize = (pageSize) => {
    this._pageSize = pageSize
  }

  setHasNextPage = (hasNextPage) => {
    this._hasNextPage = hasNextPage
  }

  get calls() {
    return this._calls
  }

  get pager() {
    return this._pager
  }

  get matchCalls() {
    return this._matchCalls
  }

  get totalCount() {
    return this._totalCount
  }

  get totalPages() {
    return this._totalPages
  }

  get pageIndex() {
    return this._pageIndex
  }

  get pageSize() {
    return this._pageSize
  }

  get hasNextPage() {
    return this._hasNextPage
  }


  changeFilter = (filter) => {
    this._filter = filter
  }

  clearFilter = () => {
    this._filter = filterState
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

