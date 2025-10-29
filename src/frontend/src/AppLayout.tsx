import { Outlet } from "react-router";
import Header from "./components/header";
import { useCheckAuthQuery } from "./features/auth/auth.api";

export default function AppLayout() {
  useCheckAuthQuery();

  return (
    <div className="flex min-h-screen flex-col">
      <Header />
      <main className="flex-1">
        <Outlet />
      </main>
      <footer />
    </div>
  );
}
