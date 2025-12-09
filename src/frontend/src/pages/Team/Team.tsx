import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { Spinner } from "@/components/ui/spinner";
import { Textarea } from "@/components/ui/textarea";
import { useGetProjectQuery } from "@/features/projects/projectsApi";
import { useGetTeamQuery } from "@/features/teams/teamsApi";
import useTitle from "@/hooks/useTitle";
import { SaveIcon } from "lucide-react";
import { Link, useParams } from "react-router";
import { toast } from "sonner";

export default function Team() {
  const { projectId, teamId } = useParams();

  const { data, isLoading, isError, error } = useGetTeamQuery(teamId!);
  const { data: projectData } = useGetProjectQuery(projectId!);

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
        <Label>Проект</Label>
        <Link to={`/projects/${projectId}`} className="hover:underline">
          {projectData?.name}
        </Link>
        <Label className="mt-4 mb-1">Описание</Label>
        <Textarea value={data.description} />
        <Button
          size="sm"
          variant="outline"
          className="w-fit"
          onClick={() => {
            toast.success(`Команда сохранена`);
          }}
        >
          <SaveIcon />
          Сохранить
        </Button>
      </div>
    </div>
  );
}
