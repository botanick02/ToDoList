import {
    categoriesAdded,
    categoryCreated, categoryDeleted, categoryUpdated,
    createCategory, deleteCategory,
    fetchCategories, updateCategory
} from "../../features/Categories/CategoriesSlice";
import {RootState} from "../store";
import {combineEpics, Epic, ofType} from "redux-observable";
import {from, map, mergeMap, of} from "rxjs";
import {client} from "../../GraphQl/client";
import {GET_CATEGORIES} from "../../GraphQl/Categories/queries";
import {CREATE_CATEGORY, DELETE_CATEGORY, UPDATE_CATEGORY} from "../../GraphQl/Categories/mutations";
import {UPDATE_TODOTASK} from "../../GraphQl/ToDoTasks/mutations";

export const fetchCategoriesEpic: Epic<ReturnType<typeof fetchCategories>, any, RootState> = action$ => {
    return action$.pipe(
        ofType("categories/fetchCategories"),
        mergeMap(() =>
            from(client.query({
                    query: GET_CATEGORIES
                }
            )).pipe(map(res => categoriesAdded(res.data.categories.getAll)))
        )
    )
}

export const createCategoryEpic:  Epic<ReturnType<typeof createCategory>, any, RootState> = action$ => {
    return action$.pipe(
        ofType("categories/createCategory"),
        mergeMap( action => from(client.mutate({
            mutation: CREATE_CATEGORY,
            variables: {categoryCreateInputType: action.payload}
        })).pipe(map(res => categoryCreated(res.data.categories.create))))
    )
}

export const updateCategoryEpic:  Epic<ReturnType<typeof updateCategory>, any, RootState> = action$ => {
    return action$.pipe(
        ofType("categories/updateCategory"),
        mergeMap( action => from(client.mutate({
            mutation: UPDATE_CATEGORY,
            variables: {categoryUpdateInputType: action.payload}
        })).pipe(map(res => categoryUpdated(res.data.categories.update))))
    )
}

export const deleteCategoryEpic:  Epic<ReturnType<typeof deleteCategory>, any, RootState> = action$ => {
    return action$.pipe(
        ofType("categories/deleteCategory"),
        mergeMap( action => from(client.mutate({
            mutation: DELETE_CATEGORY,
            variables: {id: action.payload.id}
        })).pipe(map(res => categoryDeleted(res.data.categories.delete))))
    )
}

// @ts-ignore
export const CategoriesEpics = combineEpics(fetchCategoriesEpic, createCategoryEpic, deleteCategoryEpic, updateCategoryEpic);