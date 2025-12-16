import { useState } from "react";
import { ChevronRight, FolderIcon, FileIcon } from "lucide-react";
import { cn } from "@/lib/utils";
import type { FileNode } from "@/features/files/utils/buildFileTree.ts";
import { DeleteFileDialog } from "@/pages/Team/Files/DeleteFileDialog.tsx";
import {DownloadFileButton} from "@/pages/Team/Files/DownloadFileButton.tsx";

export function FileTree({
                             nodes,
                             teamId,
                         }: {
    nodes: FileNode[];
    teamId: string;
}) {
    return (
        <div className="space-y-1">
            {nodes.map((node) => (
                <FileTreeNode
                    key={node.name}
                    node={node}
                    level={0}
                    teamId={teamId}
                />
            ))}
        </div>
    );
}

function FileTreeNode({
                          node,
                          level,
                          teamId,
                      }: {
    node: FileNode;
    level: number;
    teamId: string;
}) {

    const [open, setOpen] = useState(false);

    if (node.type === "file") {
        return (
            <div
                className="group flex items-center gap-2 text-sm"
                style={{ paddingLeft: level * 16 }}
            >
                <FileIcon className="h-4 w-4 text-muted-foreground" />
                <span>{node.name}</span>

                <span className="ml-auto text-xs text-muted-foreground">
          {node.size} B
        </span>
            <div className="flex items-center gap-1 opacity-0 group-hover:opacity-100 transition-opacity">
                <DownloadFileButton
                    teamId={teamId}
                    fileName={node.name}
                />
                <DeleteFileDialog
                    fileId={node.id}
                    fileName={node.name}
                />
            </div>
            </div>
        );
    }

    return (
        <div>
            <button
                type="button"
                onClick={() => setOpen((v) => !v)}
                className="flex items-center gap-1 text-sm font-medium"
                style={{ paddingLeft: level * 16 }}
            >
                <ChevronRight
                    className={cn("h-4 w-4 transition-transform", open && "rotate-90")}
                />
                <FolderIcon className="h-4 w-4" />
                {node.name}
            </button>

            {open && (
                <div className="mt-1 space-y-1">
                    {node.children.map((child) => (
                        <FileTreeNode
                            key={child.name}
                            node={child}
                            level={level + 1}
                            teamId={teamId}
                        />
                    ))}
                </div>
            )}
        </div>
    );
}

