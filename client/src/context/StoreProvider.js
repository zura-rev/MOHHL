import React, { createContext } from 'react'
import { FilterState } from '../store/filter'
import { CallsState } from '../store/calls'
import { CategoriesState } from '../store/categories'
import { TasksState } from '../store/tasks'

export const StoreContext = createContext(null)

export const StoreProvider = ({ children }) => {

  return (
    <StoreContext.Provider
      value={{
        callsState: new CallsState(),
        filterState: new FilterState(),
        categoriesState: new CategoriesState(),
        tasksState: new TasksState(),
      }}
    >
      {children}
    </StoreContext.Provider>
  )
}
