import React from "react";
import {useDispatch} from "react-redux";
import {categoryDeleted, deleteCategory} from "../CategoriesSlice";
import {useAppDispatch} from "../../../app/hooks";
import {CategoryDeleteInputType} from "../../../GraphQl/Categories/mutations";

interface DeleteCategoryButtonProps{
    categoryId: number
}

export const DeleteCategoryButton = (props: DeleteCategoryButtonProps) => {
    const categoryId = props.categoryId;
    const dispatch = useAppDispatch();

    const ConfirmationAlert = () => {
        if (window.confirm('Are you sure?')) {
            onDeleteCategoryClicked();
        }
    }

    const onDeleteCategoryClicked = () => {
        if (categoryId) {
            const category: CategoryDeleteInputType ={
                id: categoryId
            }

            dispatch(
                deleteCategory(category)
            )
        }
    }

    return(
        <button className={"delete-button"} onClick={ConfirmationAlert}><span role="img" aria-label="cross">❌</span></button>
    )
}