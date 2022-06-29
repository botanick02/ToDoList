import React from "react";
import {CategoryItem} from "./CategoryItem";
import { useAppSelector } from '../../../app/hooks'

export const CategoriesList = () => {
    const categories = useAppSelector(state => state.categories);


    const renderedCategories = categories.categoriesList.map(category => (
        category.id !== 1 ? <CategoryItem key={category.id} category={category}/> : ""
    ))
    return (
        <div className="categories-wrapper">
            <h3>Categories:</h3>
            <div className="table-wrapper">
                <table>
                    <thead>
                    <tr>
                        <th>Name</th>
                        <th></th>
                    </tr>
                    </thead>
                    {renderedCategories.length > 0 ? renderedCategories :
                        <thead>
                        <tr>
                            <td colSpan={2}>Nothing here</td>
                        </tr>
                        </thead>}
                </table>
            </div>
        </div>
    )
}