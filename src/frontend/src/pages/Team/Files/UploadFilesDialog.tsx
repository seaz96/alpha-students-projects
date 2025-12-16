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
import { useEffect, useState } from "react";
import {
    useGetUploadUrlMutation,
    useConfirmUploadMutation,
} from "@/features/files/filesApi";
import { Input } from "@/components/ui/input";

function getExtension(fileName: string) {
    const index = fileName.lastIndexOf(".");
    return index === -1 ? "" : fileName.slice(index);
}

export function UploadFilesDialog({
                                      teamId,
                                      currentPath,
                                  }: {
    teamId: string;
    currentPath: string[];
}) {
    const [open, setOpen] = useState(false);
    const [file, setFile] = useState<File | null>(null);
    const [fileName, setFileName] = useState("");
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);

    const [getUploadUrl] = useGetUploadUrlMutation();
    const [confirmUpload] = useConfirmUploadMutation();

    // Когда пользователь выбрал файл — подставляем имя без расширения
    useEffect(() => {
        if (!file) return;

        const ext = getExtension(file.name);
        setFileName(file.name.replace(ext, ""));
    }, [file]);

    async function onUpload() {
        if (!file) return;

        const ext = getExtension(file.name);
        const safeName = fileName.trim() || "file";
        const finalName = `${safeName}${ext}`;

        const fullName =
            currentPath.length > 0
                ? `${currentPath.join("/")}/${finalName}`
                : finalName;

        setLoading(true);
        setError(null);

        try {
            const { link } = await getUploadUrl({
                teamId,
                name: fullName,
            }).unwrap();

            const uploadRes = await fetch(link, {
                method: "PUT",
                body: file,
                headers: {
                    "Content-Type": file.type || "application/octet-stream",
                },
            });

            if (!uploadRes.ok) {
                throw new Error("Ошибка загрузки файла в хранилище");
            }

            await confirmUpload({
                teamId,
                name: fullName,
            }).unwrap();

            setOpen(false);
            setFile(null);
            setFileName("");
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
                    {/* File picker */}
                    <label className="flex cursor-pointer flex-col items-center gap-2 rounded-md border border-dashed p-6 text-sm text-muted-foreground hover:bg-accent">
                        <UploadIcon className="h-5 w-5" />
                        <span>
              {file ? file.name : "Нажмите или перетащите файл"}
            </span>
                        <input
                            type="file"
                            className="hidden"
                            onChange={(e) =>
                                setFile(e.target.files?.[0] ?? null)
                            }
                        />
                    </label>

                    {/* Custom file name */}
                    {file && (
                        <div className="space-y-1">
                            <label className="text-xs text-muted-foreground">
                                Имя файла
                            </label>
                            <div className="flex items-center gap-1">
                                <Input
                                    value={fileName}
                                    onChange={(e) => setFileName(e.target.value)}
                                    placeholder="Введите имя файла"
                                />
                                <span className="text-sm text-muted-foreground">
                  {getExtension(file.name)}
                </span>
                            </div>
                        </div>
                    )}

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
