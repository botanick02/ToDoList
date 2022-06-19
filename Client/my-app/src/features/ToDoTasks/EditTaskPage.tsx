import React from "react";

import {EditTaskForm} from "./components/EditTaskForm";
import {useParams} from "react-router-dom";

export const EditTaskPage = () => {
    let {taskId} = useParams();
    if(!taskId){
        return (<h2 className={"page_not_found"}>Task not found! :(</h2>)
    }
    return (
        <EditTaskForm taskId={taskId}/>
    )
}