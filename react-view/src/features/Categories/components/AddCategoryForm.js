import React, {useState} from "react";
import {useDispatch} from "react-redux";
import {categoryAdded} from "../CategoriesSlice";

export default function AddCategoryForm() {
    const [name, setName] = useState('');

    const dispatch = useDispatch();

    const onNameChanged = e => setName(e.target.value);
    const onAddCategoryClicked = () => {
        if (name) {
            dispatch(
                categoryAdded({
                    id: Date.now(),
                    name
                    }
                )
            )
        }
    }

    return (
        <div className={"add-category"}>
            <h3>Add category:</h3>
            <form>
                <label>
                    Name:
                    <input type={"text"} value={name} onChange={onNameChanged}/>
                </label>
                <button className={"submit-button"} type="button" onClick={onAddCategoryClicked}>Add</button>
            </form>
        </div>
    )
}