import React from "react";
import StringToFormattedDateTimeString from "../../../Services/Convertors/StringToFormattedDateTimeString";
import {SwitchIsDoneButton} from "./SwitchIsDoneButton";
import {Link} from "react-router-dom";
import {DeleteTaskButton} from "./DeleteTaskButton";
import {ToDoTask} from "../ToDoTasksSlice";
import {useAppSelector} from '../../../app/hooks'


interface TaskTableItemProps {
    task: ToDoTask
}

export const TaskTableItem = (props: TaskTableItemProps) => {
    const task = props.task;
    const category = useAppSelector(state =>
        state.categories.find(category => category.id === +task.categoryId) ?? {name: "null"}).name;

    interface deadlineExpirationsStatus {
        class: string,
        text: string
    }

    const DeadlineField = () => {
        let status = (): deadlineExpirationsStatus | null => {
            let res = {} as deadlineExpirationsStatus;

            if (task.deadlineDate && !task.isDone) {
                if (Date.parse(task.deadlineDate) > Date.now()) {
                    res.class = "active";
                    res.text = "Active"
                } else {
                    res.class = "expired";
                    res.text = "Expired"
                }
                return res;
            }
            return null;
        }

        let statusRes = status();

        return (
            <>
                {StringToFormattedDateTimeString(task.isDone ? task.doneDate : task.deadlineDate)}
                {statusRes ? <div className={statusRes.class}>{statusRes.text}</div> : ""}
            </>
        )
    }

    return (
        <thead className={"task"} key={task.id}>
        <tr>
            <td className={"table-item-name"}>{task.title}</td>
            <td>{category !== "Uncategorized" ? category : ""}</td>
            <td>{DeadlineField()}</td>
            <td>{StringToFormattedDateTimeString(task.createdDate)}</td>
            <td>
                <div className={"table-item-controls"}>
                    <SwitchIsDoneButton taskId={task.id} isDone={task.isDone}/>
                    <Link to={`edit/${task.id}`} className={"edit-button"}>Edit</Link>
                    <DeleteTaskButton taskId={task.id}/>
                </div>
            </td>
        </tr>
        </thead>
    )
}