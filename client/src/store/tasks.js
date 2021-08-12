import { makeAutoObservable } from 'mobx'

const filterState = {
  id: '',
  callId: '',
  userId: '',
  status: '',
  note: '',
}

export class TasksState {
  _tasks = []
  _totalCount = null
  _totalPages = null
  _pageIndex = 1
  _pageSize = 10
  _hasNextPage = false
  _filter = filterState
  _submit = false

  constructor() {
    makeAutoObservable(this)
  }

  setTasks = (tasks) => {
    this._tasks = tasks
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

  changeFilter = (filter) => {
    this._filter = filter
  }

  clearFilter = () => {
    this._filter = filterState
  }

  setSubmit = (value) => {
    this._submit = value
  }

  get tasks() {
    return this._tasks
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

  get filter() {
    return this._filter
  }

  get submit() {
    return this._submit
  }

}
