import React, {useEffect} from "react";
import {AddCategoryForm} from "../features/Categories/components/AddCategoryForm";
import {CategoriesList} from "../features/Categories/components/CategoriesList";
import {fetchToDoTasks} from "../features/ToDoTasks/ToDoTasksSlice";
import {fetchCategories} from "../features/Categories/CategoriesSlice";
import {useAppDispatch} from "../app/hooks";

export const Categories = () => {

    const dispatch = useAppDispatch();

    useEffect(() => {
        dispatch(fetchCategories())
    })

    return (
        <>
            <AddCategoryForm/>
            <CategoriesList/>
        </>
    )
}

