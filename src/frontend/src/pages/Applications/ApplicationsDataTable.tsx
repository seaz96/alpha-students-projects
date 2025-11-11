import { useAppSelector } from "@/app/hooks";
import { Button } from "@/components/ui/button";
import { DataTable } from "@/components/ui/data-table";
import {
  useChangeCaseTypeMutation,
  useDeleteCaseMutation,
  useGetCasesQuery,
} from "@/features/cases/casesApi";
import type { ICase } from "@/features/cases/types";
import { selectUser } from "@/features/users/usersSlice";
import { getInitials } from "@/lib/utils";
import type { ColumnDef } from "@tanstack/react-table";
import {
  ArrowDownIcon,
  ArrowUpDownIcon,
  ArrowUpIcon,
  Trash2Icon,
} from "lucide-react";
import { useMemo } from "react";
import ReviewPopover from "./ReviewPopover";
import { Checkbox } from "@/components/ui/checkbox";

export default function ApplicationsDataTable() {
  const user = useAppSelector(selectUser);

  const { data } = useGetCasesQuery({
    limit: 10,
    offset: 0,
  });

  const [deleteCase] = useDeleteCaseMutation();
  const [changeCaseType] = useChangeCaseTypeMutation();

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
            <ReviewPopover caseId={row.original.id} isDislike={false}>
              <Button variant="ghost" size="icon-sm">
                <ArrowUpIcon />
              </Button>
            </ReviewPopover>
            <p>{row.original.likes - row.original.dislikes}</p>
            <ReviewPopover caseId={row.original.id} isDislike={true}>
              <Button variant="ghost" size="icon-sm">
                <ArrowDownIcon />
              </Button>
            </ReviewPopover>
          </div>
        ),
      },
      {
        accessorKey: "type",
        header: "Кейс",
        cell: ({ row }) => (
          <Checkbox
            defaultChecked={row.original.type == "Submitted"}
            onCheckedChange={() => {
              changeCaseType({
                id: row.original.id,
                type: row.original.type != "Request" ? "Request" : "Submitted",
              });
            }}
          />
        ),
        enableSorting: true,
      },
      {
        accessorKey: "delete",
        header: "",
        cell: ({ row }) => {
          if (!user || user.id !== row.original.author.id) return null;
          return (
            <Button
              size="icon"
              variant="ghost"
              onClick={() => deleteCase(row.original.id)}
            >
              <Trash2Icon />
            </Button>
          );
        },
      },
    ],
    [deleteCase, user],
  );

  return (
    <div className="mt-4">
      {data !== undefined && <DataTable columns={columns} data={data} />}
    </div>
  );
}
