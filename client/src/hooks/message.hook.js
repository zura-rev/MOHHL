import { useCallback } from 'react'
import Swal from 'sweetalert2'

export const useMessage = () => {
  const message = useCallback((text) => {
    if (text) {
      Swal.fire({
        icon: 'error',
        text,
        confirmButtonText: 'დახურვა',
      })
    }
  }, [])
  return message
}



