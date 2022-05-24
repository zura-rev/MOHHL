import { makeAutoObservable} from 'mobx'
import apiClient from '../apiClient'

const filterState = {
  fromDate: '',
  toDate: '',
  privateNumber: '',
  callAuthor: '',
  callNumber: '',
  phone: '',
  categories: '',
  users: '',
  note: ''
}

const filterControls = [
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
    placeholder: 'კატეგორია',
    url: '/api/categories',
  },
  {
    field: 'user',
    type: 'SELECT',
    placeholder: 'ოპერატორი',
    url: '/api/users/operators',
  },
  {
    field: 'note',
    type: 'TEXT',
    placeholder: 'ზარის შინაარსი'
  },
]

export class CallsState {

  _filter = filterState
  _filterControls = filterControls
  _calls = []
  _totalCount = null
  _totalPages = null
  _pageIndex = 1
  _pageSize = 10
  _hasNextPage = false
  _matchCalls = []
  _submit = true
  _loading = false
  _error = null

  constructor() {
    makeAutoObservable(this)
  }

  getCalls = async (token) => {
    try {
      this._loading = true
      const calls = await apiClient.calls.get(token, this._filter, this._pageIndex, this._pageSize)
      this._calls = calls.data
      this._totalCount = Number(calls.headers.totalcount)
      this._totalPages = Number(calls.headers.totalpages)
      this._pageIndex = Number(calls.headers.pageindex)
      this._pageSize = Number(calls.headers.pagesize)
      this._hasNextPage = calls.headers.hasnextpage === 'True'
    } catch (error) {
      this._error = error
    } finally {
      this._loading = false
      this._submit = false
    }
  }

  //getByPhoneOrPN = async (token, key, phone, privateNumber, top)
  getMatchCalls = async (token, key, value) => {
    try {
      const data = await apiClient.calls.getByPhoneOrPN(token, key, value)
      this._matchCalls = data
    } catch (error) {
      this._error = error
    }
  }

  setMatchCalls = (calls) => {
    this._matchCalls = calls
  }

  setPageIndex = (pageIndex) => {
    this._pageIndex = pageIndex
    this._submit = true
  }

  setPageSize = (pageSize) => {
    this._pageSize = pageSize
    this._submit = true
    this._pageIndex = 1
  }

  // setHasNextPage = (hasNextPage) => {
  //   this._hasNextPage = (hasNextPage === 'True')
  // }

  setSubmit = (value) => {
    this._submit = value
  }

  changeFilter = (filter) => {
    this._filter = filter
  }

  clearFilter = () => {
    this._filter = filterState
  }

  get calls() {
    return this._calls
  }

  get filter() {
    return this._filter
  }

  get filterControls() {
    return this._filterControls
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

  get submit() {
    return this._submit
  }

  get loading() {
    return this._loading
  }

}

