import CreateNewProject from "./CreateNewProject";
import ProjectsDataTable from "./ProjectsDataTable";

export default function Projects() {
  return (
    <div className="mx-auto max-w-screen-xl px-8 py-4">
      <h1 className="scroll-m-20 text-4xl font-semibold tracking-tight text-balance">
        Проекты
      </h1>
      <div>
        <CreateNewProject className="mt-4" />
        <ProjectsDataTable />
      </div>
    </div>
  );
}
