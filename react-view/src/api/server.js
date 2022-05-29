import { factory, manyOf, primaryKey } from '@mswjs/data'
import { nanoid } from '@reduxjs/toolkit'


export const db = factory({
   toDoTasks : manyOf('toDoTask'),
    toDoTask: {
        id: primaryKey(nanoid),
        title: String,
        categoryId: Int16Array,
        category: String,
        createdDate: String,
        deadlineDate: String,
        isDone: Boolean,
        doneDate: String
    },
    categories : manyOf('category'),
    category: {
        id: primaryKey(nanoid),
        name: String
    }
})