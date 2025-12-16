import { FileTree } from "./FileTree";
import { Spinner } from "@/components/ui/spinner";
import {useGetFilesQuery} from "@/features/files/filesApi.ts";
import {buildFileTree} from "@/features/files/utils/buildFileTree.ts";

export default function Files({ teamId }: { teamId: string }) {
    const { data, isLoading, isError } = useGetFilesQuery({ teamId });

    if (isLoading) return <Spinner />;
    if (isError || !data) return <p>Ошибка загрузки файлов</p>;

    const tree = buildFileTree(data);

    return (
        <div className="mt-6 rounded-md border p-4">
            <h3 className="mb-3 text-lg font-medium">Файлы</h3>
            <FileTree nodes={tree} />
        </div>
    );
}
