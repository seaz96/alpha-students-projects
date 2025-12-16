import { useState } from "react";
import { ChevronRight, FolderIcon, FileIcon } from "lucide-react";
import { cn } from "@/lib/utils";
import type { FileNode } from "@/features/files/utils/buildFileTree.ts";
import { DeleteFileDialog } from "@/pages/Team/Files/DeleteFileDialog.tsx";
import {DownloadFileButton} from "@/pages/Team/Files/DownloadFileButton.tsx";

interface FileTreeProps {
    nodes: FileNode[];
    teamId: string;

    /** текущий путь (например ["docs", "images"]) */
    currentPath: string[];

    /** открыть папку */
    onOpenFolder: (path: string[]) => void;
}

/**
 * Возвращает содержимое папки по пути
 */
function getNodesByPath(
    nodes: FileNode[],
    path: string[],
): FileNode[] {
    let current = nodes;

    for (const part of path) {
        const folder = current.find(
            (n) => n.type === "folder" && n.name === part,
        );

        if (!folder || folder.type !== "folder") {
            return [];
        }

        current = folder.children;
    }

    return current;
}

export function FileTree({
                             nodes,
                             teamId,
                             currentPath,
                             onOpenFolder,
                         }: FileTreeProps) {
    const visibleNodes = getNodesByPath(nodes, currentPath);

    if (visibleNodes.length === 0) {
        return (
            <p className="text-sm text-muted-foreground">
                Папка пуста
            </p>
        );
    }

    return (
        <div className="space-y-1">
            {visibleNodes.map((node) => (
                <FileTreeNode
                    key={node.name}
                    node={node}
                    level={0}
                    teamId={teamId}
                    currentPath={currentPath}
                    onOpenFolder={onOpenFolder}
                />
            ))}
        </div>
    );
}

interface FileTreeNodeProps {
    node: FileNode;
    level: number;
    teamId: string;

    /** текущий путь до этой ноды */
    currentPath: string[];

    /** открыть папку (перейти внутрь) */
    onOpenFolder: (path: string[]) => void;
}

export function FileTreeNode({
                                 node,
                                 level,
                                 teamId,
                                 currentPath,
                                 onOpenFolder,
                             }: FileTreeNodeProps) {
    const [open, setOpen] = useState(false);

    /* ================= FILE ================= */

    if (node.type === "file") {
        const fullName =
            currentPath.length > 0
                ? `${currentPath.join("/")}/${node.name}`
                : node.name;

        return (
            <div
                className="group flex items-center gap-2 text-sm"
                style={{ paddingLeft: level * 16 }}
            >
                <FileIcon className="h-4 w-4 text-muted-foreground" />

                <span className="truncate">{node.name}</span>

                <span className="ml-auto text-xs text-muted-foreground">
          {node.size} B
        </span>

                <div className="flex items-center gap-1 opacity-0 transition-opacity group-hover:opacity-100">
                    <DownloadFileButton teamId={teamId} fileName={fullName} />
                    <DeleteFileDialog fileId={node.id} fileName={fullName} />
                </div>
            </div>
        );
    }

    /* ================= FOLDER ================= */

    const folderPath = [...currentPath, node.name];

    return (
        <div>
            <div
                className="flex items-center gap-1 text-sm font-medium cursor-pointer"
                style={{ paddingLeft: level * 16 }}
            >
                {/* раскрыть */}
                <button
                    type="button"
                    onClick={() => setOpen((v) => !v)}
                    className="flex items-center"
                >
                    <ChevronRight
                        className={cn(
                            "h-4 w-4 transition-transform",
                            open && "rotate-90",
                        )}
                    />
                </button>

                {/* перейти в папку */}
                <button
                    type="button"
                    onClick={() => onOpenFolder(folderPath)}
                    className="flex items-center gap-1 hover:underline"
                >
                    <FolderIcon className="h-4 w-4" />
                    <span>{node.name}</span>
                </button>
            </div>

            {open && (
                <div className="mt-1 space-y-1">
                    {node.children.map((child) => (
                        <FileTreeNode
                            key={`${node.name}/${child.name}`}
                            node={child}
                            level={level + 1}
                            teamId={teamId}
                            currentPath={folderPath}
                            onOpenFolder={onOpenFolder}
                        />
                    ))}
                </div>
            )}
        </div>
    );
}
