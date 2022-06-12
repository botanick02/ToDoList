import React, {useState} from "react";
import {useDispatch, useSelector} from "react-redux";
import {useNavigate} from "react-router-dom";
import {categoryUpdated} from "../CategoriesSlice";

export default function EditCategoryForm(props) {
    const categoryId = props.id;
    const category = useSelector(state =>
        state.categories.find(category => category.id === +categoryId)
    );

    if (!category) {
        return (<h2>Category not found!</h2>);
    }

    const [name, setName] = useState(category.name);

    const navigate = useNavigate();
    const dispatch = useDispatch();

    const onNameChanged = e => setName(e.target.value);
    const onUpdateCategoryClicked = () => {
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
            <form>
                <label>
                    Name:
                    <input type={"text"} value={name} onChange={onNameChanged}/>
                </label>
                <button className={"submit-button"} type="button" onClick={onUpdateCategoryClicked}>Save</button>
            </form>
        </div>
    )
}