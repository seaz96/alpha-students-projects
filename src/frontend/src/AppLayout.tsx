import { Outlet } from "react-router";
import Header from "./components/header";

export default function AppLayout() {
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
