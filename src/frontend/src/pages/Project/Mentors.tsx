import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
  CommandList,
} from "@/components/ui/command";
import { Label } from "@/components/ui/label";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { usePutMentorsMutation } from "@/features/projects/projectsApi";
import type { IProject } from "@/features/projects/types";
import type { IUser } from "@/features/users/types";
import { useGetUsersQuery } from "@/features/users/usersApi";
import useDebounce from "@/hooks/useDebounce";
import { useCallback, useMemo, useState } from "react";

export function Mentors({ project }: { project: IProject }) {
  const [isOpen, setIsOpen] = useState(false);
  const [search, setSearch] = useState("");
  const debouncedSearch = useDebounce(search, 300);
  const { data } = useGetUsersQuery({
    limit: 5,
    offset: 0,
    query: debouncedSearch,
  });

  const handleSearch = useCallback((e: string) => {
    setSearch(e);
  }, []);

  const [putMentors] = usePutMentorsMutation();

  const currentMentors = useMemo(
    () => project.mentors || [],
    [project.mentors],
  );

  const addMentor = useCallback(
    async (user: IUser) => {
      if (currentMentors.some((m) => m.id === user.id)) {
        setIsOpen(false);
        return;
      }
      const newMentorsIds = [...currentMentors.map((m) => m.id), user.id];
      await putMentors({ id: project.id, mentors: newMentorsIds });
      setIsOpen(false);
    },
    [currentMentors, project.id, putMentors],
  );

  const removeMentor = useCallback(
    async (userId: string) => {
      const newMentorIds = currentMentors
        .filter((m) => m.id !== userId)
        .map((m) => m.id);
      await putMentors({ id: project.id, mentors: newMentorIds });
    },
    [currentMentors, project.id, putMentors],
  );

  return (
    <div>
      <Label>Менторы</Label>
      <div>
        <div className="flex flex-col">
          {currentMentors.map((m) => (
            <button
              key={m.id}
              className="w-fit pb-0.5 decoration-1 underline-offset-4 hover:line-through"
              onClick={() => removeMentor(m.id)}
            >
              {m.middleName} {m.firstName} {m.lastName}
            </button>
          ))}
        </div>
        <Popover
          open={isOpen}
          onOpenChange={(open) => {
            if (!open) setSearch("");
            setIsOpen(open);
          }}
        >
          <PopoverTrigger asChild>
            <button className="text-blue-600 underline decoration-dashed decoration-1 underline-offset-4 hover:text-blue-800">
              добавить
            </button>
          </PopoverTrigger>
          <PopoverContent>
            <Command shouldFilter={false}>
              <CommandInput
                placeholder="Поиск ментора..."
                className="h-9"
                onValueChange={handleSearch}
              />
              <CommandList>
                <CommandEmpty>Пользователи не найдены</CommandEmpty>
                <CommandGroup>
                  {data &&
                    data.data.map((m) => (
                      <CommandItem
                        key={m.id}
                        value={m.id}
                        onSelect={() => {
                          addMentor(m);
                          setIsOpen(false);
                        }}
                      >
                        {m.middleName} {m.firstName} {m.lastName}
                      </CommandItem>
                    ))}
                </CommandGroup>
              </CommandList>
            </Command>
          </PopoverContent>
        </Popover>
      </div>
    </div>
  );
}
