import React, { useEffect, useContext, useCallback } from 'react'
import Select from 'react-select'
import { useHttp } from '../../hooks/http.hook'
import { AuthContext } from '../../context/AuthProvider'
import { StoreContext } from '../../context/StoreProvider'
import { observer } from 'mobx-react-lite'

const groupStyles = {
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'space-between',
}

const groupBadgeStyles = {
  backgroundColor: '#EBECF0',
  borderRadius: '2em',
  color: '#172B4D',
  display: 'inline-block',
  fontSize: 12,
  fontWeight: 'normal',
  lineHeight: '1',
  minWidth: 1,
  padding: '0.16666666666667em 0.5em',
  textAlign: 'center',
}

const formatGroupLabel = (data) => (
  <div style={groupStyles}>
    <span>{data.label}</span>
    <span style={groupBadgeStyles}>{data.options.length}</span>
  </div>
)

export const CategorySelect = observer(
  ({ name, onChange, required, value }) => {
    const { loading, request } = useHttp()
    const { user } = useContext(AuthContext)
    const { categoriesState: { groupedCategories, setCategories } } = useContext(StoreContext)

    // if (!value && required) {
    //   console.log('value', value)
    // }

    const handleChange = (selected, nameOfComponent) => {
      onChange(selected, nameOfComponent)
    }

    const fetchCategories = useCallback(async () => {
      try {
        const response = await request(`/api/Categories`, 'GET', null, {
          Authorization: `Bearer ${user.token}`,
        })
        setCategories(response && response.data)
      } catch (error) {
        throw error
      }
    }, [])

    useEffect(() => {
      fetchCategories()
    }, [])

    return (
      <>
        <Select
          formatGroupLabel={formatGroupLabel}
          options={groupedCategories}
          placeholder='აირჩიეთ კატეგორია ...'
          name={name}
          onChange={handleChange}
          isSearchable={true}
          value={value}
        />
      </>
    )
  }
)
