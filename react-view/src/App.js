import React from 'react';
import Navbar from './features/header/Navbar'
import {
    Routes,
    Route, Navigate
} from "react-router-dom";
import ToDoIndex from './sections/ToDoIndex'
import Categories from './sections/Categories'
import EditTaskPage from "./features/ToDoTasks/EditTaskPage";
import EditCategoryPage from "./features/Categories/EditCategoryPage";
import PageNotFound from "./sections/pageNotFound";


function App() {
    return (
        <div className="App">
            <Navbar/>
            <Routes>
                <Route index element={<Navigate to={'/todo'}/>}/>
                <Route path={'todo/*'}>
                    <Route index element={<ToDoIndex/>}/>
                    <Route exact path={'edit/:taskId'} element={<EditTaskPage/>}/>
                    <Route path={'*'} element={<PageNotFound/>}/>
                </Route>
                <Route path={'categories/*'}>
                    <Route index element={<Categories/>}/>
                    <Route exact path={'edit/:taskId'} element={<EditCategoryPage/>}/>
                    <Route path={'*'} element={<PageNotFound/>}/>
                </Route>
            </Routes>
        </div>
    );
}

export default App;
