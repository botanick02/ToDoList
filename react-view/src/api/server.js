import {factory, manyOf} from '@mswjs/data'


export const db = factory({
    toDoTasks: manyOf('toDoTask'),
    toDoTask: {
        id: Number,
        title: String,
        categoryId: Number,
        createdDate: String,
        deadlineDate: String,
        isDone: Boolean,
        doneDate: String
    },
    categories: manyOf('category'),
    category: {
        id: Number,
        name: String
    }
})