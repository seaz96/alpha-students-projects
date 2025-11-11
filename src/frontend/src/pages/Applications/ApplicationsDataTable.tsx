import { Button } from "@/components/ui/button";
import { DataTable } from "@/components/ui/data-table";
import {
  useDeleteCaseMutation,
  useGetCasesQuery,
} from "@/features/cases/casesApi";
import type { ICase } from "@/features/cases/types";
import { getInitials } from "@/lib/utils";
import type { ColumnDef } from "@tanstack/react-table";
import {
  ArrowDownIcon,
  ArrowUpDownIcon,
  ArrowUpIcon,
  Trash2Icon,
} from "lucide-react";
import { useMemo } from "react";

export default function ApplicationsDataTable() {
  const { data } = useGetCasesQuery({
    limit: 10,
    offset: 0,
  });

  const [deleteCase] = useDeleteCaseMutation();

  const columns = useMemo<ColumnDef<ICase>[]>(
    () => [
      {
        accessorKey: "name",
        header: "Название",
        cell: ({ row }) => <p>{row.original.name}</p>,
        enableSorting: true,
        enableHiding: false,
      },
      {
        accessorKey: "author",
        header: "Автор",
        cell: ({ row }) => <p>{getInitials(row.original.author)}</p>,
        enableSorting: true,
        enableHiding: false,
      },
      {
        accessorKey: "createdAt",
        header: ({ column }) => (
          <button
            className="flex items-center gap-2"
            onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          >
            Дата создания <ArrowUpDownIcon className="ml-2 size-4" />
          </button>
        ),
        cell: ({ row }) => (
          <p>{new Date(row.original.createdAt).toLocaleDateString("ru-RU")}</p>
        ),
        enableSorting: true,
        enableHiding: false,
      },
      {
        accessorKey: "likes",
        header: "Лайки",
        cell: ({ row }) => (
          <div className="flex items-center gap-1">
            <Button variant="ghost" size="icon-sm">
              <ArrowUpIcon />
            </Button>
            <p>{row.original.likes - row.original.dislikes}</p>
            <Button variant="ghost" size="icon-sm">
              <ArrowDownIcon />
            </Button>
          </div>
        ),
      },
      {
        accessorKey: "delete",
        header: "",
        cell: ({ row }) => (
          <Button
            size="icon"
            variant="ghost"
            onClick={() => deleteCase(row.original.id)}
          >
            <Trash2Icon />
          </Button>
        ),
      },
    ],
    [deleteCase],
  );

  return (
    <div className="mt-4">
      {data !== undefined && <DataTable columns={columns} data={data} />}
    </div>
  );
}
