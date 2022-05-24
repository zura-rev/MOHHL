import axios from 'axios'
import { apiurl } from '../constants'

export const upsert = async (token, call) => {
    const url = `api/calls`

    try {
        const response = await axios({
            method: 'PUT',
            url: `${apiurl}/${url}`,
            data: { ...call },
            headers: { Authorization: `Bearer ${token}` }
        })
        return response.data
    } catch (error) {
        throw error
    }

}

