export type FileNode =
    | {
    type: "folder";
    name: string;
    children: FileNode[];
}
    | {
    type: "file";
    id: string;
    name: string;
    size: number;
};

export function buildFileTree(files: { id: string; name: string; size: number }[]): FileNode[] {
    const root: Record<string, any> = {};

    for (const file of files) {
        const parts = file.name.split("/");
        let current = root;

        parts.forEach((part, index) => {
            if (!current[part]) {
                current[part] =
                    index === parts.length - 1
                        ? { type: "file", ...file, name: part }
                        : { type: "folder", name: part, children: {} };
            }
            current = current[part].children;
        });
    }

    const toArray = (node: Record<string, any>): FileNode[] =>
        Object.values(node).map((item: any) =>
            item.type === "folder"
                ? {
                    type: "folder",
                    name: item.name,
                    children: toArray(item.children),
                }
                : item,
        );

    return toArray(root);
}
