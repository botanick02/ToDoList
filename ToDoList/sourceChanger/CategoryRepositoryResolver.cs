using Business.Models;
using ToDoList.sourceChanger.Enums;

public delegate ICategoryRepository CategoryRepositoryResolver(StorageSources key);