import { baseQuery } from "@/api/baseQuery";
import { createApi } from "@reduxjs/toolkit/query/react";
import type { IFile } from "./types";

export const filesApi = createApi({
    reducerPath: "filesApi",
    baseQuery,
    tagTypes: ["File"],
    endpoints: (builder) => ({
        getFiles: builder.query<IFile[], { teamId: string }>({
            query: ({ teamId }) => ({
                url: "/files",
                method: "GET",
                credentials: "include",
                params: { teamId },
            }),
            providesTags: (res) =>
                res
                    ? [
                        ...res.map(({ id }) => ({ type: "File" as const, id })),
                        { type: "File", id: "LIST" },
                    ]
                    : [{ type: "File", id: "LIST" }],
        }),

        getUploadUrl: builder.mutation<
            { link: string },
            { teamId: string; name: string }
        >({
            query: ({ teamId, name }) => ({
                url: `/files/${teamId}/upload-url`,
                method: "GET",
                credentials: "include",
                params: {
                    name
                }
            }),
        }),

        confirmUpload: builder.mutation<
            IFile,
            { teamId: string; name: string }
        >({
            query: ({ teamId, name }) => ({
                url: `/files/${teamId}`,
                method: "POST",
                credentials: "include",
                params: {
                    name
                }
            }),
            invalidatesTags: [{ type: "File", id: "LIST" }],
        }),

        deleteFile: builder.mutation<void, { id: string }>({
            query: ({ id }) => ({
                url: `/files/${id}`,
                method: "DELETE",
                credentials: "include",
            }),
            invalidatesTags: (_res, _err, { id }) => [
                { type: "File", id },
                { type: "File", id: "LIST" },
            ],
        }),

        getDownloadUrl: builder.mutation<
            { link: string },
            { teamId: string; name: string }
        >({
            query: ({ teamId, name }) => ({
                url: `/files/${teamId}/content-url`,
                method: "GET",
                credentials: "include",
                params: {
                    name
                }
            }),
        }),
    }),
});

export const {
    useGetFilesQuery,
    useDeleteFileMutation,
    useGetUploadUrlMutation,
    useConfirmUploadMutation,
    useGetDownloadUrlMutation,
} = filesApi;
