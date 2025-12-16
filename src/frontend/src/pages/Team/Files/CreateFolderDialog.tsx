import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogTrigger,
    DialogFooter,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { FolderPlusIcon } from "lucide-react";
import { useState } from "react";
import { Input } from "@/components/ui/input";

interface CreateFolderDialogProps {
    currentPath: string[];
    onCreate: (path: string[]) => void;
}

export function CreateFolderDialog({
                                       currentPath,
                                       onCreate,
                                   }: CreateFolderDialogProps) {
    const [open, setOpen] = useState(false);
    const [name, setName] = useState("");

    function onSubmit() {
        const trimmed = name.trim();

        if (!trimmed) return;

        onCreate([...currentPath, trimmed]);
        setName("");
        setOpen(false);
    }

    return (
        <Dialog open={open} onOpenChange={setOpen}>
            <DialogTrigger asChild>
                <Button size="icon-sm" variant="outline">
                    <FolderPlusIcon className="h-4 w-4" />
                </Button>
            </DialogTrigger>

            <DialogContent className="max-w-[360px]">
                <DialogHeader>
                    <DialogTitle>Создать папку</DialogTitle>
                </DialogHeader>

                <Input
                    autoFocus
                    placeholder="Название папки"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    onKeyDown={(e) => {
                        if (e.key === "Enter") {
                            onSubmit();
                        }
                    }}
                />

                <DialogFooter>
                    <Button onClick={onSubmit} disabled={!name.trim()}>
                        Создать
                    </Button>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    );
}
