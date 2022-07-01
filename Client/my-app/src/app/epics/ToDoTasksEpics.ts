import {RootState} from "../store";
import {
    addToDoTask,
    completedTasksAdded,
    currentTasksAdded, deleteToDoTask,
    fetchToDoTasks, isDoneToggled, taskAdded, taskDeleted, taskUpdated, toggleIsDone
} from "../../features/ToDoTasks/ToDoTasksSlice"
import {ofType, combineEpics, Epic} from "redux-observable";
import {catchError, from, map, merge, mergeMap, of} from "rxjs";
import {client} from "../../GraphQl/client";
import {GET_TODOTASKS} from "../../GraphQl/ToDoTasks/queries";
import {CREATE_TODOTASK, DELETE_TODOTASK, TOGGLE_IS_DONE, UPDATE_TODOTASK} from "../../GraphQl/ToDoTasks/mutations";


export const fetchToDoTasksEpic: Epic<ReturnType<typeof fetchToDoTasks>, any, RootState> = action$ => {
    return action$.pipe(
        ofType("toDoTasks/fetchToDoTasks"),
        mergeMap(action => {
                const currentTasksRequestObservable = from(client.query({
                    query: GET_TODOTASKS,
                    variables: {isDone: false, categoryId: action.payload.categoryId}
                }))
                const completedTasksRequestObservable = from(client.query({
                    query: GET_TODOTASKS,
                    variables: {isDone: true, categoryId: action.payload.categoryId}
                }))

                const completedTasksObservable = completedTasksRequestObservable.pipe(map(response => completedTasksAdded(response.data.toDoTasks.getAll)))
                const currentTasksObservable = currentTasksRequestObservable.pipe(map(response => currentTasksAdded(response.data.toDoTasks.getAll)))
                return merge(completedTasksObservable, currentTasksObservable);

            }
        ))
}

export const addToDoTaskEpic: Epic<ReturnType<typeof addToDoTask>, any, RootState> = action$ => {
    return action$.pipe(
        ofType("toDoTasks/addToDoTask"),
        mergeMap(action => from(client.mutate({
                mutation: CREATE_TODOTASK,
                variables: {toDoTaskCreateInputType: action.payload}
            })).pipe(map(response => taskAdded(response.data.toDoTasks.create)))
        )
    )
}

export const updateToDoTaskEpic: Epic<ReturnType<typeof addToDoTask>, any, RootState> = action$ => {
    return action$.pipe(
        ofType("toDoTasks/updateToDoTask"),
        mergeMap(action => from(client.mutate({
                mutation: UPDATE_TODOTASK,
                variables: {toDoTaskUpdateInputType: action.payload}
            })).pipe(map(response => taskUpdated(response.data.toDoTasks.update)))
        )
    )
}

export const toggleIsDoneEpic: Epic<ReturnType<typeof toggleIsDone>, any, RootState> = action$ => {
    return action$.pipe(
        ofType("toDoTasks/toggleIsDone"),
        mergeMap(action => from(client.mutate({
                mutation: TOGGLE_IS_DONE,
                variables: {id: action.payload.id}
            })).pipe(map(response => isDoneToggled(response.data.toDoTasks.toggleDoneStatus)))
        )
    )
}

export const deleteToDoTaskEpic: Epic<ReturnType<typeof deleteToDoTask>, any, RootState> = action$ => {
    return action$.pipe(
        ofType("toDoTasks/deleteToDoTask"),
        mergeMap(action => from(client.mutate({
            mutation: DELETE_TODOTASK,
            variables: {id: action.payload.id}
        })).pipe(map(response => taskDeleted(response.data.toDoTasks.delete))))
    )
}

// @ts-ignore
export const ToDoTasksEpics = combineEpics(addToDoTaskEpic, fetchToDoTasksEpic, toggleIsDoneEpic, deleteToDoTaskEpic, updateToDoTaskEpic);