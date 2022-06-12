import React from "react";
import {useSelector} from "react-redux";

export function CategoryPicker(props) {
    const {setCategoryId, categoryId} = props;
    const categories = useSelector(state => state.categories)
    const renderedOptions = categories.map(category => (
        <option key={category.id} value={category.id}>{category.name}</option>
    ));

    function handleCategoryInput(event) {
        setCategoryId(event.target.value);
    }

    return (
        <div className={"category-filter"}>
            <form>
                <select name="categoryId" id="categoryId" value={categoryId} onChange={handleCategoryInput}>
                    <option value="">All</option>
                    {renderedOptions}
                </select>
            </form>
        </div>
    )
}