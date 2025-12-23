import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import { Input } from "@/components/ui/input";
import {
  useCreateTodoMutation,
  useDeleteTodoMutation,
  usePatchTodoMutation,
} from "@/features/todo/todoApi";
import type { ITodo } from "@/features/todo/types";
import { cn } from "@/lib/utils";
import {
  ChevronRightIcon,
  PlusIcon,
  Trash2Icon,
  MoreHorizontalIcon,
} from "lucide-react";
import { useState, useEffect, type KeyboardEvent } from "react";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";

interface TodoProps {
  todo: ITodo;
}

export default function Todo({ todo }: TodoProps) {
  const [patchTodo] = usePatchTodoMutation();
  const [createTodo] = useCreateTodoMutation();
  const [deleteTodo] = useDeleteTodoMutation();

  const [isExpanded, setIsExpanded] = useState(true);
  const [value, setValue] = useState(todo.content);
  const [isHovered, setIsHovered] = useState(false);

  useEffect(() => {
    setValue(todo.content);
  }, [todo.content]);

  const handleToggleCheck = (checked: boolean) => {
    patchTodo({
      todoId: todo.id,
      content: todo.content,
      checked,
    });
  };

  const handleSaveContent = () => {
    if (value !== todo.content) {
      patchTodo({
        todoId: todo.id,
        content: value,
        checked: todo.checked,
      });
    }
  };

  const handleKeyDown = (e: KeyboardEvent<HTMLInputElement>) => {
    if (e.key === "Enter") {
      e.currentTarget.blur();
    }
  };

  const handleAddChild = async () => {
    try {
      await createTodo({
        meetingId: todo.meetingId,
        parentId: todo.id,
        content: "Новая подзадача",
      }).unwrap();
      setIsExpanded(true);
    } catch (error) {
      console.error("Failed to create child todo", error);
    }
  };

  const handleDelete = () => {
    deleteTodo({ id: todo.id });
  };

  const hasChildren = todo.children && todo.children.length > 0;

  return (
    <div className="flex w-full flex-col">
      <div
        className={cn(
          "group hover:bg-accent/50 flex items-center gap-2 rounded-md px-2 py-1 transition-colors",
          todo.checked && "opacity-60",
        )}
        onMouseEnter={() => setIsHovered(true)}
        onMouseLeave={() => setIsHovered(false)}
      >
        <Button
          variant="ghost"
          size="icon"
          className={cn(
            "text-muted-foreground h-6 w-6 shrink-0 hover:bg-transparent",
            !hasChildren && "pointer-events-none opacity-0",
          )}
          onClick={() => setIsExpanded((prev) => !prev)}
        >
          <ChevronRightIcon
            className={cn(
              "h-4 w-4 transition-transform duration-200",
              isExpanded && "rotate-90",
            )}
          />
        </Button>

        <Checkbox
          checked={todo.checked}
          onCheckedChange={(val) => handleToggleCheck(!!val)}
        />

        <Input
          value={value}
          onChange={(e) => setValue(e.target.value)}
          onBlur={handleSaveContent}
          onKeyDown={handleKeyDown}
          className={cn(
            "h-8 border-none bg-transparent px-2 shadow-none focus-visible:ring-1",
            todo.checked && "text-muted-foreground line-through",
          )}
        />

        <div
          className={cn(
            "flex items-center gap-1 transition-opacity",
            isHovered
              ? "opacity-100"
              : "opacity-0 group-focus-within:opacity-100",
          )}
        >
          <Button
            variant="ghost"
            size="icon"
            className="h-7 w-7"
            onClick={handleAddChild}
            title="Добавить подзадачу"
          >
            <PlusIcon className="h-4 w-4" />
          </Button>

          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="ghost" size="icon" className="h-7 w-7">
                <MoreHorizontalIcon className="h-4 w-4" />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuItem
                className="text-destructive focus:text-destructive"
                onClick={handleDelete}
              >
                <Trash2Icon className="mr-2 h-4 w-4" />
                Удалить
              </DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
      </div>

      {hasChildren && isExpanded && (
        <div className="border-border/50 mt-1 ml-[1.35rem] flex flex-col border-l pl-2">
          {todo.children!.map((child) => (
            <Todo key={child.id} todo={child} />
          ))}
        </div>
      )}
    </div>
  );
}
