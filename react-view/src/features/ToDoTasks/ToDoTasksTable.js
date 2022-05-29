import React from "react";
import {useSelector} from "react-redux";


export function ToDoTasksTable(props){
    const toDoTasks = useSelector(state => state.toDoTasks)

    let toDoTasksCopy;
    let sortFunc;
    if (props.isDone) {
        toDoTasksCopy = [...toDoTasks.completed];
        sortFunc = (a, b) => {
            let dateA = Date.parse(a.doneDate);
            let dateB = Date.parse(b.doneDate);
            return dateB - dateA;
        }
    } else {
        toDoTasksCopy = [...toDoTasks.current];
        sortFunc = (a, b) => {
            let dateA = Date.parse(a.deadlineDate);
            let dateB = Date.parse(b.deadlineDate);
            return dateA - dateB;

        }
    }

    if(props.categoryId){
        toDoTasksCopy = toDoTasksCopy.filter(task => task.categoryId == props.categoryId);
    }

    toDoTasksCopy.sort(sortFunc);

    const renderedToDoTasks = toDoTasksCopy.map(task => (
        <thead className="task" key={task.id}>
        <tr>
            <td className="taskName">{task.title}</td>
            <td>{task.category != "Uncategorized" ? task.category : ""}</td>
            <td> {props.isDone ? task.doneDate : task.deadlineDate} </td>
            <td>{task.createdDate}</td>
            <td>
                <div className="taskControl">
                    <form method="post">
                        <button className="setDoneButt" type="submit" name="Id" value="{}">Complete✔</button>
                    </form>
                    <form method="post">
                        <button className="editButt" type="submit" name="Id" value="">Edit</button>
                    </form>
                    <button data-id="" className="deleteButt">❌</button>
                    <div className="delConfirmationTask" id="delConfirmation@(task.Id)">
                        <p>Are you sure?</p>
                        <form action="/ToDoList/Delete" method="post">
                            <div className="delDecline">No</div>
                            <button className="delAccept" type="submit" name="Id" value="@task.Id">Yes
                            </button>
                        </form>
                    </div>
                </div>
            </td>
        </tr>
        </thead>

    ))

    return (
        <table>
            <thead>
            <tr>
                <th>Task</th>
                <th>Category</th>
                <th>{props.isDone ? 'Date of completion' : 'Deadline'}</th>
                <th>Date of creation</th>
                <th></th>
            </tr>
            </thead>
            {renderedToDoTasks}
        </table>
    )
}