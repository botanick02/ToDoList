import {createSlice} from '@reduxjs/toolkit'


const initialState = {
    completed: [
        {
            id: "1",
            title: "Do something",
            categoryId: '2',
            category: "Work",
            createdDate: "05-05-2022 12:40:00",
            deadlineDate: "5-29-2022 12:40:00",
            isDone: false,
            doneDate: "05-29-2022 12:40:00"
        },
        {
            id: "2",
            title: "Do something 2",
            categoryId: '1',
            category: "Uncategorized",
            createdDate: "10-05-2022 12:40:00",
            deadlineDate: "5-30-2022 12:40:00",
            isDone: false,
            doneDate: "05-25-2022 09:40:00"
        },
        {
            id: "3",
            title: "Do something 3",
            categoryId: '3',
            category: "School",
            createdDate: "09-05-2022 12:40:00",
            deadlineDate: "5-29-2022 17:40:00",
            isDone: false,
            doneDate: "05-25-2022 12:41:00"
        },
        {
            id: "4",
            title: "Do something 5",
            categoryId: '3',
            category: "School",
            createdDate: "09-05-2022 12:40:00",
            deadlineDate: "",
            isDone: false,
            doneDate: "05-25-2022 12:40:00"
        },
    ],
    current: [{
        id: "5",
        title: "Do something",
        categoryId: '2',
        category: "Work",
        createdDate: "05-05-2022 12:40:00",
        deadlineDate: "5-29-2022 12:40:00",
        isDone: true,
        doneDate: ""
    },
        {
            id: "6",
            title: "Do something 2",
            categoryId: '1',
            category: "Uncategorized",
            createdDate: "10-05-2022 12:40:00",
            deadlineDate: "5-30-2022 12:40:00",
            isDone: true,
            doneDate: ""
        },
        {
            id: "7",
            title: "Do something 3",
            categoryId: '3',
            category: "School",
            createdDate: "09-05-2022 12:40:00",
            deadlineDate: "5-29-2022 17:40:00",
            isDone: true,
            doneDate: ""
        },
        {
            id: "8",
            title: "Do something 4",
            categoryId: '3',
            category: "School",
            createdDate: "09-05-2022 12:40:00",
            deadlineDate: "",
            isDone: true,
            doneDate: ""
        },
    ]
}

const toDoTasksSlice = createSlice({
    name: 'toDoTasks',
    initialState,
    reducers: {}
})

export default toDoTasksSlice.reducer