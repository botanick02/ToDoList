import React, {useState} from "react";
import {addToDoTask, taskAdded} from '../ToDoTasksSlice'
import DateTimeToString from "../../../Services/Convertors/DateTimeToString";
import {useAppSelector, useAppDispatch} from '../../../app/hooks'
import {ToDoTaskCreateInputType} from "../../../GraphQl/ToDoTasks/mutations";


export const AddTaskForm = () => {
    const categories = useAppSelector(state => state.categories);
    const [title, setTitle] = useState('');
    const [categoryId, setCategoryId] = useState('1');
    const [deadlineDate, setDeadlineDate] = useState('');

    const dispatch = useAppDispatch();

    const onTitleChanged = (e: React.FormEvent<HTMLInputElement>) => setTitle(e.currentTarget.value);
    const onCategoryIdChanged = (e: React.FormEvent<HTMLSelectElement>) => setCategoryId(e.currentTarget.value);
    const onDeadlineChanged = (e: React.FormEvent<HTMLInputElement>) => setDeadlineDate(e.currentTarget.value);

    let categoriesList = [...categories.categoriesList];

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();

        if (title && categoryId) {
            const task: ToDoTaskCreateInputType = {
                title,
                categoryId: +categoryId,
                deadlineDate: new Date(deadlineDate)
            }
            dispatch(
                addToDoTask(task)
            )

            setTitle('');
            setCategoryId('1');
            setDeadlineDate('');
        }
    }

    return (
        <div className={"add-task"}>
            <h3>Add task:</h3>
            <form onSubmit={handleSubmit}>
                <label>
                    <p>Title</p>
                    <input size={50} type="text" value={title} onChange={onTitleChanged} required={true}/>
                </label>
                <label>
                    <p>Category</p>
                    <select value={categoryId} onChange={onCategoryIdChanged}>
                        {categoriesList.map(category => (
                            <option key={category.id} value={category.id}>{category.name}</option>
                        ))}
                    </select>
                </label>
                <label>
                    <p>Deadline (empty - no deadline)</p>
                    <input type="datetime-local" value={deadlineDate} onChange={onDeadlineChanged}/>
                </label>
                <input className={"submit-button"} type="submit" value={"Add"}/>
            </form>
        </div>
    )
}