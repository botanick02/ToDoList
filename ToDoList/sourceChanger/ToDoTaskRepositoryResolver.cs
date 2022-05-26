using Business.Models;
using ToDoList.sourceChanger.Enums;

public delegate IToDoTaskRepository ToDoTaskRepositoryResolver(StorageSources key);