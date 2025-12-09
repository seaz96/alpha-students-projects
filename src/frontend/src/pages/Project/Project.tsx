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
import { useCallback, useEffect, useState } from "react";
import type { ProjectStatus } from "@/features/projects/types";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import SaveButton from "./SaveButton";
import { toast } from "sonner";

export default function Project() {
  const { id } = useParams();

  const { data, isLoading, isError, error, isSuccess } = useGetProjectQuery(
    id!,
  );

  useTitle(data?.name);

  const [description, setDescription] = useState("");
  const [status, setStatus] = useState<ProjectStatus>("Unknown");

  useEffect(() => {
    if (isSuccess && data !== undefined) {
      setDescription(data.description);
      setStatus(data.status);
    }
  }, [isSuccess, data]);

  const [patchProject] = usePatchProjectMutation();
  const saveProject = useCallback(async () => {
    if (!data) return;
    await patchProject({
      projectId: id!,
      name: data?.name,
      description,
      status: status,
    });
    toast.success(`Проект сохранён`);
  }, [data, id, patchProject, description, status]);

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
        <div className="flex gap-8">
          <div>
            <Label className="mb-2">Дата создания</Label>
            <p>{formattedDate}</p>
          </div>
          <div>
            <Label className="mb-2">Автор</Label>
            <p>{getInitials(data.author)}</p>
          </div>
          <div>
            <Label className="mb-2">Статус</Label>
            <Select
              value={status}
              onValueChange={(e) => setStatus(e as ProjectStatus)}
            >
              <SelectTrigger>
                <SelectValue placeholder="Статус проекта" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="Active">Активный</SelectItem>
                <SelectItem value="Archived">Архив</SelectItem>
              </SelectContent>
            </Select>
          </div>
        </div>
        <Label className="mt-4 mb-2">Описание</Label>
        <Textarea
          value={description}
          onChange={(e) => setDescription(e.currentTarget.value)}
        />
        <SaveButton onClick={saveProject} className="mt-2" />
        <TeamsDataTable projectId={data.id} />
      </div>
    </div>
  );
}
