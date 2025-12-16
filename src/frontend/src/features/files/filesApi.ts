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
    }),
});

export const { useGetFilesQuery } = filesApi;
