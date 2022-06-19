import {createSlice} from '@reduxjs/toolkit'


const initialState = [
    {
        id: 1,
        name: 'Uncategorized'
    },
    {
        id: 2,
        name: 'Work'
    },
    {
        id: 3,
        name: 'School'
    }
]

const categoriesSlice = createSlice({
    name: 'categories',
    initialState,
    reducers: {
        categoryAdded(state, action){
            state.push(action.payload);
        },
        categoryUpdated(state, action){
            const {id, name} = action.payload;
            let existingCategory = state.find(category => category.id === +id);
            if(existingCategory){
                existingCategory.name = name;
            }
        },
        categoryDeleted(state, action){
            const {id} = action.payload;
            return state.filter(category => category.id !== +id);
        }
    }
})
export const {categoryAdded, categoryDeleted, categoryUpdated} = categoriesSlice.actions;

export default categoriesSlice.reducer;

export type Category ={
    id: number,
    name: string
}