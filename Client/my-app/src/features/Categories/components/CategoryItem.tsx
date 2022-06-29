import React from "react";
import {DeleteCategoryButton} from "./DeleteCategoryButton";
import {Link} from "react-router-dom";
import {Category} from "../types/Category";

interface CategoryItemProps{
    category: Category
}

export const CategoryItem = (props: CategoryItemProps) => {
    const category = props.category;

    return(
        <thead className={"category-item"} key={category.id}>
        <tr>
            <td className={"table-item-name"}>{category.name}</td>
            <td>
                <div className={"table-item-controls"}>
                    <Link to={`edit/${category.id}`} className={"edit-button"}>Edit</Link>
                    <DeleteCategoryButton categoryId={category.id}/>
                </div>
            </td>
        </tr>
        </thead>
    )
}