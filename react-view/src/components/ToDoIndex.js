import React, {useState} from "react";
import {ToDoTasksTable} from '../features/ToDoTasks/ToDoTasksTable'
import {CategoryPicker} from '../app/categoryPicker'

export default function ToDoIndex() {
    const [categoryId, setCategoryId] = useState('');

    return (
        <>
            <div className="addTask">
                <h3 >Add task:</h3>
                <form method="post">
                    <div>
                        <p>Title</p>
                        <input type="text" size="50"/>
                    </div>
                    <div>
                        <p>Category</p>
                        <select>

                        </select>
                    </div>
                    <div>
                        <p>Deadline (empty - no deadline)</p>
                        <input type="datetime-local"/>
                    </div>
                    <input type="submit" value="Create"/>
                </form>
            </div>
            <CategoryPicker category={categoryId} setCategoryId={setCategoryId}/>
            <div className="tasksWrapper">
                <div className="currentTasks">
                    <h3>Current</h3>
                    <ToDoTasksTable isDone={false} categoryId={categoryId}/>
                </div>
            </div>
            <div className="tasksWrapper">
                <div className="completedTasks">
                    <h3>Completed</h3>
                    <ToDoTasksTable isDone={true} categoryId={categoryId}/>
                </div>
            </div>
        </>
    )
}

