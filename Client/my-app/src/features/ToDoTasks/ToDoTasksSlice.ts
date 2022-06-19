import {createSlice} from '@reduxjs/toolkit'
import DateTimeToString from "../../Services/Convertors/DateTimeToString";


const initialState = [
    {
        id: 1,
        title: "Do something",
        categoryId: 2,
        createdDate: "2022-06-01T13:53",
        deadlineDate: "2022-06-23T13:53",
        isDone: false,
        doneDate: ""
    },
    {
        id: 2,
        title: "Do something",
        categoryId: 2,
        createdDate: "2022-06-01T13:53",
        deadlineDate: "2022-06-21T13:53",
        isDone: false,
        doneDate: ""
    },
    {
        id: 3,
        title: "Do something",
        categoryId: 2,
        createdDate: "2022-06-01T13:53",
        deadlineDate: "2022-06-27T13:53",
        isDone: false,
        doneDate: ""
    },
    {
        id: 8,
        title: "Do something 4",
        categoryId: 3,
        createdDate: "2022-06-04T13:53",
        deadlineDate: "",
        isDone: true,
        doneDate: "2022-06-04T13:56"
    },
]

const toDoTasksSlice = createSlice({
    name: 'toDoTasks',
    initialState,
    reducers: {
        taskAdded(state, action) {
            state.push(action.payload);
        },
        taskUpdated(state, action) {
            const {id, title, categoryId, deadlineDate} = action.payload;
            let existingTask = state.find(task => task.id === +id);
            if (existingTask) {
                existingTask.title = title;
                existingTask.categoryId = categoryId;
                existingTask.deadlineDate = deadlineDate;
            }
        },
        isDoneSwitched(state, action) {
            const {id} = action.payload;
            let existingTask = state.find(task => task.id === id);
            if (existingTask) {
                existingTask.isDone = !existingTask.isDone;
                if(existingTask.isDone){
                    existingTask.doneDate = DateTimeToString(new Date());
                }else{
                    existingTask.doneDate = "";
                }
            }
        },
        taskDeleted(state, action) {
            const {id} = action.payload;
            return state.filter(task => task.id !== id);
        }
    }
})
export const {taskAdded, taskUpdated, isDoneSwitched, taskDeleted} = toDoTasksSlice.actions;

export default toDoTasksSlice.reducer

export type ToDoTask = {
        id: number,
        title: string,
        categoryId: number,
        createdDate: string,
        deadlineDate: string,
        isDone: boolean,
        doneDate: string
}