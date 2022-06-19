import React, {useState} from "react";
import {useNavigate} from "react-router-dom";
import {categoryUpdated} from "../CategoriesSlice";
import {useAppDispatch, useAppSelector} from '../../../app/hooks'

interface EditCategoryFormProps{
    categoryId: string
}

export const EditCategoryForm = (props: EditCategoryFormProps) => {
    const categoryId = props.categoryId;
    const category = useAppSelector(state =>
        state.categories.find(category => category.id === +categoryId)
    );

    const [name, setName] = useState(category?.name);

    const navigate = useNavigate();
    const dispatch = useAppDispatch();

    if (!category) {
        return (<h2>Category not found!</h2>);
    }


    const onNameChanged = (e: React.FormEvent<HTMLInputElement>) => setName(e.currentTarget.value);

    const handleSubmit = (event: React.FormEvent) =>{
        event.preventDefault();

        if (name) {
            dispatch(
                categoryUpdated({
                        id: categoryId,
                        name
                    }
                )
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