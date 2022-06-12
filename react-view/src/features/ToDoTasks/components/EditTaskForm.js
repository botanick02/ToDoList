import React, {useState} from "react";
import {useDispatch, useSelector} from "react-redux";
import {taskUpdated} from "../ToDoTasksSlice";
import {useNavigate} from "react-router-dom"

export default function EditTaskForm(props) {
    const taskId = props.id;


    const task = useSelector(state =>
        state.toDoTasks.find(task => task.id === +taskId)
    );
    const categories = useSelector(state => state.categories);


    if (!task) {
        return (<h2>Task not found!</h2>)
    }

    const [title, setTitle] = useState(task.title);
    const [categoryId, setCategoryId] = useState(task.categoryId);
    const [deadlineDate, setDeadlineDate] = useState(task.deadlineDate);


    const onTitleChanged = e => setTitle(e.target.value);
    const onCategoryIdChanged = e => setCategoryId(e.target.value);
    const onDeadlineChanged = e => setDeadlineDate(e.target.value);

    let categoriesCopy = [...categories];

    const dispatch = useDispatch();
    const navigate = useNavigate();

    const onSaveTaskClicked = () => {
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
            <h3>Add task:</h3>
            <form>
                <div>
                    <p>Title</p>
                    <input type="text" size="50" value={title} onChange={onTitleChanged}/>
                </div>
                <div>
                    <p>Category</p>
                    <select value={categoryId} onChange={onCategoryIdChanged}>
                        {categoriesCopy.map(category => (
                            <option key={category.id} value={category.id}>{category.name}</option>
                        ))}
                    </select>
                </div>
                {
                    !task.isDone &&
                    <div>
                        <p>Deadline (empty - no deadline)</p>
                        <input type="datetime-local" value={deadlineDate} onChange={onDeadlineChanged}/>
                    </div>
                }
                <button className={"submit-button"} type="button" onClick={onSaveTaskClicked}>Save</button>
            </form>
        </div>
    )
}