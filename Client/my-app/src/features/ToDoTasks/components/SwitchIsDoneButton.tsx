import React from "react";
import {isDoneSwitched} from "../ToDoTasksSlice";
import {useAppDispatch } from '../../../app/hooks'

interface SwitchIsDoneButtonProps{
    taskId: number,
    isDone: boolean
}

export const SwitchIsDoneButton = (props: SwitchIsDoneButtonProps) => {
    const dispatch = useAppDispatch();

    const onSwitchIsDoneClicked = () => {
        if (props.taskId) {
            dispatch(
                isDoneSwitched({
                    id: props.taskId
                })
            );
        }
    }
    return (
        <button className={props.isDone ? "setNotDoneButt" : "set-done-button"} onClick={onSwitchIsDoneClicked}>
            <span>{props.isDone ? "Incomplete⮌" : "Complete✔"}</span>
        </button>
    )
}