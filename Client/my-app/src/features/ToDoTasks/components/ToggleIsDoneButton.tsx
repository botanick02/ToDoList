import React from "react";
import {useAppDispatch} from '../../../app/hooks'
import {toggleIsDone} from "../ToDoTasksSlice";

interface SwitchIsDoneButtonProps {
    taskId: number,
    isDone: boolean
}

export const ToggleIsDoneButton = (props: SwitchIsDoneButtonProps) => {
    const dispatch = useAppDispatch();

    const onSwitchIsDoneClicked = () => {
        if (props.taskId){
            dispatch(
                toggleIsDone({id: props.taskId})
            )
        }
    }
    return (
        <button className={props.isDone ? "setNotDoneButt" : "set-done-button"} onClick={onSwitchIsDoneClicked}>
            <span>{props.isDone ? "Incomplete⮌" : "Complete✔"}</span>
        </button>
    )
}