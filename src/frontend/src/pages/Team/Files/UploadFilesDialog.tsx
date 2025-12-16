import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogTrigger,
    DialogFooter,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { PlusIcon, UploadIcon } from "lucide-react";
import { useState } from "react";
import {
    useGetUploadUrlMutation,
    useConfirmUploadMutation,
} from "@/features/files/filesApi";

export function UploadFilesDialog({ teamId }: { teamId: string }) {
    const [open, setOpen] = useState(false);
    const [file, setFile] = useState<File | null>(null);
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);

    const [getUploadUrl] = useGetUploadUrlMutation();
    const [confirmUpload] = useConfirmUploadMutation();

    async function onUpload() {
        if (!file) return;

        setLoading(true);
        setError(null);

        try {
            const { link } = await getUploadUrl({
                teamId,
                name: file.name,
            }).unwrap();
            
            const uploadRes = await fetch(link, {
                method: "PUT",
                body: file,
                headers: {
                    "Content-Type": file.type || "application/octet-stream",
                },
            });

            if (!uploadRes.ok) {
                const text = await uploadRes.text();
                console.error(text);
                throw new Error("Ошибка загрузки файла в хранилище");
            }

            await confirmUpload({
                teamId,
                name: file.name,
            }).unwrap();

            setOpen(false);
            setFile(null);
        } catch (e) {
            setError(
                e instanceof Error ? e.message : "Не удалось загрузить файл",
            );
        } finally {
            setLoading(false);
        }
    }

    return (
        <Dialog open={open} onOpenChange={setOpen}>
            <DialogTrigger asChild>
                <Button size="icon-sm" variant="outline">
                    <PlusIcon className="h-4 w-4" />
                </Button>
            </DialogTrigger>

            <DialogContent className="max-w-[420px]">
                <DialogHeader>
                    <DialogTitle>Загрузка файла</DialogTitle>
                </DialogHeader>

                <div className="space-y-4">
                    <label className="flex cursor-pointer flex-col items-center gap-2 rounded-md border border-dashed p-6 text-sm text-muted-foreground hover:bg-accent">
                        <UploadIcon className="h-5 w-5" />
                        <span>
              {file ? file.name : "Нажмите или перетащите файл"}
            </span>
                        <input
                            type="file"
                            className="hidden"
                            onChange={(e) => setFile(e.target.files?.[0] ?? null)}
                        />
                    </label>

                    {error && (
                        <p className="text-sm text-destructive">{error}</p>
                    )}
                </div>

                <DialogFooter>
                    <Button
                        onClick={onUpload}
                        disabled={!file || loading}
                    >
                        Загрузить
                    </Button>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    );
}
