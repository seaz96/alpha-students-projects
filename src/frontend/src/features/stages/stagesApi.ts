import { baseQuery } from "@/api/baseQuery";
import { createApi } from "@reduxjs/toolkit/query/react";
import { type GetResponse } from "@/types";
import type { PatchStageArgs, IStage, PostStageArgs } from "./types";

export const stagesApi = createApi({
  baseQuery,
  reducerPath: "stagesApi",
  tagTypes: ["Stage"],
  endpoints: (builder) => ({
    getStages: builder.query<
      GetResponse<IStage>,
      { teamId: string; limit: number; offset: number }
    >({
      query: (params) => ({
        url: "/stages",
        method: "GET",
        credentials: "include",
        params,
      }),
      providesTags: (res) =>
        res
          ? [
              ...res.data.map(({ id }) => ({ type: "Stage" as const, id })),
              { type: "Stage", id: "LIST" },
            ]
          : [{ type: "Stage", id: "LIST" }],
    }),
    createStage: builder.mutation<IStage, PostStageArgs>({
      query: (body) => ({
        url: "/stages",
        method: "POST",
        credentials: "include",
        body,
      }),
      invalidatesTags: [{ type: "Stage", id: "LIST" }],
    }),
    patchStage: builder.mutation<IStage, PatchStageArgs>({
      query: ({ stageId, ...body }) => ({
        url: "/stages/" + stageId,
        method: "PATCH",
        credentials: "include",
        body,
      }),
      invalidatesTags: (_res, _err, { stageId }) => [
        { type: "Stage", id: stageId },
      ],
    }),
    getStage: builder.query<IStage, { id: string }>({
      query: ({ id }) => ({
        url: "/stages/" + id,
        method: "GET",
        credentials: "include",
      }),
      providesTags: (_res, _err, { id }) => [{ type: "Stage", id }],
    }),
    deleteStage: builder.mutation<IStage, { id: string }>({
      query: ({ id }) => ({
        url: "/stages/" + id,
        method: "DELETE",
        credentials: "include",
      }),
      invalidatesTags: (_res, _err, { id }) => [{ type: "Stage", id }],
    }),
  }),
});

export const {
  useGetStageQuery,
  useGetStagesQuery,
  useCreateStageMutation,
  usePatchStageMutation,
  useDeleteStageMutation,
} = stagesApi;
