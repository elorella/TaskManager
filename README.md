# TaskManager

**Problem:* : This is a console application to simulate how a task  Task manager is the component that is designed for handling multiple processes inside an operating system. Each process is identified by 2 fields, a unique unmodifiable identifier (PID), and a priority (low, medium, high).
The process is immutable, it is generated with a priority and will die with this priority – each process has a kill() method that will destroy it. 
Depending on the customer adding a new process to task manager behaviour will change. 

Pipeline is set up to publish the artifacts for windows. You can download the latest one via;
```
curl -H "Circle-Token: 72970137130bf50a28b4ff1b4600426f0f654076" https://circleci.com/api/v1.1/project/github/elorella/TaskManager/latest/artifacts
```

# How to run 
Run TaskManager.UI.exe to start the application. 

# About the application : There are 3 projects in the solution.
TaskManager.UI : It is a console application to interact with TaskManager business layer.
TaskManager business logic : I implemented Strategy Design Pattern to modify the behavior on runtime. Based on customer type, one of the task-managers listed below, will run.
TaskManager
FifoTaskManager
PriorityTaskManager
I also applied Simple Factory Pattern to centralise the creation of task-managers. This is also where I set the max task limit (to 5) for task-manager with a possibility to override the limit during task-manager creation.
TaskManager.Test is unit test projects with Xunit framework.
- Test result can be seen in circle-ci. I think I need to invite you as 'team members'; please let me know if you would like to see the pipeline.
- UI is not covered by unit tests as I think, console won't be a long living UI for the application.

# Next steps;
- Abstract factory pattern can be implemented, in order to remove the "if" block in the factory class.
- GetById and GetByPriority functions in BaseTaskManager can ben moved to a repository.
- I keep a counter in task manager in order to assign PID to a new process and I don't recycle the ids that has been deleted. That's why, application is limited to int.32 max value.  
- Max task limit is hardcoded as 5, can be moved to configuration.
- A mapping layer (IMapper) between Process and ProcessDto can be implemented.
- An exception handling layer can be added.  
 
