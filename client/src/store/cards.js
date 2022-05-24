import { makeAutoObservable } from 'mobx'
import apiClient from '../apiClient'

const filterState = {
  id: '',
  callId: '',
  userId: '',
  status: '',
  category: '',
  note: '',
}

const filterControls = [
  {
    field: 'id',
    type: 'TEXT',
    placeholder: 'ბარათის ნომერი'
  },
  {
    field: 'callId',
    type: 'TEXT',
    placeholder: 'ზარის ნომერი'
  },

  {
    field: 'category',
    type: 'SELECT',
    placeholder: 'კატეგორია',
    url: '/api/categories',
  },
  {
    field: 'status',
    type: 'DROPDOWN',
    placeholder: 'სტატუსი',
    data: [{ id: -1, name: 'დამუშავების პროცესში' }, { id: 1, name: 'შესრულებული' }],
  },
  {
    field: 'note',
    type: 'TEXT',
    placeholder: 'შინაარსი'
  }
]

export class CardsState {
  _filter = filterState
  _filterControls = filterControls
  _cards = []
  _totalCount = null
  _totalPages = null
  _pageIndex = 1
  _pageSize = 5
  _hasNextPage = false
  _submit = true
  _loading = false

  constructor() {
    makeAutoObservable(this)
  }

  getCards = async (token) => {
    try {
      this._loading = true
      const cards = await apiClient.cards.get(token, this._filter, this._pageIndex, this._pageSize)
      console.log('cards', cards)
      this._cards = cards.data
      this._totalCount = Number(cards.headers.totalcount)
      this._totalPages = Number(cards.headers.totalpages)
      this._pageIndex = Number(cards.headers.pageindex)
      this._pageSize = Number(cards.headers.pagesize)
      this._hasNextPage = cards.headers.hasnextpage === 'True'
    } catch (error) {
      this._error = error
    } finally {
      this._loading = false
      this._submit = false
    }
  }

  setCards = (cards) => {
    this._cards = cards
  }

  // setTotalCount = (totalCount) => {
  //   this._totalCount = totalCount
  // }

  // setTotalPages = (totalPages) => {
  //   this._totalPages = totalPages
  // }

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

  changeFilter = (filter) => {
    this._filter = filter
  }

  clearFilter = () => {
    this._filter = filterState
  }

  setSubmit = (value) => {
    this._submit = value
  }

  get cards() {
    return this._cards
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

  get filterControls() {
    return this._filterControls
  }

  get submit() {
    return this._submit
  }

  get loading() {
    return this._loading
  }

}


