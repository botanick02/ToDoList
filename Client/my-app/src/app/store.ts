import {configureStore} from '@reduxjs/toolkit';
import ToDoTasksReducer from '../features/ToDoTasks/ToDoTasksSlice';
import CategoriesReducer from '../features/Categories/CategoriesSlice';
import StorageSourcesReducer from '../features/StorageSources/StorageSourcesSlice';

export const store = configureStore({
    reducer: {
        toDoTasks: ToDoTasksReducer,
        categories: CategoriesReducer,
        storageSources: StorageSourcesReducer
    }
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch