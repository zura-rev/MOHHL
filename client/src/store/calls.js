import { makeAutoObservable } from 'mobx'

export class CallsState {
  _calls = []
  _totalCount = null
  _totalPages = null
  _pageIndex = 1
  _pageSize = 10
  _hasNextPage = false
  _matchCalls = []

  constructor() {
    makeAutoObservable(this)
  }

  setCalls = (calls) => {
    this._calls = calls
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

  setMatchCalls = (matchCalls) => {
    this._matchCalls = matchCalls
  }

  get calls() {
    return this._calls
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

  get matchCalls() {
    return this._matchCalls
  }
}
