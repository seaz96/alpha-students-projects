import { Link, Outlet } from "react-router";

export default function Dashboard() {
  return (
    <div className="mx-auto flex max-w-screen-xl px-8 py-4">
      <aside className="flex w-fit flex-col py-2 pr-32">
        <h1 className="text-xl font-medium">Дэшборд</h1>
        <div className="h-2" />
        <Link to="/dashboard/users" className="py-1 hover:underline">
          Роли пользователей
        </Link>
        <Link to="/dashboard/student-roles" className="py-1 hover:underline">
          Роли студентов
        </Link>
      </aside>
      <main className="flex-1 pb-4">
        <Outlet />
      </main>
    </div>
  );
}
