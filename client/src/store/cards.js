import { makeAutoObservable } from 'mobx'

const filterState = {
  id: '',
  callId: '',
  userId: '',
  status: '',
  category: '',
  note: '',
}

export const filterControls = [
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
    field: 'userId',
    type: 'TEXT',
    placeholder: 'მომხმარებელი'
  },
  {
    field: 'status',
    type: 'TEXT',
    placeholder: 'სტატუსი'
  },
  {
    field: 'category',
    type: 'SELECT',
    placeholder: 'კატეგორია'
  },
  {
    field: 'note',
    type: 'TEXT',
    placeholder: 'შინაარსი'
  }
]

export class CardsState {
  _cards = []
  _totalCount = null
  _totalPages = null
  _pageIndex = 1
  _pageSize = 5
  _hasNextPage = false
  _filter = filterState
  _submit = false

  constructor() {
    makeAutoObservable(this)
  }

  setCards = (cards) => {
    this._cards = cards
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

  get submit() {
    return this._submit
  }

}


