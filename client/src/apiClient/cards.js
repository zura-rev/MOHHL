import axios from 'axios'
import { apiurl } from '../constants'

export const get = async (token, filter, pageIndex, pageSize) => {
    const url = `api/card?pageIndex=${pageIndex}&pageSize=${pageSize}
                ${filter.id ? `&id=${filter.id}` : ''}
                ${filter.callId ? `&callId=${filter.callId}` : ''}
                ${filter.userId ? `&userId=${filter.userId}` : ''}
                ${filter.status ? `&status=${filter.status}` : ''}
                ${filter.note ? `&note=${filter.note}` : ''}
                ${filter.category ? `&categoryId=${filter.category.id}` : ''}`
    try {
        const response = await await axios.get(`${apiurl}/${url}`, { headers: { Authorization: `Bearer ${token}` } })
        return response
    } catch (error) { throw error }
}
