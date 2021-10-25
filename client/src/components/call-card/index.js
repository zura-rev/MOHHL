import React from 'react'
import moment from 'moment'
import 'moment/locale/ka'

export const CallCard = ({ call }) => {
  //console.log('call', call)
  return (
    <div className='row'>
      <div className='col-6'>
        <div className='card'>
          <div className='card-body'>
            <h5>
              {`N ${call.id}`}
            </h5>
            <hr />
            <table className='table'>
              <tbody>
                <tr>
                  <th>პირადი ნომერი  </th>
                  <td>{call.privateNumber}</td>
                </tr>
                <tr>
                  <th>სახელი გვარი  </th>
                  <td>{call.callAuthor}</td>
                </tr>
                <tr>
                  <th>თარიღი  </th>
                  <td>{moment(call.createDate).format('LLLL')}</td>
                </tr>
                <tr>
                  <th>ტელეფონი  </th>
                  <td>{call.phone}</td>
                </tr>
                <tr>
                  <th>კატეგორია  </th>
                  <td>{call.category.categoryName}</td>
                </tr>
                <tr>
                  <th>ზარის ტიპი</th>
                  <td>{call.callType === 1 ? 'კონსულტაცია' : 'ბარათი'}</td>
                </tr>
                <tr>
                  <th>აღწერა </th>
                  <td>{call.note}</td>
                </tr>
                <tr>
                  <th>ოპერატორი </th>
                  <td>
                    {call.user.firstName} {call.user.lastName}
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
      {
        call.card ?
          <div className='col-6'>
            <div className='card'>
              <div className='card-body'>
                <h5>სუპერვაიზერი</h5>
                <hr />
                <table className='table'>
                  <tbody>
                    <tr>
                      <th>სახელი</th>
                      <td>{call.card.user.firstName}</td>
                    </tr>
                    <tr>
                      <th>გვარი</th>
                      <td>{call.card.user.lastName}</td>
                    </tr>
                    <tr>
                      <th>სტატუსი</th>
                      <td><h6 style={{ marginBottom: 0 }}>{call.card.status === 1 ? <span className='badge rounded-pill bg-success'>დასრულებული</span> : <span className='badge rounded-pill bg-danger'>დამუშავების პროცესში</span>}</h6></td>
                    </tr>
                    <tr>
                      <th>შესრულების თარიღი</th>
                      <td>{call.card.performDate ? moment(call.card.performDate).format('LLLL') : '-'}</td>
                    </tr>
                    <tr>
                      <th>აღწერა</th>
                      <td>{call.card.note ?? '-'}</td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div> : null
      }
    </div>)
}
