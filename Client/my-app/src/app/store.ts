import {configureStore} from '@reduxjs/toolkit';
import ToDoTasksReducer from '../features/ToDoTasks/ToDoTasksSlice';
import CategoriesReducer from '../features/Categories/CategoriesSlice';
import StorageSourcesReducer from '../features/StorageSources/StorageSourcesSlice';
import {combineEpics, createEpicMiddleware} from "redux-observable";
import {ToDoTasksEpics} from "./epics/ToDoTasksEpics"
import {CategoriesEpics} from "./epics/CategoriesEpics";
import {StorageSourceEpics} from "./epics/StorageSourcesEpics";

const epicMiddleware = createEpicMiddleware();

export const store = configureStore({
    reducer: {
        toDoTasks: ToDoTasksReducer,
        categories: CategoriesReducer,
        storageSources: StorageSourcesReducer
    }, middleware: [epicMiddleware]
})

// @ts-ignore
export const RootEpic = combineEpics(ToDoTasksEpics, CategoriesEpics, StorageSourceEpics);
// @ts-ignore
epicMiddleware.run(RootEpic);

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch