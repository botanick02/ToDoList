import {ToDoTask} from "../../features/ToDoTasks/types/ToDoTask";
import {gql} from "@apollo/client";

export type ToDoTaskCreateInputType = {
    title: string,
    categoryId: number,
    deadlineDate: Date

}

export type ToDoTaskUpdateInputType = {
    id: number,
    title: string,
    categoryId: number,
    deadlineDate: Date

}

export type ToggleIsDoneInputType = {
    id: number
}
export type ToDoTaskDeleteInputType = {
    id: number
}

export const CREATE_TODOTASK = gql`
mutation Create($toDoTaskCreateInputType: ToDoTaskCreateInputType!){
  toDoTasks{
    create(toDoTaskCreateInputType: $toDoTaskCreateInputType){
        id
        title
        categoryId
        createdDate
        isDone
        deadlineDate
    }
  }
}
`

export const UPDATE_TODOTASK = gql`
mutation UpdateTask($toDoTaskUpdateInputType: ToDoTaskUpdateInputType!){
  toDoTasks{
    update(toDoTaskUpdateInputType: $toDoTaskUpdateInputType){
        id
        title
        categoryId
        createdDate
        isDone
        deadlineDate
    }
  }
}
`

export const TOGGLE_IS_DONE = gql`
mutation toggleIsDoneStatus($id: Int!){
  toDoTasks{
    toggleDoneStatus(id: $id){
      id
      isDone
      doneDate
    }
  }
}
`
export const DELETE_TODOTASK = gql`
    mutation Delete($id: Int!){
  toDoTasks{
    delete(id: $id){
      id
    }
  }
}
    `