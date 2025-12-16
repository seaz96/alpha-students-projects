import { Button } from "@/components/ui/button";
import { DataTable } from "@/components/ui/data-table";
import {
  Dialog,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Spinner } from "@/components/ui/spinner";
import {
  useCreatePositionMutation,
  useDeletePositionMutation,
  useGetPositionsQuery,
} from "@/features/studentPositions/studentPositionApi";
import type { IStudentPosition } from "@/features/studentPositions/types";
import type { ColumnDef } from "@tanstack/react-table";
import { Edit2Icon, TrashIcon } from "lucide-react";
import { useCallback, useMemo, useState } from "react";

export default function StudentPositions() {
  const [isOpen, setIsOpen] = useState(false);
  const [name, setName] = useState("");

  const [createPosition, { isLoading }] = useCreatePositionMutation();
  const { data: positions } = useGetPositionsQuery({ limit: 9999, offset: 0 });

  const [deletePosition] = useDeletePositionMutation();

  const handleSubmit = useCallback(async () => {
    await createPosition({ name });
    setIsOpen(false);
    setName("");
  }, [createPosition, name]);

  const columns = useMemo<ColumnDef<IStudentPosition>[]>(
    () => [
      {
        accessorKey: "name",
        header: "Название",
        cell: ({ row }) => <p>{row.original.name}</p>,
      },
      {
        accessorKey: "controls",
        header: "",
        cell: ({ row }) => (
          <div className="flex w-0 gap-1">
            <Button variant="outline" size="icon-sm">
              <Edit2Icon />
            </Button>
            <Button
              variant="outline"
              size="icon-sm"
              onClick={() => deletePosition({ id: row.original.id })}
            >
              <TrashIcon />
            </Button>
          </div>
        ),
      },
    ],
    [deletePosition],
  );

  return (
    <div className="flex flex-col gap-1">
      <Dialog open={isOpen} onOpenChange={setIsOpen}>
        <DialogTrigger asChild>
          <Button className="w-fit">Создать роль студента</Button>
        </DialogTrigger>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>Создать роль студента</DialogTitle>
          </DialogHeader>
          {!positions && <Spinner />}
          <Input
            value={name}
            onChange={(e) => setName(e.currentTarget.value)}
          />
          <DialogFooter>
            <Button
              variant="secondary"
              onClick={() => setIsOpen(false)}
              disabled={isLoading}
            >
              Отмена
            </Button>
            <Button onClick={handleSubmit}>Создать</Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>

      <DataTable columns={columns} data={positions?.data || []} />
    </div>
  );
}
