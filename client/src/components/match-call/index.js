import React, { useContext, useEffect } from 'react'
import { observer } from 'mobx-react-lite'
import moment from 'moment'
import 'moment/locale/ka'
import { StoreContext } from '../../context/StoreProvider'

export const MatchCall = observer(() => {
  const { callsState: { matchCalls, setMatchCalls } } = useContext(StoreContext)

  useEffect(() => {
    setMatchCalls([])
  }, [])

  const renderMatchItems = () => {

    if (matchCalls.length > 0) {
      return <div className='card'>
        <div className='card-body'>
          <h5 className='mb-3'>სხვა ზარები</h5>
          <hr />
          <table className='table'>
            <thead>
              <tr>
                <th>მოქალაქე</th>
                <th>თარიღი</th>
                <th>კატეგორია</th>
                <th>სტატუსი</th>
                <th>ოპერატორი</th>
              </tr>
            </thead>
            <tbody>
              {matchCalls.map((call) => <tr>
                <td>{call.callAuthor}</td>
                <td>{moment(call.createDate).format('LLLL')}</td>
                <td>{call.category.categoryName}</td>
                <td>{call.callStatus}</td>
                <td>{call.user.firstName + '  ' + call.user.lastName}</td>
              </tr>)}
            </tbody>
          </table>
        </div>
      </div >
    }
  }

  return <div className='col-md-6'>{renderMatchItems()}</div>
})

