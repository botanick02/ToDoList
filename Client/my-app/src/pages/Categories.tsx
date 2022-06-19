import React from "react";
import {AddCategoryForm} from "../features/Categories/components/AddCategoryForm";
import {CategoriesList} from "../features/Categories/components/CategoriesList";

export const Categories = () => {
    return (
        <>
            <AddCategoryForm/>
            <CategoriesList/>
        </>
    )
}
