import { DataTable } from "@/components/ui/data-table";
import { useGetProjectsQuery } from "@/features/projects/projectsApi";
import type { IProject } from "@/features/projects/types";
import { getInitials } from "@/lib/utils";
import type { ColumnDef } from "@tanstack/react-table";
import { useMemo } from "react";
import { Link } from "react-router";

export default function ProjectsDataTable() {
  // TODO: Pagination
  const { data } = useGetProjectsQuery({ limit: 9999, offset: 0 });

  const columns = useMemo<ColumnDef<IProject>[]>(
    () => [
      {
        accessorKey: "name",
        header: "Название",
        cell: ({ row }) => (
          <Link to={row.original.id} className="py-2">
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
    ],
    [],
  );

  return (
    <div className="mt-4">
      {data !== undefined && <DataTable columns={columns} data={data} />}
    </div>
  );
}
