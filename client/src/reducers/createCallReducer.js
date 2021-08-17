import { useReducer } from 'react'

export function useCreateCallReducer(initialState) {

    function createCallReducer(state, action) {
        const { type, payload } = action
        switch (type) {
            case 'CHANGE':
                return { ...state, ...payload }
        }
    }

    const [state, dispatch] = useReducer(createCallReducer, initialState)

    return [state, dispatch]

}

