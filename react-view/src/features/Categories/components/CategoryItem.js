import React from "react";
import DeleteCategoryButton from "./DeleteCategoryButton";
import {Link} from "react-router-dom";

export default function CategoryItem(props){
    const category = props.category;

    return(
        <div className={"category-item"}>
            <p>{category.name}</p>
            <Link to={`edit/${category.id}`} className={"edit-button"}>Edit</Link>
            <DeleteCategoryButton id={category.id}/>
        </div>
    )
}