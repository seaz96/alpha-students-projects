import { useState } from "react";
import { ChevronRight, FolderIcon, FileIcon } from "lucide-react";
import { cn } from "@/lib/utils";
import type { FileNode } from "@/features/files/utils/buildFileTree.ts";

export function FileTree({ nodes }: { nodes: FileNode[] }) {
    return (
        <div className="space-y-1">
            {nodes.map((node) => (
                <FileTreeNode key={node.name} node={node} level={0} />
            ))}
        </div>
    );
}

function FileTreeNode({
                          node,
                          level,
                      }: {
    node: FileNode;
    level: number;
}) {
    const [open, setOpen] = useState(false);

    if (node.type === "file") {
        return (
            <div
                className="flex items-center gap-2 text-sm"
                style={{ paddingLeft: level * 16 }}
            >
                <FileIcon className="h-4 w-4 text-muted-foreground" />
                {node.name}
                <span className="ml-auto text-xs text-muted-foreground">
          {node.size} B
        </span>
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
                        />
                    ))}
                </div>
            )}
        </div>
    );
}
