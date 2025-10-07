import { Link } from "react-router";
import { Button } from "./ui/button";
import { ButtonGroup } from "./ui/button-group";

export default function Header() {
  return (
    <header className="bg-background flex items-center justify-between p-2">
      <Link to="/">
        <Button variant="link">{import.meta.env.VITE_APP_NAME}</Button>
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
        <Button variant="ghost" aria-label="Profile">
          Иванов Иван
        </Button>
        <Button variant="ghost" aria-label="Logout">
          Выйти
        </Button>
      </ButtonGroup>
    </header>
  );
}
