import { makeAutoObservable } from 'mobx'

export class CategoriesState {
  _categories = []
  _groupedCategories = []
  _parentCategories = []
  
  constructor() {
    makeAutoObservable(this)
  }

  _groupBy = (items, key) =>
    items
      .filter((x) => x.parentId !== 0)
      .reduce((result, item) => {
        let k = this._parentCategories.find(
          (x) => x.id === item[key]
        ).categoryName
        return {
          ...result,
          [k]: [...(result[k] || []), item],
        }
      }, {})

  setCategories = (categories) => {
    this._parentCategories = categories.filter(
      (category) => category.parentId === 0
    )

    this._categories = categories

    const obj = this._groupBy(categories, 'parentId')
    const keys = Object.keys(obj)
    this._groupedCategories = keys.map((item) => ({
      label: item,
      options: obj[item].map((item) => ({
        label: item.categoryName,
        option: item.parentId,
        value: item.id,
      })),
    }))
  }

  get categories() {
    return this._categories
  }

  get groupedCategories() {
    return this._groupedCategories
  }

  get parentCategories() {
    return this._parentCategories
  }
}
