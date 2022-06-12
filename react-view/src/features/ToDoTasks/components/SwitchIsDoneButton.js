import React from "react";
import {useDispatch} from "react-redux";
import {isDoneSwitched} from "../ToDoTasksSlice";

export default function SwitchIsDoneButton(props) {
    const dispatch = useDispatch();

    const onSwitchIsDoneClicked = () => {
        if (props.id) {
            dispatch(
                isDoneSwitched({
                    id: props.id
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