import React from 'react'
import moment from 'moment'
import 'moment/locale/ka'


export function MatchCallItem({ call }) {
    return <div className='card mb-2'>
        <div className='match-call '>
            <h6>{call.id}</h6>
            <div>
                <div className='d-flex justify-content-between'>
                    <div><span className='fw-bold pe-2'>მოქალაქე</span>{call.callAuthor}</div>
                    <div> {moment(call.createDate).format('LLLL')}</div>
                </div>
                <div><span className='fw-bold pe-2'>კატეგორია</span>{call.category.categoryName}</div>
                <div className='d-flex justify-content-between'>
                    <div><span className='fw-bold pe-2'>ოპერატორი</span>{call.user.firstName + '  ' + call.user.lastName}</div>
                    <div><span className='badge rounded-pill bg-primary fw-lighter'>{call.callType === 1 ? 'კონსულტაცია' : 'ბარათი'}</span></div>
                </div>
                <div className='call-note-operator'>{call.note}</div>
                {call.card ? <div className='d-flex justify-content-between'>
                    <div><span className='fw-bold pe-2'>სუპერვაიზერი</span>{call.card?.user.firstName + '  ' + call.card?.user.lastName}</div>
                    <div>{call.card.status === 1 ? <span className='badge rounded-pill bg-success fw-lighter'>დასრულებული</span> : <span className='badge rounded-pill bg-danger fw-light'>ქმედების მოლოდინში</span>}</div>
                </div> : null}
                {call.callType === 2 && call.card.status === 1 ? <div className='call-note-supervaiser'>{call.card.note}</div> : null}
            </div>
        </div>
    </div >
}