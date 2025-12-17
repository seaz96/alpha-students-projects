import CreateApplication from "./CreateApplication";
import ApplicationsDataTable from "./ApplicationsDataTable";

export default function Applications() {
  return (
    <div className="mx-auto max-w-screen-xl px-8 py-4">
      <h1 className="mb-4 scroll-m-20 text-4xl font-semibold tracking-tight text-balance">
        Заявки на проекты
      </h1>
      <CreateApplication />
      <ApplicationsDataTable />
    </div>
  );
}
