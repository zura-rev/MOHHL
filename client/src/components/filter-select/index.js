import React, { useState, useEffect, useContext, useCallback } from 'react'
import Select from 'react-select'
import { useHttp } from '../../hooks/http.hook'
import { AuthContext } from '../../context/AuthProvider'
//import { StoreContext } from '../../context/StoreProvider'
//import { observer } from 'mobx-react-lite'



export const FilterSelect = ({ name, onChange, value, url, placeholder }) => {

  const { loading, request } = useHttp()
  const { user } = useContext(AuthContext)
  const [data, setData] = useState()

  const fetchData = useCallback(async () => {
    try {
      const response = await request(url, 'GET', null, {
        Authorization: `Bearer ${user.token}`,
      })
      setData(response?.data?.map(item => ({
        value: Object.values(item)[0],
        label: Object.values(item)[1]
      })))
    } catch (error) {
      throw error
    }
  }, [])

  useEffect(() => {
    fetchData()
  }, [])

  // const handleChange = (event) => {
  //   onChange(event)
  // }

  const handleChange = (selected, nameOfComponent) => {
    onChange(selected, nameOfComponent)
  }

  return (

    <Select
      id={name}
      options={data}
      placeholder={placeholder}
      name={name}
      onChange={handleChange}
      value={value}
      isClearable={false}
      isDisabled={false}
      isLoading={false}
      isRtl={false}
      isSearchable={true}
    />


  )
}

//)
