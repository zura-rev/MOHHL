import axios from 'axios'
import moment from 'moment'
import { apiurl } from '../constants'

export const get = async (token, filter, pageIndex, pageSize) => {
    const url = `api/calls?pageIndex=${pageIndex}&pageSize=${pageSize}
    ${filter.callNumber ? `&id=${filter.callNumber}` : ''}
    ${filter.phone ? `&phone=${filter.phone}` : ''}
    ${filter.privateNumber ? `&privateNumber=${filter.privateNumber}` : ''}
    ${filter.callAuthor ? `&callAuthor=${filter.callAuthor}` : ''}
    ${filter.category ? `&categoryId=${filter.category.id}` : ''}
    ${filter.user ? `&userId=${filter.user.id}` : ''}
    ${filter.note ? `&note=${filter.note}` : ''}
    ${filter.fromDate ? `&fromDate=${moment(filter.fromDate).format('YYYY-MM-DD')}` : ''}
    ${filter.toDate ? `&toDate=${moment(filter.toDate).format('YYYY-MM-DD')}` : ''}`
    try {
        const response = await axios.get(`${apiurl}/${url}`, { headers: { Authorization: `Bearer ${token}` } })
        return response
    } catch (error) {
        throw error
    }
}

export const getByPhoneOrPN = async (token, key, value) => {
    const url = `api/calls/matchcalls?${(key === 'PHONE') ? `phone=${value}` : (key === 'PN') ? `privateNumber=${value}` : null}&topValue=${10}`
    try {
        const response = await axios.get(`${apiurl}/${url}`, { headers: { Authorization: `Bearer ${token}` } })
        console.log('___', response.data)
        return response.data
    } catch (error) {
        throw error
    }
}

