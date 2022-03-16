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

// const labelKa = {
//   fromDate: 'თარიღიდან',
//   toDate: 'თარიღამდე',
//   privateNumber: 'პირადი N',
//   callAuthor: 'ზარის ავტორი',
//   callNumber: 'ზარის ნომერი',
//   phone: 'ტელეფონი',
//   category: 'კატეგორია',
//   note: 'ზარის შინაარსი',
// }

export const filterControls = [
  {
    field: 'fromDate',
    type: 'CALENDAR',
    placeholder: 'თარიღიდან'
  },
  {
    field: 'toDate',
    type: 'CALENDAR',
    placeholder: 'თარიღამდე'
  },
  {
    field: 'privateNumber',
    type: 'TEXT',
    placeholder: 'პირადი N'
  },
  {
    field: 'callAuthor',
    type: 'TEXT',
    placeholder: 'ზარის ავტორი'
  },
  {
    field: 'callNumber',
    type: 'TEXT',
    placeholder: 'ზარის ნომერი'
  },
  {
    field: 'phone',
    type: 'TEXT',
    placeholder: 'ტელეფონი'
  },
  {
    field: 'category',
    type: 'SELECT',
    placeholder: 'კატეგორია'
  },
  {
    field: 'note',
    type: 'TEXT',
    placeholder: 'ზარის შინაარსი'
  }
]

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
    this._hasNextPage = (hasNextPage === 'True')
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

  get filter() {
    return this._filter
  }

  get submit() {
    return this._submit
  }

}

