import { useState, useEffect } from "react"

export const useLocalStorage = (key) => {
    const storage = JSON.parse(localStorage.getItem(key))
    const [value, setValue] = useState(storage)

    useEffect(() => {
        if (value) {
            localStorage.setItem(key, JSON.stringify(value))
        }
    }, [value])

    return [value, setValue]
}
