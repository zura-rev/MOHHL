import React, { useState, useEffect, useRef } from 'react'

const FileUploader = ({ getFiles }) => {
  const [files, setFiles] = useState([])
  const fileUploadRef = useRef(null)

  const selectFile = (event) => {
    setFiles([...files, { file: event.target.files[0], title: '' }])
    fileUploadRef.current.value = ''
  }

  useEffect(() => {
    getFiles(files)
  }, [files])

  const removeFile = (name) => {
    const _files = files.filter((item) => item.file.name != name)
    setFiles(_files)
  }

  const addFileName = (event) => {
    const _files = files.map((item) => {
      if (item.file.name === event.target.name) {
        item.title = event.target.value
      }
      return item
    })
    setFiles(_files)
  }

  return (
    <div>
      <label htmlFor='note'>ფაილების ატვირთვა</label>
      <div className='card'>
        <div>
          <label className='btn btn-default'>
            <input type='file' ref={fileUploadRef} onChange={selectFile} />
          </label>

          <ul
            className='list-group list-group-flush'
            style={{ borderTop: '1px solid #ddd' }}
          >
            {files.length > 0 ? (
              files.map((item, index) => (
                <li className='list-group-item' key={index}>
                  <div>
                    <span> {` ${item.file.name} - ${item.file.size} `}</span>
                    <span
                      className='float-right remove-icon'
                      onClick={() => removeFile(item.file.name)}
                    >
                      x
                    </span>
                  </div>
                  <div>
                    <input
                      type='text'
                      name={item.file.name}
                      placeholder='ფაილის აღწერა'
                      style={{ width: '100%' }}
                      value={item.title || ''}
                      onChange={addFileName}
                    />
                  </div>
                </li>
              ))
            ) : (
              <li className='list-group-item text-center'>
                ფაილები ატვირთული არ არის.
              </li>
            )}
          </ul>
        </div>
      </div>
    </div>
  )
}

export default FileUploader
