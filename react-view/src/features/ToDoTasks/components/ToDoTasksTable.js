import React from "react";
import {useSelector} from "react-redux";
import TaskTableItem from "./TaskTableItem";


export function ToDoTasksTable(props) {
    const toDoTasks = useSelector(state => state.toDoTasks)

    let toDoTasksFiltered = toDoTasks.filter(task => task.isDone === props.isDone);

    let sortFunc;
    if (props.isDone) {
        sortFunc = (a, b) => {
            let dateA = Date.parse(a.doneDate);
            let dateB = Date.parse(b.doneDate);
            return dateB - dateA;
        }
    } else {
        sortFunc = (a, b) => {
            let dateA = Date.parse(a.deadlineDate);
            let dateB = Date.parse(b.deadlineDate);
            return dateA - dateB;

        }
    }

    if (props.categoryId) {
        toDoTasksFiltered = toDoTasksFiltered.filter(task => task.categoryId === +props.categoryId);
    }

    toDoTasksFiltered.sort(sortFunc);

    const renderedToDoTasks = toDoTasksFiltered.map(task => (
        <TaskTableItem key={task.id} task={task}/>
    ))

    return (
        <table>
            <thead>
            <tr>
                <th>Task</th>
                <th>Category</th>
                <th>{props.isDone ? 'Date of completion' : 'Deadline'}</th>
                <th>Date of creation</th>
                <th></th>
            </tr>
            </thead>
            {renderedToDoTasks}
        </table>
    )
}