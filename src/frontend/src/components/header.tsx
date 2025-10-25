import { Link } from "react-router";
import { Button } from "./ui/button";
import { ButtonGroup } from "./ui/button-group";
import { useAppSelector } from "@/app/hooks";
import { selectIsAuthenticated, selectUser } from "@/features/auth/authSlice";
import AlfaLogo from "@/assets/alfa-logo";

export default function Header() {
  const user = useAppSelector(selectUser);
  const isAuthenticated = useAppSelector(selectIsAuthenticated);

  return (
    <header className="bg-background sticky top-0">
      <div className="mx-auto flex w-full max-w-screen-xl items-center justify-between px-8 py-2">
        <Link
          to="/"
          className="flex items-center gap-4 font-medium underline-offset-4 hover:underline"
        >
          <AlfaLogo className="text-brand" />
          {import.meta.env.VITE_APP_NAME}
        </Link>

        <ButtonGroup>
          <Link to="/applications">
            <Button variant="link">Заявки</Button>
          </Link>
          <Link to="/projects">
            <Button variant="link">Проекты</Button>
          </Link>
          <Link to="/admin">
            <Button variant="link">Админ-Панель</Button>
          </Link>
        </ButtonGroup>

        <ButtonGroup>
          {isAuthenticated && (
            <>
              <Button variant="ghost" aria-label="Profile">
                {user?.name}
              </Button>
              <Button variant="ghost" aria-label="Logout">
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
