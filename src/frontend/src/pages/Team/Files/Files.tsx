import { FileTree } from "./FileTree";
import { Spinner } from "@/components/ui/spinner";
import { buildFileTree } from "@/features/files/utils/buildFileTree";
import { useGetFilesQuery } from "@/features/files/filesApi";
import { UploadFilesDialog } from "@/pages/Team/Files/UploadFilesDialog";

export default function Files({ teamId }: { teamId: string }) {
    const { data, isLoading, isError } = useGetFilesQuery({ teamId });

    if (isLoading) return <Spinner />;
    if (isError || !data) return <p>Ошибка загрузки файлов</p>;

    const header = (
        <div className="mb-3 flex items-center justify-between">
            <h3 className="text-lg font-medium">Файлы</h3>
            <UploadFilesDialog teamId={teamId} />
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

    const tree = buildFileTree(data);

    return (
        <div className="mt-6 rounded-md border p-4">
            {header}
            <FileTree nodes={tree} />
        </div>
    );
}
