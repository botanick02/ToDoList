import {createSlice} from '@reduxjs/toolkit'


const initialState = [
    {
        id: '1',
        name: 'Uncategorized'
    },
    {
        id: '2',
        name: 'Work'
    },
    {
        id: '3',
        name: 'School'
    }
]

const categoriesSlice = createSlice({
    name: 'categories',
    initialState,
    reducers: {}
})

export default categoriesSlice.reducer