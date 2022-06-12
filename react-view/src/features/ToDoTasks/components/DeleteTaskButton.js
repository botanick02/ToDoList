import React from "react";
import {useDispatch} from "react-redux";
import {taskDeleted} from "../ToDoTasksSlice";
import 'react-confirm-alert/src/react-confirm-alert.css';

export default function DeleteTaskButton(props) {
    const taskId = props.id;
    const dispatch = useDispatch();

    const ConfirmationAlert = () => {
        if (window.confirm('Are you sure?')) {
            onDeleteTaskClicked();
        }
    }

    const onDeleteTaskClicked = () => {
        if (taskId) {
            dispatch(
                taskDeleted({
                    id: taskId
                })
            )
        }
    }
    return (
        <button className={"delete-button"} onClick={ConfirmationAlert}><span role="img" aria-label="cross">❌</span></button>
    )
}