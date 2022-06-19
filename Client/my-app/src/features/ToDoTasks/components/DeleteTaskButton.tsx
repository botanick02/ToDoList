import React from "react";
import {taskDeleted} from "../ToDoTasksSlice";
import {useAppDispatch} from '../../../app/hooks';

interface DeleteTaskButtonProps{
    taskId: number
}

export const DeleteTaskButton = (props: DeleteTaskButtonProps) => {
    const taskId = props.taskId;
    const dispatch = useAppDispatch();

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