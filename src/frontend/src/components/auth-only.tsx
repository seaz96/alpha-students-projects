import { useAppSelector } from "@/app/hooks";
import {
  selectIsAuthenticated,
  selectUser,
  selectAuthStatus,
} from "@/features/auth/authSlice";
import { Navigate, Outlet } from "react-router";
import { Button } from "./ui/button";
import { useLogoutMutation } from "@/features/auth/auth.api";

export default function AuthOnly() {
  const isAuthenticated = useAppSelector(selectIsAuthenticated);
  const user = useAppSelector(selectUser);
  const authStatus = useAppSelector(selectAuthStatus);

  const [logout] = useLogoutMutation();

  if (authStatus === "loading" || authStatus === "idle") {
    return <p>Loading...</p>;
  }

  if (!isAuthenticated) {
    return <Navigate to="/sign-in" />;
  }

  const hasRequiredRole = user?.roles.some((role) =>
    ["admin", "manager"].includes(role),
  );

  if (!hasRequiredRole) {
    return (
      <div className="mx-auto flex max-w-screen-sm flex-col gap-2 rounded border-2 p-8 text-pretty shadow-xl">
        <p>
          К сожалению, прав на вашем аккаунте недостаточно для пользования
          сайтом.
        </p>
        <p>Обратитесь к администратору для изменения вашей роли.</p>
        <p>Ваш id: {user?.id}</p>
        <Button className="mx-auto w-fit" onClick={() => logout()}>
          Выйти
        </Button>
      </div>
    );
  }

  return <Outlet />;
}
