export const userState = {
    id: 0,
    privateNumber: '',
    userName: '',
    firstName: '',
    lastName: '',
    password: '',
    resources: [],
    edit: false
}

export function userReducer(state = userState, action) {
    const { type, payload } = action
    switch (type) {
        case 'CHANGE':
            return { ...state, ...payload }
        case 'EDIT':
            return { ...state, ...payload }
        case 'CLEAR':
            return userState
        default:
            break
    }
}