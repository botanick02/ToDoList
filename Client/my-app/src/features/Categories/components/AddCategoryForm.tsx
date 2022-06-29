import React, {useState} from "react";
import {createCategory} from "../CategoriesSlice";
import {useAppDispatch} from '../../../app/hooks'
import {CategoryCreateInputType} from "../../../GraphQl/Categories/mutations";

export const AddCategoryForm = () => {
    const [name, setName] = useState('');

    const dispatch = useAppDispatch();

    const onNameChanged = (e: React.FormEvent<HTMLInputElement>) => setName(e.currentTarget.value);
    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();

        if (name) {
            let category: CategoryCreateInputType = {
                name
            }
            dispatch(
                createCategory(category)
            )
            setName("");
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
                <input className={"submit-button"} type="submit" value={"Add"}/>
            </form>
        </div>
    )
}