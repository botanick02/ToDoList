import React, {useEffect} from "react";
import {TaskTableItem} from "./TaskTableItem";
import {useAppSelector} from '../../../app/hooks'
import {ToDoTask} from "../types/ToDoTask";
import {SortToDoTasks} from "../../../Services/Sorters/SortToDoTasks";

interface ToDoTasksTableProps {
    isDone: boolean,
}

export const ToDoTasksTable = (props: ToDoTasksTableProps) => {
    const allToDoTasks = useAppSelector(state => state.toDoTasks)

    let requiredToDoTasks = props.isDone ? [...allToDoTasks.completedTasks] : [...allToDoTasks.currenTasks];

    SortToDoTasks(requiredToDoTasks, props.isDone);

    const renderedToDoTasks = requiredToDoTasks.map(task => (
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