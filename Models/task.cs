namespace tl2_tp09_2023_InakiPoch.Models;

public enum TaskState { Ideas, ToDo, Doing, Review, Done }

public class Task {
    int id;
    int boardId;
    string name;
    TaskState state;
    string description;
    string color;
    int? assignedUserId;
}