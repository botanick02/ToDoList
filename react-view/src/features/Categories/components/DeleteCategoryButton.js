import React from "react";
import {useDispatch} from "react-redux";
import {categoryDeleted} from "../CategoriesSlice";

export default function DeleteCategoryButton(props){
    const categoryId = props.id;
    const dispatch = useDispatch();

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