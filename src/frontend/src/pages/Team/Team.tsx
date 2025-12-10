import { Label } from "@/components/ui/label";
import { Spinner } from "@/components/ui/spinner";
import { Textarea } from "@/components/ui/textarea";
import { useGetProjectQuery } from "@/features/projects/projectsApi";
import {
  useGetTeamQuery,
  usePatchTeamMutation,
} from "@/features/teams/teamsApi";
import useTitle from "@/hooks/useTitle";
import { Link, useParams } from "react-router";
import { toast } from "sonner";
import { StudentsDataTable } from "./StudentsDataTable";
import { useEffect, useRef, useState } from "react";
import useDebounce from "@/hooks/useDebounce";

export default function Team() {
  const { projectId, teamId } = useParams();

  const { data, isLoading, isError, error } = useGetTeamQuery(teamId!);
  const { data: projectData } = useGetProjectQuery(projectId!);
  const [patchTeam] = usePatchTeamMutation();

  const [description, setDescription] = useState<string | null>(null);
  const teamIdRef = useRef<string | null>(null);

  useEffect(() => {
    if (data && data.id !== teamIdRef.current) {
      setDescription(data.description);
      teamIdRef.current = data.id;
    }
  }, [data]);

  const debouncedDescription = useDebounce(description, 750);

  useEffect(() => {
    if (!data) return;
    if (debouncedDescription === null) return;
    if (data.description === debouncedDescription) return;

    patchTeam({
      teamId: data.id,
      name: data.name,
      description: debouncedDescription,
      teamprojectLink: data.teamprojectLink,
    })
      .unwrap()
      .then(() => toast.success(`Команда сохранена`))
      .catch(() => toast.error("Ошибка сохранения"));
  }, [data, debouncedDescription, patchTeam]);

  useTitle(data?.name);

  if (isLoading) return <Spinner />;

  if (isError)
    return (
      <div>
        <p>Error loading team.</p>
        <p>{JSON.stringify(error)}</p>
      </div>
    );

  if (data === undefined) return <p>Team not found.</p>;

  return (
    <div className="mx-auto max-w-screen-xl px-8 py-4">
      <h1 className="scroll-m-20 text-4xl font-semibold tracking-tight text-balance">
        {data.name}
      </h1>
      <div className="mt-4 flex flex-col gap-1">
        {projectData && (
          <>
            <Label>Проект</Label>
            <Link to={`/projects/${projectId}`} className="hover:underline">
              {projectData.name}
            </Link>
          </>
        )}
        <Label className="mt-4 mb-1">Описание</Label>
        <Textarea
          value={description ?? ""}
          onChange={(e) => setDescription(e.currentTarget.value)}
          disabled={description === null}
          placeholder={
            description === null ? "Загрузка..." : "Введите описание"
          }
        />
        <StudentsDataTable />
      </div>
    </div>
  );
}
