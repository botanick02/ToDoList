import React from "react";

import {useParams} from "react-router-dom";
import EditCategoryForm from "./components/EditCategoryForm";

export default function EditCategoryPage() {
    let {taskId} = useParams();
    return (
        <EditCategoryForm id={taskId}/>
    )
}