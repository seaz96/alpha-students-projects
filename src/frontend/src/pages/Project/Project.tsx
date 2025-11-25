import { Label } from "@/components/ui/label";
import { Spinner } from "@/components/ui/spinner";
import { Textarea } from "@/components/ui/textarea";
import { useGetProjectQuery } from "@/features/projects/projectsApi";
import useTitle from "@/hooks/useTitle";
import { formatter, getInitials } from "@/lib/utils";
import { useParams } from "react-router";
import TeamsDataTable from "./TeamsDataTable";

export default function Project() {
  const { id } = useParams();

  const { data, isLoading, isError, error } = useGetProjectQuery(id!);

  useTitle(data?.name);

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
        <Textarea value={data.description} />
        <TeamsDataTable projectId={data.id} />
      </div>
    </div>
  );
}
