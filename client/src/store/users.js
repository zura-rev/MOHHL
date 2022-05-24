import { makeAutoObservable } from 'mobx'

const filterState = {
  privateNumber: '',
  firstName: '',
  lastName: '',
}

const filterControls = [
  {
    field: 'privateNumber',
    type: 'TEXT',
    placeholder: 'პირადი N'
  },
  {
    field: 'userName',
    type: 'TEXT',
    placeholder: 'მომხმარებლის სახელი'
  },
  {
    field: 'firstName',
    type: 'TEXT',
    placeholder: 'სახელი'
  },
  {
    field: 'lastName',
    type: 'TEXT',
    placeholder: 'გვარი'
  }
]

export class UsersState {
  _users = []
  _totalCount = null
  _totalPages = null
  _pageIndex = 1
  _pageSize = 10
  _hasNextPage = false
  _filter = filterState
  _filterControls = filterControls
  _submit = false

  constructor() {
    makeAutoObservable(this)
  }

  // setUser = (user) => {
  //   this._users.map(_user => _user.id === user.id ? user : _user)
  // }

  // upsert = (user) => {
  //   const index = this._users.findIndex(_element => _element.id === user.id)
  //   if (index > -1) this._users[index] = user
  //   else this._users.push(user)
  // }

  setUsers = (users) => {
    this._users = users
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

  get users() {
    return this._users
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

}


