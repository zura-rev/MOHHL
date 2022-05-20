import React from 'react'

export const FilterDropdown = ({ name, onChange, value, data, placeholder }) => {

  // const [data, setData] = useState()

  const handleChange = (event) => {
    onChange(event)
  }

  return (
    <select onChange={handleChange} id={name} name={name} value={value} className='form-select'>
      <option value=''>{placeholder}</option>
      {
        data.map(item => <option key={item.id} value={item.id}>{item.name}</option>)
      }
    </select>
  )
}


