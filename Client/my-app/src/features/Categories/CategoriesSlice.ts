import {createAction, createSlice, PayloadAction} from '@reduxjs/toolkit'
import {Category} from "./types/Category";
import {FetchToDoTasksInputType} from "../../GraphQl/ToDoTasks/queries";
import {
    CategoryCreateInputType,
    CategoryDeleteInputType,
    CategoryUpdateInputType
} from "../../GraphQl/Categories/mutations";


type categoriesState = {
    categoriesList: Category[]
}

const initialState: categoriesState = {
    categoriesList: []
}

const categoriesSlice = createSlice({
    name: 'categories',
    initialState,
    reducers: {
        categoriesAdded(state, action: PayloadAction<Category[]>){
            state.categoriesList = action.payload;
        },
        categoryCreated(state, action : PayloadAction<Category>){
            state.categoriesList.push(action.payload);
        },
        categoryUpdated(state, action: PayloadAction<Category>){
            const {id, name} = action.payload;
            let existingCategory = state.categoriesList.find(category => category.id === +id);
            if(existingCategory){
                existingCategory.name = name;
            }
        },
        categoryDeleted(state, action: PayloadAction<Category>){
            const {id} = action.payload;
            state.categoriesList = state.categoriesList.filter(category => category.id !== +id);
        }
    }
})
export const {categoriesAdded,categoryCreated, categoryDeleted, categoryUpdated} = categoriesSlice.actions;

export const fetchCategories = createAction("categories/fetchCategories");
export const createCategory = createAction<CategoryCreateInputType>("categories/createCategory");
export const deleteCategory = createAction<CategoryDeleteInputType>("categories/deleteCategory");
export const updateCategory = createAction<CategoryUpdateInputType>("categories/updateCategory");

export default categoriesSlice.reducer;
