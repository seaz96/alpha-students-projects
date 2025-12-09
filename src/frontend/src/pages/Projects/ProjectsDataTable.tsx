import { useAppSelector } from "@/app/hooks";
import { Button } from "@/components/ui/button";
import { DataTable } from "@/components/ui/data-table";
import { useGetProjectsQuery } from "@/features/projects/projectsApi";
import type { IProject } from "@/features/projects/types";
import { selectUser } from "@/features/users/usersSlice";
import { getInitials } from "@/lib/utils";
import type { ColumnDef } from "@tanstack/react-table";
import { Trash2Icon } from "lucide-react";
import { useMemo } from "react";
import { Link } from "react-router";

export default function ProjectsDataTable() {
  // TODO: Pagination
  const { data, isLoading } = useGetProjectsQuery({ limit: 9999, offset: 0 });
  const user = useAppSelector(selectUser);

  const columns = useMemo<ColumnDef<IProject>[]>(
    () => [
      {
        accessorKey: "name",
        header: "Название",
        cell: ({ row }) => (
          <Link to={row.original.id} className="py-2 hover:underline">
            <span className="block py-1">{row.original.name}</span>
          </Link>
        ),
        enableSorting: true,
        enableHiding: false,
      },
      {
        accessorKey: "author",
        header: "Автор",
        cell: ({ row }) => <p>{getInitials(row.original.author)}</p>,
        enableSorting: true,
      },
      {
        accessorKey: "createdAt",
        header: "Дата создания",
        cell: ({ row }) => (
          <p>{new Date(row.original.createdAt).toLocaleDateString("ru-RU")}</p>
        ),
        enableSorting: true,
      },
      {
        accessorKey: "status",
        header: "Статус",
        cell: ({ row }) => {
          return (
            <p>
              {row.original.status == "Active"
                ? "Активный"
                : row.original.status == "Archived"
                  ? "Архив"
                  : "Неизвестно"}
            </p>
          );
        },
      },
      {
        accessorKey: "control",
        header: "",
        cell: ({ row }) => {
          if (!(user?.id === row.original.author.id || user?.role === "Admin"))
            return;

          return (
            <>
              <Button
                size="icon"
                variant="ghost"
                onClick={() => null /* TODO: Удалить проект */}
              >
                <Trash2Icon />
              </Button>
            </>
          );
        },
      },
    ],
    [user?.id, user?.role],
  );

  if (isLoading || data === undefined) return <p>Загрузка...</p>;
  const { data: projects } = data;

  return (
    <div className="mt-4">
      <DataTable columns={columns} data={projects} />
    </div>
  );
}
