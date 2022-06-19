import React from "react";
import {useAppSelector} from '../../../app/hooks'



interface CategoryFilterPickerProps{
    categoryId: string,
    setCategoryId: React.Dispatch<React.SetStateAction<string>>
}

export const CategoryFilterPicker = (props: CategoryFilterPickerProps) => {
    const {setCategoryId, categoryId} = props;
    const categories = useAppSelector(state => state.categories)
    const renderedOptions = categories.map(category => (
        <option key={category.id} value={category.id}>{category.name}</option>
    ));

    function handleCategoryInput(event:  React.FormEvent<HTMLSelectElement>) {
        setCategoryId(event.currentTarget.value);
    }

    return (
        <div className={"category-filter"}>
            Filter:
            <form>
                <select name="categoryId" id="categoryId" value={categoryId} onChange={handleCategoryInput}>
                    <option value="">All</option>
                    {renderedOptions}
                </select>
            </form>
        </div>
    )
}