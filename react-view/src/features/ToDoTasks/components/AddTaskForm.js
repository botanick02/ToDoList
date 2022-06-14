import React, {useState} from "react";
import {useSelector} from "react-redux";
import {useDispatch} from 'react-redux'

import {taskAdded} from '../ToDoTasksSlice'
import DateTimeToString from "../../../Convertors/DateTimeToString";


export function AddTaskForm() {
    const categories = useSelector(state => state.categories);
    const [title, setTitle] = useState('');
    const [categoryId, setCategoryId] = useState(1);
    const [deadlineDate, setDeadlineDate] = useState('');

    const dispatch = useDispatch();

    const onTitleChanged = e => setTitle(e.target.value);
    const onCategoryIdChanged = e => setCategoryId(e.target.value);
    const onDeadlineChanged = e => setDeadlineDate(e.target.value);

    let categoriesCopy = [...categories];

    const onAddTaskClicked = () => {
        if (title && categoryId) {
            let currentDate = new Date();
            dispatch(
                taskAdded({
                    id: Date.now(),
                    title,
                    categoryId,
                    deadlineDate,
                    isDone: false,
                    createdDate: DateTimeToString(currentDate),
                    doneDate: ''
                })
            )
            setTitle('');
            setCategoryId(1);
            setDeadlineDate('');

        }
    }



    return (
        <div className={"add-task"}>
            <h3>Add task:</h3>
            <form>
                <label>
                    <p>Title</p>
                    <input type="text" size="50" value={title} onChange={onTitleChanged}/>
                </label>
                <label>
                    <p>Category</p>
                    <select value={categoryId} onChange={onCategoryIdChanged}>
                        {categoriesCopy.map(category => (
                            <option key={category.id} value={category.id}>{category.name}</option>
                        ))}
                    </select>
                </label>
                <label>
                    <p>Deadline (empty - no deadline)</p>
                    <input type="datetime-local" value={deadlineDate} onChange={onDeadlineChanged}/>
                </label>
                <button className={"submit-button"} type="button" onClick={onAddTaskClicked}>Add</button>
            </form>
        </div>
    )
}