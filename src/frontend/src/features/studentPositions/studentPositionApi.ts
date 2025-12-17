import { baseQuery } from "@/api/baseQuery";
import type { GetResponse } from "@/types";
import { createApi } from "@reduxjs/toolkit/query/react";
import type { IStudentPosition } from "./types";

export const studentPositionApi = createApi({
  baseQuery,
  reducerPath: "studentPositionApi",
  tagTypes: ["Position"],
  endpoints: (builder) => ({
    getPositions: builder.query<
      GetResponse<IStudentPosition>,
      { query?: number; limit: number; offset: number }
    >({
      query: (params) => ({
        url: "/student-positions",
        method: "GET",
        credentials: "include",
        params,
      }),
      providesTags: (res) =>
        res
          ? [
              ...res.data.map(({ id }) => ({ type: "Position" as const, id })),
              { type: "Position", id: "LIST" },
            ]
          : [{ type: "Position", id: "LIST" }],
    }),
    createPosition: builder.mutation<IStudentPosition, { name: string }>({
      query: ({ name }) => ({
        url: "/student-positions",
        method: "POST",
        body: { name },
        credentials: "include",
      }),
      invalidatesTags: [{ type: "Position", id: "LIST" }],
    }),
    patchPosition: builder.mutation<IStudentPosition, IStudentPosition>({
      query: ({ id, name }) => ({
        url: "/student-positions/" + id,
        body: { name },
        credentials: "include",
        method: "PUT",
      }),
      invalidatesTags: (_r, _e, { id }) => [{ type: "Position", id }],
    }),
    deletePosition: builder.mutation<IStudentPosition, { id: string }>({
      query: ({ id }) => ({
        url: "/student-positions/" + id,
        method: "DELETE",
        credentials: "include",
      }),
      invalidatesTags: (_r, _e, { id }) => [{ type: "Position", id }],
    }),
  }),
});

export const {
  useGetPositionsQuery,
  useCreatePositionMutation,
  usePatchPositionMutation,
  useDeletePositionMutation,
} = studentPositionApi;
