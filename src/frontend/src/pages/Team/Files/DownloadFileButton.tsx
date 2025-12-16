"use client";

import { Button } from "@/components/ui/button";
import { DownloadIcon } from "lucide-react";
import { useGetDownloadUrlMutation } from "@/features/files/filesApi";
import { useState } from "react";

export function DownloadFileButton({
                                       teamId,
                                       fileName,
                                   }: {
    teamId: string;
    fileName: string;
}) {
    const [getDownloadUrl] = useGetDownloadUrlMutation();
    const [loading, setLoading] = useState(false);

    const onDownload = async () => {
        setLoading(true);

        try {
            const { link } = await getDownloadUrl({
                teamId,
                name: fileName,
            }).unwrap();

            const res = await fetch(link);

            if (!res.ok) {
                throw new Error("Не удалось скачать файл");
            }

            const blob = await res.blob();
            const url = window.URL.createObjectURL(blob);

            const a = document.createElement("a");
            a.href = url;
            a.download = fileName;
            document.body.appendChild(a);
            a.click();
            a.remove();

            window.URL.revokeObjectURL(url);
        } catch (e) {
            console.error("Ошибка скачивания файла", e);
        } finally {
            setLoading(false);
        }
    };

    return (
        <Button
            size="icon-xs"
            variant="ghost"
            onClick={onDownload}
            disabled={loading}
        >
            <DownloadIcon className="h-4 w-4" />
        </Button>
    );
}
