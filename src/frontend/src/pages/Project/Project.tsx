import { Label } from "@/components/ui/label";
import { Spinner } from "@/components/ui/spinner";
import { Textarea } from "@/components/ui/textarea";
import {
  useGetProjectQuery,
  usePatchProjectMutation,
} from "@/features/projects/projectsApi";
import useTitle from "@/hooks/useTitle";
import { formatter, getInitials } from "@/lib/utils";
import { useParams } from "react-router";
import TeamsDataTable from "./TeamsDataTable";
import { Button } from "@/components/ui/button";
import { SaveIcon } from "lucide-react";
import { useCallback, useEffect, useState } from "react";

export default function Project() {
  const { id } = useParams();

  const { data, isLoading, isError, error, isSuccess } = useGetProjectQuery(
    id!,
  );

  useTitle(data?.name);

  const [description, setDescription] = useState("");
  useEffect(() => {
    if (isSuccess && data !== undefined) setDescription(data.description);
  }, [isSuccess, data]);

  const [patchProject] = usePatchProjectMutation();
  const saveProject = useCallback(async () => {
    if (!data) return;
    await patchProject({
      projectId: id!,
      name: data?.name,
      description,
      status: data.status,
    });
  }, [data, id, patchProject, description]);

  if (isLoading) {
    return <Spinner />;
  }

  if (isError) {
    return (
      <div>
        <p>Error loading project.</p>
        <p>{JSON.stringify(error)}</p>
      </div>
    );
  }

  if (data === undefined) return <p>Project not found.</p>;

  const date = new Date(data.createdAt);
  const formattedDate = formatter.format(date);

  return (
    <div className="mx-auto max-w-screen-xl px-8 py-4">
      <h1 className="scroll-m-20 text-4xl font-semibold tracking-tight text-balance">
        {data.name}
      </h1>
      <div className="mt-4">
        <div className="flex gap-6">
          <div>
            <Label>Дата создания</Label>
            <p>{formattedDate}</p>
          </div>
          <div>
            <Label>Автор</Label>
            <p>{getInitials(data.author)}</p>
          </div>
        </div>
        <Label className="mt-4 mb-2">Описание</Label>
        <Textarea
          value={description}
          onChange={(e) => setDescription(e.currentTarget.value)}
        />
        <Button
          size="sm"
          variant="outline"
          className="mt-2"
          onClick={saveProject}
        >
          <SaveIcon />
          Сохранить
        </Button>
        <TeamsDataTable projectId={data.id} />
      </div>
    </div>
  );
}
