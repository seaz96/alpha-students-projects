import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { useDeleteTeamMutation } from "@/features/teams/teamsApi";
import type { ITeam } from "@/features/teams/types";
import { TrashIcon } from "lucide-react";
import { useState } from "react";

export default function TeamsRowActions({ team }: { team: ITeam }) {
  const [deleteTeam] = useDeleteTeamMutation();
  const [isOpen, setIsOpen] = useState(false);

  return (
    <Dialog open={isOpen} onOpenChange={setIsOpen}>
      <DialogTrigger asChild>
        <Button size="icon-sm" variant="outline">
          <TrashIcon />
        </Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Подтвердите действие</DialogTitle>
          <DialogDescription />
        </DialogHeader>
        <p>
          Удалить команду "<span className="font-medium">{team.name}</span>"?
        </p>
        <DialogFooter>
          <Button
            type="button"
            variant="secondary"
            onClick={() => setIsOpen(false)}
          >
            Отмена
          </Button>
          <Button
            type="button"
            variant="destructive"
            onClick={() => {
              setIsOpen(false);
              deleteTeam({ teamId: team.id });
            }}
          >
            Удалить
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}
