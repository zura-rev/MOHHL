import React, { createContext } from 'react'
import { CallsState } from '../store/calls'
import { CategoriesState } from '../store/categories'
import { TasksState } from '../store/tasks'
import { UsersState } from '../store/users'

export const StoreContext = createContext(null)

export const StoreProvider = ({ children }) => {

  return (
    <StoreContext.Provider
      value={{
        callsState: new CallsState(),
        categoriesState: new CategoriesState(),
        tasksState: new TasksState(),
        usersState: new UsersState()
      }}
    >
      {children}
    </StoreContext.Provider>
  )
}
