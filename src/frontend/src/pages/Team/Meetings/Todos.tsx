import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import type { IMeeting } from "@/features/meetings/types";
import {
  useCreateTodoMutation,
  useGetTodosQuery,
} from "@/features/todo/todoApi";
import { BlocksIcon, PlusIcon } from "lucide-react";
import { useState } from "react";
import Todo from "./Todo";

export default function Todos({ meeting }: { meeting: IMeeting }) {
  const [isOpen, setIsOpen] = useState(false);
  const { data: todos } = useGetTodosQuery(
    { meetingId: meeting.id },
    { skip: !isOpen },
  );
  const [createTodo] = useCreateTodoMutation();

  return (
    <Dialog open={isOpen} onOpenChange={setIsOpen}>
      <DialogTrigger asChild>
        <Button variant="outline" size="icon-sm">
          <BlocksIcon />
        </Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>{meeting.name} — Задачи</DialogTitle>
          <DialogDescription />
        </DialogHeader>

        {todos && (
          <div className="max-h-96 overflow-y-auto">
            {todos.data.map((todo) => (
              <Todo key={todo.id} todo={todo} />
            ))}
          </div>
        )}

        <Button
          className="w-fit"
          type="button"
          variant="outline"
          onClick={() => {
            createTodo({
              meetingId: meeting.id,
              parentId: null,
              content: "Новая задача",
            });
          }}
        >
          <PlusIcon />
          Создать
        </Button>
      </DialogContent>
    </Dialog>
  );
}
