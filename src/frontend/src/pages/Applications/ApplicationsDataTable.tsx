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
import { cn, getInitials } from "@/lib/utils";
import type { ColumnDef } from "@tanstack/react-table";
import { ArrowDownIcon, ArrowUpIcon, Trash2Icon } from "lucide-react";
import { useMemo } from "react";
import ReviewPopover from "./ReviewPopover";
import { Checkbox } from "@/components/ui/checkbox";
import { Dialog, DialogContent, DialogTrigger } from "@/components/ui/dialog";
import Application from "./Application";
import ApplicationReviewsPopover from "./ApplicationReviewsPopover";

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
        cell: ({ row }) => (
          <Dialog>
            <DialogTrigger asChild>
              <p>{row.original.name}</p>
            </DialogTrigger>
            <DialogContent>
              <Application case={row.original} />
            </DialogContent>
          </Dialog>
        ),
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
            className="flex h-full items-center"
            onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          >
            Дата создания
            {
              <ArrowUpIcon
                className={cn("ml-2 size-4", {
                  "rotate-180": column.getIsSorted() === "desc",
                  "opacity-0": column.getIsSorted() === false,
                })}
              />
            }
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
        header: ({ column }) => (
          <button
            className="flex h-full items-center"
            onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          >
            Лайки
            {
              <ArrowUpIcon
                className={cn("ml-2 size-4", {
                  "rotate-180": column.getIsSorted() === "desc",
                  "opacity-0": column.getIsSorted() === false,
                })}
              />
            }
          </button>
        ),
        cell: ({ row }) => (
          <div className="flex items-center">
            <ReviewPopover caseId={row.original.id} isDislike={false}>
              <Button variant="ghost" size="icon-sm">
                <ArrowUpIcon />
              </Button>
            </ReviewPopover>
            <ApplicationReviewsPopover
              likes={row.original.likes - row.original.dislikes}
              id={row.original.id}
            />
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
        header: ({ column }) => (
          <button
            className="flex h-full items-center"
            onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          >
            Кейс
            {
              <ArrowUpIcon
                className={cn("ml-2 size-4", {
                  "rotate-180": column.getIsSorted() === "desc",
                  "opacity-0": column.getIsSorted() === false,
                })}
              />
            }
          </button>
        ),
        cell: ({ row }) => (
          <Checkbox
            disabled={user?.id !== row.original.author.id}
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
          if (user?.id !== row.original.author.id) return null;
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
    [changeCaseType, deleteCase, user?.id],
  );

  return (
    <div className="mt-4">
      {data !== undefined && <DataTable columns={columns} data={data} />}
    </div>
  );
}
