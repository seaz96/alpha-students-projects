import { DataTable } from "@/components/ui/data-table";
import { useGetTeamsQuery } from "@/features/teams/teamsApi";
import type { ITeam } from "@/features/teams/types";
import type { ColumnDef } from "@tanstack/react-table";
import { useMemo } from "react";
import CreateTeamPopover from "./CreateTeamPopover";
import { Link } from "react-router";
import { Link2Icon, Link2OffIcon } from "lucide-react";

export default function TeamsDataTable({ projectId }: { projectId: string }) {
  const { data, isLoading } = useGetTeamsQuery({
    projectId,
    limit: 9999,
    offset: 0,
  });

  const columns = useMemo<ColumnDef<ITeam>[]>(
    () => [
      {
        accessorKey: "name",
        header: "Команды",
        cell: ({ row }) => (
          <Link to={`teams/${row.original.id}`} className="hover:underline">
            {row.original.name}
          </Link>
        ),
      },
      {
        accessorKey: "description",
        header: "Описание",
        cell: ({ row }) => (
          <p className="max-w-screen-sm truncate">{row.original.description}</p>
        ),
      },
      {
        accessorKey: "teamprojectLink",
        header: "Ссылка на проект",
        cell: ({ row }) => {
          if (!row.original.teamprojectLink)
            return <Link2OffIcon className="opacity-40" />;
          return (
            <Link
              to={row.original.teamprojectLink}
              className="flex items-center gap-1 text-indigo-600 hover:text-indigo-800"
            >
              <Link2Icon /> Ссылка
            </Link>
          );
        },
      },
    ],
    [],
  );

  if (isLoading || data === undefined) return <p>Загрузка...</p>;
  const { data: teams } = data;

  return (
    <div>
      <CreateTeamPopover projectId={projectId} />
      <DataTable columns={columns} data={teams} />
    </div>
  );
}
