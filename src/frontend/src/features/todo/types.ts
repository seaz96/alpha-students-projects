export interface ITodo {
  id: string;
  content: string;
  checked: boolean;
  meetingId: string;
  parentId: string | null;
  children: ITodo[];
}

export interface PostTodoArgs {
  content: string;
  meetingId: string;
  parentId: string | null;
}

export interface PatchTodoArgs {
  todoId: string;
  checked: boolean;
  content: string;
}

export interface GetTodosArgs {
  meetingId: string;
}
