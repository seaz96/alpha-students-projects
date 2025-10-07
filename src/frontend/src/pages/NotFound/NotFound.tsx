import { Button } from "@/components/ui/button";
import { Link } from "react-router";

export default function NotFound() {
  return (
    <div className="flex flex-col items-center gap-2 p-8">
      <p className="text-accent-foreground text-4xl font-medium">404</p>
      <p>Страница не найдена</p>
      <Link to="/">
        <Button variant="outline">Вернуться на главную</Button>
      </Link>
    </div>
  );
}
