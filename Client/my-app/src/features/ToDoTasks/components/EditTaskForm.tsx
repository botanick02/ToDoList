import React, {useState} from "react";
import {taskUpdated} from "../ToDoTasksSlice";
import {useNavigate} from "react-router-dom"
import { useAppSelector, useAppDispatch } from '../../../app/hooks'


interface EditTaskFormProps{
    taskId: string
}

export const EditTaskForm = (props: EditTaskFormProps) => {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();



    const taskId = props.taskId;

    const task = useAppSelector(state =>
        state.toDoTasks.find(task => task.id === +taskId)
    );

    const categories = useAppSelector(state => state.categories);

    const [title, setTitle] = useState(task?.title);
    const [categoryId, setCategoryId] = useState(task?.categoryId.toString());
    const [deadlineDate, setDeadlineDate] = useState(task?.deadlineDate);

    if (!task) {
        return (<h2 className={"page_not_found"}>Task not found! :(</h2>)
    }


    const onTitleChanged = (e:  React.FormEvent<HTMLInputElement>) => setTitle(e.currentTarget.value);
    const onCategoryIdChanged = (e:  React.FormEvent<HTMLSelectElement>) => setCategoryId(e.currentTarget.value);
    const onDeadlineChanged = (e: React.FormEvent<HTMLInputElement>) => setDeadlineDate(e.currentTarget.value);

    let categoriesCopy = [...categories];


    const handleSubmit = (event: React.FormEvent) =>{
        event.preventDefault();

        if (title && categoryId) {
            dispatch(taskUpdated({
                id: taskId,
                title,
                categoryId,
                deadlineDate
            }))
            navigate('/todo')
        }
    }

    return (
        <div className={"add-task"}>
            <h3>Edit task:</h3>
            <form onSubmit={handleSubmit}>
                <label>
                    <p>Title</p>
                    <input type="text" size={50} value={title} onChange={onTitleChanged} required={true}/>
                </label>
                <label>
                    <p>Category</p>
                    <select value={categoryId} onChange={onCategoryIdChanged}>
                        {categoriesCopy.map(category => (
                            <option key={category.id} value={category.id}>{category.name}</option>
                        ))}
                    </select>
                </label>
                {
                    !task.isDone &&
                    <label>
                        <p>Deadline (empty - no deadline)</p>
                        <input type="datetime-local" value={deadlineDate} onChange={onDeadlineChanged}/>
                    </label>
                }
                <input className={"submit-button"} type="submit" value={"Save"}/>
            </form>
        </div>
    )
}