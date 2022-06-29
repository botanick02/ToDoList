import {ToDoTask} from "../../features/ToDoTasks/types/ToDoTask";

export const SortToDoTasks = ( tasks: ToDoTask[], isDone: boolean): ToDoTask[] =>  {
    let sortFunc;
    if (isDone) {
        sortFunc = (a: ToDoTask, b: ToDoTask) => {
            let dateA = Date.parse(a.doneDate);
            let dateB = Date.parse(b.doneDate);
            return dateB - dateA;
        }
    } else {
        sortFunc = (a: ToDoTask, b: ToDoTask) => {
            if (a.deadlineDate === null) {
                return 1
            }
            if (b.deadlineDate === null) {
                return -1
            }
            let dateA = Date.parse(a.deadlineDate);
            let dateB = Date.parse(b.deadlineDate);
            return dateA - dateB;
        }
    }

    return tasks.sort(sortFunc);
}