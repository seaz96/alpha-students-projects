import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Spinner } from "@/components/ui/spinner";
import { buildFileTree } from "@/features/files/utils/buildFileTree";
import { useGetFilesQuery } from "@/features/files/filesApi";
import { UploadFilesDialog } from "@/pages/Team/Files/UploadFilesDialog";
import { FileTree } from "./FileTree";
import {CreateFolderDialog} from "@/pages/Team/Files/CreateFolderDialog.tsx";

export default function Files({ teamId }: { teamId: string }) {
    const { data, isLoading, isError } = useGetFilesQuery({ teamId });

    const [currentPath, setCurrentPath] = useState<string[]>([]);

    if (isLoading) return <Spinner />;
    if (isError || !data) {
        return <p>Ошибка загрузки файлов</p>;
    }

    const tree = buildFileTree(data);

    const header = (
        <div className="mb-3 flex items-center justify-between">
            <div className="flex items-center gap-2">
                <h3 className="text-lg font-medium">Файлы</h3>

                {currentPath.length > 0 && (
                    <Button
                        variant="ghost"
                        size="sm"
                        onClick={() => setCurrentPath((p) => p.slice(0, -1))}
                    >
                        ← Назад
                    </Button>
                )}
            </div>

            <div className="flex items-center gap-2">
                <CreateFolderDialog
                    currentPath={currentPath}
                    onCreate={setCurrentPath}
                />

                <UploadFilesDialog
                    teamId={teamId}
                    currentPath={currentPath}
                />
            </div>
        </div>
    );

    if (data.length === 0) {
        return (
            <div className="mt-6 rounded-md border p-4">
                {header}
                <p className="text-sm text-muted-foreground">
                    Файлы отсутствуют. Добавьте новые.
                </p>
            </div>
        );
    }

    return (
        <div className="mt-6 rounded-md border p-4">
            {header}

            <FileTree
                nodes={tree}
                teamId={teamId}
                currentPath={currentPath}
                onOpenFolder={setCurrentPath}
            />
        </div>
    );
}
