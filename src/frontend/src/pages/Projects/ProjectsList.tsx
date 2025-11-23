import { useGetProjectsQuery } from "@/features/projects/projectsApi";

export default function ProjectsList() {
  const { data, isLoading } = useGetProjectsQuery({ limit: 9999, offset: 0 });

  if (isLoading || data === undefined) return null;

  return (
    <div>
      <ul>
        {data.map((project) => (
          <li key={project.id}>{project.name}</li>
        ))}
      </ul>
    </div>
  );
}
