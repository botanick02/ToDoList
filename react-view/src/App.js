import React from 'react';
import Navbar from './app/Navbar'
import {
    Routes,
    Route, Navigate
} from "react-router-dom";
import ToDoIndex from './components/ToDoIndex'
import Categories from './components/Categories'

function App() {
    return (
        <div className="App">
            <Navbar/>
            <Routes>
                <Route index element={<Navigate to={'/todo'}/>}/>
                <Route path={'todo/*'}>
                    <Route index element={<ToDoIndex/>}/>
                </Route>
                <Route path={'categories/*'}>
                    <Route index element={<Categories/>}/>
                </Route>
            </Routes>
        </div>
    );
}

export default App;
