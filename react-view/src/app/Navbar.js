import React from "react";
import {Link} from 'react-router-dom'

export default function Navbar(){
    return(
        <div className="header">
            <Link to={"/"} className="navbar-logo">ToDoList</Link>
            <ul className="navigation">
                <li>
                    <Link to={"/categories"}>Categories</Link>
                </li>
            </ul>
        </div>
    )
}

