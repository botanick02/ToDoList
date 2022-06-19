import React, {useState} from "react";
import {ToDoTasksTable} from '../features/ToDoTasks/components/ToDoTasksTable'
import {AddTaskForm} from '../features/ToDoTasks/components/AddTaskForm'
import {CategoryFilterPicker} from '../features/ToDoTasks/components/CategoryFilterPicker'

export const ToDoIndex = () => {
    const [categoryId, setCategoryId] = useState('');

    return (
        <>
            <AddTaskForm/>
            <CategoryFilterPicker categoryId={categoryId} setCategoryId={setCategoryId}/>
            <div className={"table-wrapper"}>
                <div className={"current-tasks"}>
                    <h3>Current</h3>
                    <ToDoTasksTable isDone={false} categoryId={categoryId}/>
                </div>
            </div>
            <div className={"table-wrapper"}>
                <div className={"completed-tasks"}>
                    <h3>Completed</h3>
                    <ToDoTasksTable isDone={true} categoryId={categoryId}/>
                </div>
            </div>
        </>
    )
}

