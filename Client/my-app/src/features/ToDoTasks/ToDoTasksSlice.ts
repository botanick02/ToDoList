import {createAction, createSlice, PayloadAction} from '@reduxjs/toolkit'
import DateTimeToString from "../../Services/Convertors/DateTimeToString";
import {ToDoTask} from "./types/ToDoTask";
import {FetchToDoTasksInputType} from "../../GraphQl/ToDoTasks/queries";
import {
    ToDoTaskCreateInputType,
    ToDoTaskDeleteInputType, ToDoTaskUpdateInputType,
    ToggleIsDoneInputType
} from "../../GraphQl/ToDoTasks/mutations";

type toDoTasksState = {
    currenTasks: Array<ToDoTask>,
    completedTasks: Array<ToDoTask>
}


const initialState: toDoTasksState = {
    currenTasks: [],
    completedTasks: []
}


const toDoTasksSlice = createSlice({
    name: 'toDoTasks',
    initialState,
    reducers: {
        currentTasksAdded(state, action: PayloadAction<ToDoTask[]>) {
            state.currenTasks = action.payload
        },
        completedTasksAdded(state, action: PayloadAction<ToDoTask[]>) {
            state.completedTasks = action.payload
        },
        taskAdded(state, action: PayloadAction<ToDoTask>) {
            state.currenTasks.push(action.payload);
        },
        taskUpdated(state, action: PayloadAction<ToDoTask>) {
            const {id, title, categoryId, deadlineDate, isDone} = action.payload;
            let existingTask;
            if(isDone){
                existingTask = state.completedTasks.find(task => task.id === id);
            }else{
                existingTask = state.currenTasks.find(task => task.id === id);
            }
            if (existingTask) {
                existingTask.title = title;
                existingTask.categoryId = categoryId;
                existingTask.deadlineDate = deadlineDate;
            }
        },
        isDoneToggled(state, action: PayloadAction<{ id: number, isDone: boolean, doneDate: string }>) {
            const {id, isDone, doneDate} = action.payload;
            if (!isDone) {
                let existingTask = state.completedTasks.find(task => task.id === id);
                if (existingTask) {
                    existingTask.isDone = !existingTask.isDone;
                    existingTask.doneDate = "";
                    state.currenTasks.push(existingTask);
                    state.completedTasks = state.completedTasks.filter(task => task.id != id);
                }
            } else {
                let existingTask = state.currenTasks.find(task => task.id === id);
                if (existingTask) {
                    existingTask.isDone = !existingTask.isDone;
                    existingTask.doneDate = doneDate;
                    state.completedTasks.push(existingTask);
                    state.currenTasks = state.currenTasks.filter(task => task.id != id);
                }
            }
        },
        taskDeleted(state, action: PayloadAction<ToDoTask>) {
            const {id} = action.payload;
            state.completedTasks = state.completedTasks.filter(task => task.id !== id);
            state.currenTasks = state.currenTasks.filter(task => task.id !== id);
        }
    }
})
export const {
    currentTasksAdded,
    completedTasksAdded,
    taskDeleted,
    taskAdded,
    taskUpdated,
    isDoneToggled
} = toDoTasksSlice.actions;

export const fetchToDoTasks = createAction<FetchToDoTasksInputType>("toDoTasks/fetchToDoTasks");
export const addToDoTask = createAction<ToDoTaskCreateInputType>("toDoTasks/addToDoTask");
export const updateToDoTask = createAction<ToDoTaskUpdateInputType>("toDoTasks/updateToDoTask");
export const toggleIsDone = createAction<ToggleIsDoneInputType>("toDoTasks/toggleIsDone");
export const deleteToDoTask = createAction<ToDoTaskDeleteInputType>("toDoTasks/deleteToDoTask");


export default toDoTasksSlice.reducer

