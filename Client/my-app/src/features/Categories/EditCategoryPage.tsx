import React from "react";

import {useParams} from "react-router-dom";
import {EditCategoryForm} from "./components/EditCategoryForm";

export const EditCategoryPage = () => {
    let {categoryId} = useParams();
    if(!categoryId){
        return (<h2 className={"page_not_found"}>Category not found! :(</h2>)
    }

    return (
        <EditCategoryForm categoryId={categoryId}/>
    )
}