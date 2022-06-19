import React from "react";
import {TaskTableItem} from "./TaskTableItem";
import {useAppSelector} from '../../../app/hooks'
import {ToDoTask} from "../ToDoTasksSlice";

interface ToDoTasksTableProps {
    isDone: boolean,
    categoryId: string
}

export const ToDoTasksTable = (props: ToDoTasksTableProps) => {
    const toDoTasks = useAppSelector(state => state.toDoTasks)

    let toDoTasksFiltered = toDoTasks.filter(task => task.isDone === props.isDone);

    let sortFunc;
    if (props.isDone) {
        sortFunc = (a: ToDoTask, b: ToDoTask) => {
            let dateA = Date.parse(a.doneDate);
            let dateB = Date.parse(b.doneDate);
            return dateB - dateA;
        }
    } else {
        sortFunc = (a: ToDoTask, b: ToDoTask) => {
            if (a.deadlineDate === "") {
                return 1
            }
            if (b.deadlineDate === "") {
                return -1
            }
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
            {renderedToDoTasks.length > 0 ? renderedToDoTasks :
                <thead>
                <tr>
                    <td colSpan={5}>Nothing here</td>
                </tr>
                </thead>}
        </table>
    )
}