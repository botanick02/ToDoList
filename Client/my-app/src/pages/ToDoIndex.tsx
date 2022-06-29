import React, {useEffect, useState} from "react";
import {ToDoTasksTable} from '../features/ToDoTasks/components/ToDoTasksTable'
import {AddTaskForm} from '../features/ToDoTasks/components/AddTaskForm'
import {CategoryFilterPicker} from '../features/ToDoTasks/components/CategoryFilterPicker'
import {useAppDispatch} from "../app/hooks";
import {fetchToDoTasks} from "../features/ToDoTasks/ToDoTasksSlice";
import {fetchCategories} from "../features/Categories/CategoriesSlice";

export const ToDoIndex = () => {
    const [categoryId, setCategoryId] = useState('');


    const dispatch = useAppDispatch();

    useEffect(() => {
        let catId = categoryId ? +categoryId : null;
        dispatch(fetchToDoTasks({categoryId: catId}));
        dispatch(fetchCategories())
    })


    return (
        <>
            <AddTaskForm/>
            <CategoryFilterPicker categoryId={categoryId} setCategoryId={setCategoryId}/>
            <div className={"table-wrapper"}>
                <div className={"current-tasks"}>
                    <h3>Current</h3>
                    <ToDoTasksTable isDone={false}/>
                </div>
            </div>
            <div className={"table-wrapper"}>
                <div className={"completed-tasks"}>
                    <h3>Completed</h3>
                    <ToDoTasksTable isDone={true}/>
                </div>
            </div>
        </>
    )
}

