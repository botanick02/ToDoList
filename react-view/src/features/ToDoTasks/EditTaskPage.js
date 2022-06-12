import React from "react";

import EditTaskForm from "./components/EditTaskForm";
import {useParams} from "react-router-dom";

export default function EditTaskPage() {
    let {taskId} = useParams();
    return (
        <EditTaskForm id={taskId}/>
    )
}