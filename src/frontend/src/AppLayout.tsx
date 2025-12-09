import { Outlet } from "react-router";
import Header from "./components/header";
import { useCheckAuthQuery } from "./features/users/usersApi";
import { Toaster } from "./components/ui/sonner";

export default function AppLayout() {
  useCheckAuthQuery();

  return (
    <div className="flex min-h-screen flex-col">
      <Header />
      <main className="flex-1">
        <Outlet />
      </main>
      <footer />
      <Toaster position="top-right" />
    </div>
  );
}
