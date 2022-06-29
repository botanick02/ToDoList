import React, {useState} from 'react';
import {Navbar} from './Layouts/Header/Navbar'
import {
  Routes,
  Route, Navigate
} from "react-router-dom";
import {ToDoIndex} from './pages/ToDoIndex'
import {Categories} from './pages/Categories'
import {EditTaskPage} from "./features/ToDoTasks/EditTaskPage";
import {EditCategoryPage} from "./features/Categories/EditCategoryPage";
import {PageNotFound} from "./pages/pageNotFound";
import {useAppSelector} from "./app/hooks";


function App() {

    const source = useAppSelector(state => state.storageSources.currentSource);
    const [currentSource, setCurrentSource] = useState(source);

    return (
      <div className="App">
        <Navbar/>
        <Routes>
          <Route index element={<Navigate to={'/todo'}/>}/>
          <Route path={'todo/*'}>
            <Route index element={<ToDoIndex/>}/>
            <Route path={'edit/:taskId'} element={<EditTaskPage/>}/>
            <Route path={'*'} element={<PageNotFound/>}/>
          </Route>
          <Route path={'categories/*'}>
            <Route index element={<Categories/>}/>
            <Route path={'edit/:categoryId'} element={<EditCategoryPage/>}/>
            <Route path={'*'} element={<PageNotFound/>}/>
          </Route>
        </Routes>
      </div>
  );
}

export default App;
