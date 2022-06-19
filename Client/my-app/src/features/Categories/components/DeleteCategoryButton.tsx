import React from "react";
import {useDispatch} from "react-redux";
import {categoryDeleted} from "../CategoriesSlice";
import {useAppDispatch} from "../../../app/hooks";

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
            dispatch(
                categoryDeleted({
                    id: categoryId
                })
            )
        }
    }

    return(
        <button className={"delete-button"} onClick={ConfirmationAlert}><span role="img" aria-label="cross">❌</span></button>
    )
}