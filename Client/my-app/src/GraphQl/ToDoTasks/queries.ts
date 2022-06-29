import {gql} from "@apollo/client";

export const GET_TODOTASKS = gql`
       query GetTasks($isDone: Boolean, $categoryId: Int)
{
  toDoTasks {
   getAll(isDone: $isDone, categoryId: $categoryId) {
    id
    title
    categoryId
    createdDate
    isDone
    deadlineDate
    doneDate
  }
}
}
    `;

export interface FetchToDoTasksInputType {
    categoryId?: number | null
}
