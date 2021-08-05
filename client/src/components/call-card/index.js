import React from 'react'
import moment from 'moment'
import 'moment/locale/ka'

export const CallCard = ({ call }) => {
  return (
    <div className='row'>
      <div className='col'>
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
                  <th>სტატუსი  </th>
                  <td>{call.callStatus}</td>
                </tr>
                <tr>
                  <th>აღწერა: </th>
                  <td>{call.note}</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
      <div className='col'>
        <div className='card'>
          <div className='card-body'>
            <h5>შემსრულებლები</h5>
            <hr />
            <table className='table'>
              <thead>
                <tr>
                  <th>სახელი</th>
                  <th>გვარი</th>
                  <th>პოზიცია</th>
                </tr>
              </thead>
              <tbody>
                {call.performers.map((performer) => (
                  <tr key={performer.id}>
                    <td>{performer.user.firstName}</td>
                    <td>{performer.user.lastName}</td>
                    <td>
                      {performer.userType === 1 ? 'ოპერატორი' : 'სუპერვაიზერი'}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  )
}
