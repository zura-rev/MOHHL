import { makeAutoObservable } from 'mobx'


const initialState = {
  id: 0,
  categoryName: '',
  parentId: 0,
  status: 0,
  note: '',
}

export class CategoriesState {
  _categories = []
  _groupedCategories = []
  _parentCategories = []
  _selectedCategory = null
  _categoryModalShow = false
  _category = initialState
  _isCategoryEdit = false

  constructor() {
    makeAutoObservable(this)
  }

  _groupBy = (items, key) => {
    return items.filter((x) => x.parentId !== 0)
      .reduce((result, item) => {

        let _key = this._parentCategories.find(x => x.id === item[key])?.categoryName

        return {
          ...result,
          [_key]: [...(result[_key] || []), item],
        }

      }, {})
  }

  setCategories = (categories) => {
    this._parentCategories = categories.filter(category => category.parentId === 0)
    this._categories = categories
    const obj = this._groupBy(categories, 'parentId')
    const keys = Object.keys(obj)
    this._groupedCategories = keys.map((item) => ({
      label: item,
      options: obj[item].map((item) => ({
        label: item.categoryName,
        option: item.parentId,
        value: item.id,
        checked: item.status === 1
      })),
    }))
  }

  setCategoryModalShow = (show) => {
    this._categoryModalShow = show
  }

  setCategory = (category) => {
    this._category = category
  }

  setCategoryEdit = (isCategoryEdit) => {
    this._isCategoryEdit = isCategoryEdit
  }

  clearCategory = () => {
    this._category = initialState
  }

  get categories() {
    return this._categories
  }

  get category() {
    return this._category
  }

  get categoryModalShow() {
    return this._categoryModalShow
  }

  get groupedCategories() {
    return this._groupedCategories
  }

  get parentCategories() {
    return this._parentCategories
  }

  get isCategoryEdit() {
    return this._isCategoryEdit
  }

}


