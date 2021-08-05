import React, { useContext } from 'react'
import Select from 'react-select'
import { useHttp } from '../../hooks/http.hook'
import { AuthContext} from '../../context/AuthProvider'
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
  ({ required, onChange, name, value }) => {
    //console.log('value', value)
    const { loading, request } = useHttp()
    const { user } = useContext(AuthContext)
    const { categoriesState } = useContext(StoreContext)
    const { groupedCategories, setCategories } = categoriesState

    const fetchCategories = React.useCallback(async () => {
      try {
        const response = await request(`/api/Categories`, 'GET', null, {
          Authorization: `Bearer ${user.token}`,
        })
        setCategories(response && response.data)
      } catch (error) {
        throw error
      }
    }, [])

    React.useEffect(() => {
      fetchCategories()
    }, [])

    return (
      <Select
        options={groupedCategories}
        formatGroupLabel={formatGroupLabel}
        placeholder='აირჩიეთ კატეგორია ...'
        name={name}
        onChange={onChange}
        required={required}
        //isClearable={true}
        isSearchable={true}
        value={value}
      />
    )
  }
)
