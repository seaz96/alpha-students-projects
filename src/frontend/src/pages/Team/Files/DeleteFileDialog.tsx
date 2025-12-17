import {
    Dialog,
    DialogContent,
    DialogFooter,
    DialogHeader,
    DialogTitle,
    DialogDescription,
    DialogTrigger,
    DialogClose,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { TrashIcon } from "lucide-react";
import { useState } from "react";
import {useDeleteFileMutation} from "@/features/files/filesApi.ts";

export function DeleteFileDialog({
                                     fileId,
                                     fileName,
                                 }: {
    fileId: string;
    fileName: string;
}) {
    const [open, setOpen] = useState(false);
    const [deleteFile, { isLoading }] = useDeleteFileMutation();

    const onDelete = async () => {
        await deleteFile({ id: fileId });
        setOpen(false);
    };

    return (
        <Dialog open={open} onOpenChange={setOpen}>
            <DialogTrigger asChild>
                <Button size="icon-sm" variant="ghost">
                    <TrashIcon className="h-4 w-4 text-destructive" />
                </Button>
            </DialogTrigger>

            <DialogContent className="max-w-[400px]">
                <DialogHeader>
                    <DialogTitle>Удалить файл?</DialogTitle>
                    <DialogDescription>
                        Вы точно хотите удалить файл{" "}
                        <span className="font-medium">{fileName}</span>?
                        <br />
                        Это действие невозможно отменить.
                    </DialogDescription>
                </DialogHeader>

                <DialogFooter>
                    <DialogClose asChild>
                        <Button variant="secondary" disabled={isLoading}>
                            Отмена
                        </Button>
                    </DialogClose>

                    <Button
                        variant="destructive"
                        onClick={onDelete}
                        disabled={isLoading}
                    >
                        Да, удалить
                    </Button>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    );
}
