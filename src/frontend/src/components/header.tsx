import { Link } from "react-router";
import { Button } from "./ui/button";
import { ButtonGroup } from "./ui/button-group";
import { useAppSelector } from "@/app/hooks";
import {
  selectAuthStatus,
  selectIsAuthenticated,
} from "@/features/users/usersSlice";
import AlfaLogo from "@/assets/alfa-logo";
import { useLogoutMutation } from "@/features/users/usersApi";

export default function Header() {
  const isAuthenticated = useAppSelector(selectIsAuthenticated);
  const authStatus = useAppSelector(selectAuthStatus);
  const [logout] = useLogoutMutation();

  if (authStatus !== "succeeded") return null;

  return (
    <header className="bg-background sticky top-0">
      <div className="mx-auto flex w-full max-w-screen-xl items-center justify-between px-8 py-2">
        <Link
          to="/applications"
          className="flex items-center gap-4 font-medium underline-offset-4 hover:underline"
        >
          <AlfaLogo className="text-brand" />
          {import.meta.env.VITE_APP_NAME}
        </Link>

        <ButtonGroup>
          <Button variant="link" asChild>
            <Link to="/applications">Заявки</Link>
          </Button>
          <Button variant="link" asChild>
            <Link to="/projects">Проекты</Link>
          </Button>
          <Button variant="link" asChild>
            <Link to="/dashboard/student-roles">Дэшборд</Link>
          </Button>
        </ButtonGroup>

        <ButtonGroup>
          {isAuthenticated && (
            <>
              <Button variant="link" aria-label="Profile" asChild>
                <Link to="/profile">Профиль</Link>
              </Button>
              <Button
                variant="link"
                aria-label="Logout"
                onClick={() => logout()}
              >
                Выйти
              </Button>
            </>
          )}
          {!isAuthenticated && <Button aria-label="Login">Войти</Button>}
        </ButtonGroup>
      </div>
    </header>
  );
}
