import { Button } from "@/components/ui/button";
import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
  CommandList,
} from "@/components/ui/command";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { useGetCasesQuery } from "@/features/cases/casesApi";
import { useCreateProjectMutation } from "@/features/projects/projectsApi";
import { cn } from "@/lib/utils";
import { CheckIcon, ChevronsUpDownIcon } from "lucide-react";
import { useState, type ComponentProps } from "react";

export default function CreateNewProject({
  className,
  ...rest
}: ComponentProps<"div">) {
  // TODO: Pagination
  const { data, isLoading } = useGetCasesQuery({ limit: 9999, offset: 0 });
  const [isOpen, setIsOpen] = useState(false);
  const [value, setValue] = useState("");

  const [createProject] = useCreateProjectMutation();

  if (isLoading || data === undefined) return null;

  return (
    <div className={cn("flex gap-2", className)} {...rest}>
      <Popover open={isOpen} onOpenChange={setIsOpen}>
        <PopoverTrigger asChild>
          <Button
            variant="outline"
            role="combobox"
            aria-expanded={isOpen}
            className="w-[400px] justify-between"
          >
            <span className="truncate">
              {value
                ? data.find((c) => c.id === value)?.name
                : "Выберите кейс..."}
            </span>
            <ChevronsUpDownIcon className="ml-2 size-4 shrink-0 opacity-50" />
          </Button>
        </PopoverTrigger>
        <PopoverContent className="w-[400px] p-0">
          <Command>
            <CommandInput placeholder="Выберите кейс..." />
            <CommandList>
              <CommandEmpty>Нет кейсов</CommandEmpty>
              <CommandGroup>
                {data
                  .filter((c) => c.type === "Submitted")
                  .map((c) => (
                    <CommandItem
                      key={c.id}
                      value={c.name}
                      onSelect={() => {
                        setValue(value === c.id ? "" : c.id);
                        setIsOpen(false);
                      }}
                    >
                      <CheckIcon
                        className={cn(
                          "mr-2 size-4",
                          value === c.id ? "opacity-100" : "opacity-0",
                        )}
                      />
                      {c.name}
                    </CommandItem>
                  ))}
              </CommandGroup>
            </CommandList>
          </Command>
        </PopoverContent>
      </Popover>
      <Button
        variant="outline"
        onClick={() => createProject({ caseId: value })}
      >
        Создать
      </Button>
    </div>
  );
}
