import React, {useEffect} from "react";
import {Link} from 'react-router-dom'
import {StoragePicker} from "../../features/StorageSources/StoragePicker";
import {useAppDispatch} from "../../app/hooks";
import {fetchStorageSourcesData} from "../../features/StorageSources/StorageSourcesSlice";

export const Navbar = () => {
    const dispatch = useAppDispatch();

    useEffect(() => {
        dispatch(fetchStorageSourcesData());
    })


    return (
        <div className={"header"}>
            <Link to={"/"} className={"navbar-logo"}>ToDoList</Link>
            <ul className={"navigation"}>
                <li>
                    <Link to={"/categories"}>Categories</Link>
                </li>
            </ul>
            <StoragePicker/>
        </div>
    )
}

