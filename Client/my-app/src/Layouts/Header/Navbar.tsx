import React from "react";
import {Link} from 'react-router-dom'
import {StoragePicker} from "../../features/StorageSources/StoragePicker";

export const Navbar = () => {
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

