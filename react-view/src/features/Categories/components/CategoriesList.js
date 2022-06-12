import React from "react";
import {useSelector} from "react-redux";
import CategoryItem from "./CategoryItem";

export default function CategoriesList() {
    const categories = useSelector(state => state.categories);

    return (
        <div className="categories-wrapper">
            <h3>Categories</h3>
            <div className="horizontal-list">
                {categories.map(category => (
                    category.id !== 1 ? <CategoryItem key={category.id} category={category}/> : ""
                ))}
            </div>
        </div>
    )
}