import {configureStore} from '@reduxjs/toolkit';
import ToDoTasksReducer from '../features/ToDoTasks/ToDoTasksSlice';
import CategoriesReducer from '../features/Categories/CategoriesSlice';
import StorageSourcesReducer from '../features/StorageSource/StorageSourcesSlice';

export default configureStore({
    reducer: {
        toDoTasks: ToDoTasksReducer,
        categories: CategoriesReducer,
        storageSources: StorageSourcesReducer
    }
})

