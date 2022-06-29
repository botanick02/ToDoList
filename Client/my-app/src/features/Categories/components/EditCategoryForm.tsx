import React, {useState} from "react";
import {useNavigate} from "react-router-dom";
import {categoryUpdated, updateCategory} from "../CategoriesSlice";
import {useAppDispatch, useAppSelector} from '../../../app/hooks'
import {CategoryCreateInputType, CategoryUpdateInputType} from "../../../GraphQl/Categories/mutations";

interface EditCategoryFormProps {
    categoryId: string
}

export const EditCategoryForm = (props: EditCategoryFormProps) => {
    const categoryId = props.categoryId;
    const category = useAppSelector(state =>
        state.categories.categoriesList.find(category => category.id === +categoryId)
    );

    const [name, setName] = useState(category?.name);

    const navigate = useNavigate();
    const dispatch = useAppDispatch();

    if (!category) {
        return (<h2>Category not found!</h2>);
    }


    const onNameChanged = (e: React.FormEvent<HTMLInputElement>) => setName(e.currentTarget.value);

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();

        if (name) {
            const category: CategoryUpdateInputType = {
                id: +categoryId,
                name
            }

            dispatch(
                updateCategory(category)
            )
            navigate('/categories')
        }
    }

    return (
        <div className={"add-category"}>
            <h3>Add category:</h3>
            <form onSubmit={handleSubmit}>
                <label>
                    Name:
                    <input type={"text"} value={name} onChange={onNameChanged} required={true}/>
                </label>
                <input className={"submit-button"} type="submit" value={"Save"}/>
            </form>
        </div>
    )
}