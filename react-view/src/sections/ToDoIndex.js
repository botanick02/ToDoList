import React, {useState} from "react";
import {ToDoTasksTable} from '../features/ToDoTasks/components/ToDoTasksTable'
import {AddTaskForm} from '../features/ToDoTasks/components/AddTaskForm'
import {CategoryFilterPicker} from '../features/ToDoTasks/components/CategoryFilterPicker'

export default function ToDoIndex() {
    const [categoryId, setCategoryId] = useState('');

    return (
        <>
            <AddTaskForm/>
            <CategoryFilterPicker category={categoryId} setCategoryId={setCategoryId}/>
            <div className={"tasks-wrapper"}>
                <div className={"current-tasks"}>
                    <h3>Current</h3>
                    <ToDoTasksTable isDone={false} categoryId={categoryId}/>
                </div>
            </div>
            <div className={"tasks-wrapper"}>
                <div className={"completed-tasks"}>
                    <h3>Completed</h3>
                    <ToDoTasksTable isDone={true} categoryId={categoryId}/>
                </div>
            </div>
        </>
    )
}

