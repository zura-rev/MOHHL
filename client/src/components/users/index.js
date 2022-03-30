import React, { useState, useContext, useReducer } from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { observer } from 'mobx-react-lite'
import { Button } from 'react-bootstrap'
import { faPlus } from '@fortawesome/fontawesome-free-solid'
import { AuthContext } from '../../context/AuthProvider'
import { userReducer, userState } from './reducer'
import { UserModal } from './modal'
import { UserTable } from './table'



export const Users = () => {
    const { user: { token } } = useContext(AuthContext)
    const [modalShow, setModalShow] = useState(false)
    const [state, dispatch] = useReducer(userReducer, userState)
    
    return <div style={{ marginTop: 110 }}>
        <Button size='sm' variant='outline-primary' onClick={() => setModalShow(true)}>
            <FontAwesomeIcon icon={faPlus} />
        </Button>
        <hr />
        <UserTable setModalShow={setModalShow} dispatch={dispatch} token={token} />
        <UserModal modalShow={modalShow} setModalShow={setModalShow} state={state} dispatch={dispatch} token={token} />
    </div>
}