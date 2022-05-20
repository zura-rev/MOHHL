import React, { createContext } from 'react'
import { CallsState } from '../store/calls'
import { CallState } from '../store/call'
import { CategoriesState } from '../store/categories'
import { CardsState } from '../store/cards'
import { UsersState } from '../store/users'

export const StoreContext = createContext(null)

export const StoreProvider = ({ children }) => {

  return (
    <StoreContext.Provider
      value={{
        callsState: new CallsState(),
        callState: new CallState(),
        categoriesState: new CategoriesState(),
        cardsState: new CardsState(),
        usersState: new UsersState()
      }}
    >
      {children}
    </StoreContext.Provider>
  )
}
