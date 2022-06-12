import React from "react";
import StringToFormattedDateTimeString from "../../../Convertors/StringToFormattedDateTimeString";
import SwitchIsDoneButton from "./SwitchIsDoneButton";
import {Link} from "react-router-dom";
import DeleteTaskButton from "./DeleteTaskButton";
import {useSelector} from "react-redux";

export default function TaskTableItem(props){
    const task = props.task;
    const category = useSelector(state =>
        state.categories.find(category => category.id === +task.categoryId)).name;
    return(
        <thead className={"task"} key={task.id}>
        <tr>
            <td className={"task-name"}>{task.title}</td>
            <td>{category !== "Uncategorized" ? category : ""}</td>
            <td>{StringToFormattedDateTimeString(task.isDone ? task.doneDate : task.deadlineDate)}</td>
            <td>{StringToFormattedDateTimeString(task.createdDate)}</td>
            <td>
                <div className={"task-controls"}>
                    <SwitchIsDoneButton id={task.id} isDone={task.isDone}/>
                    <Link to={`edit/${task.id}`} className={"edit-button"}>Edit</Link>
                    <DeleteTaskButton id={task.id}/>
                </div>
            </td>
        </tr>
        </thead>
    )
}