import { baseQuery } from "@/api/baseQuery";
import { createApi } from "@reduxjs/toolkit/query/react";
import type {
  ICreateStudentArgs,
  ICreateTeamArgs,
  IPatchStudentArgs,
  IPatchTeamArgs,
  IPatchTeamResult,
  ITeam,
  ITeamResult,
} from "./types";
import { type GetResponse } from "@/types";

export const teamsApi = createApi({
  reducerPath: "teamsApi",
  baseQuery,
  tagTypes: ["Team", "Result"],
  endpoints: (builder) => ({
    getTeams: builder.query<
      GetResponse<ITeam>,
      { projectId: string; limit: number; offset: number }
    >({
      query: ({ projectId, limit, offset }) => ({
        url: "/teams",
        method: "GET",
        params: { limit, offset, projectId },
        credentials: "include",
      }),
      providesTags: (result) =>
        result
          ? [
              ...result.data.map(({ id }) => ({ type: "Team" as const, id })),
              { type: "Team", id: "LIST" },
            ]
          : [{ type: "Team", id: "LIST" }],
    }),
    getTeam: builder.query<ITeam, string>({
      query: (id) => ({
        url: `/teams/${id}`,
        method: "GET",
        credentials: "include",
      }),
      providesTags: (_result, _error, id) => [{ type: "Team", id }],
    }),
    createTeam: builder.mutation<ITeam, ICreateTeamArgs>({
      query: (team) => ({
        url: "/teams",
        method: "POST",
        body: team,
        credentials: "include",
      }),
      invalidatesTags: [{ type: "Team", id: "LIST" }],
    }),
    patchTeam: builder.mutation<ITeam, IPatchTeamArgs>({
      query: ({ teamId, ...body }) => ({
        url: `/teams/${teamId}`,
        method: "PATCH",
        credentials: "include",
        body,
      }),
      invalidatesTags: (_res, _err, { teamId }) => [
        { type: "Team", id: teamId },
      ],
    }),
    deleteTeam: builder.mutation<ITeam, { teamId: string }>({
      query: ({ teamId }) => ({
        url: "/teams/" + teamId,
        method: "DELETE",
        credentials: "include",
      }),
      invalidatesTags: (_r, _e, { teamId }) => [
        { type: "Team", id: teamId },
        { type: "Team", id: "LIST" },
      ],
    }),
    addStudent: builder.mutation<
      ITeam,
      ICreateStudentArgs & { teamId: string }
    >({
      query: ({ teamId, ...student }) => ({
        url: `/teams/${teamId}/students`,
        method: "POST",
        body: { students: [student] },
        credentials: "include",
      }),
      invalidatesTags: (_res, _err, { teamId }) => [
        { type: "Team", id: teamId },
      ],
    }),
    patchStudent: builder.mutation<ITeam, IPatchStudentArgs>({
      query: ({ teamId, ...student }) => ({
        url: `/teams/${teamId}/students`,
        method: "PATCH",
        body: { students: [student] },
        credentials: "include",
      }),
      invalidatesTags: (_res, _err, { teamId }) => [
        { type: "Team", id: teamId },
      ],
    }),
    deleteStudent: builder.mutation<ITeam, { teamId: string; id: string }>({
      query: ({ teamId, id }) => ({
        url: `/teams/${teamId}/students`,
        method: "DELETE",
        body: { students: [id] },
        credentials: "include",
      }),
      invalidatesTags: (_res, _err, { teamId }) => [
        { type: "Team", id: teamId },
      ],
    }),
    getTeamResult: builder.query<ITeamResult, { teamId: string }>({
      query: ({ teamId }) => ({
        url: "/teams/" + teamId + "/result",
        credentials: "include",
      }),
      providesTags: (_res, _err, { teamId }) => [
        { type: "Result", id: teamId },
      ],
    }),

    patchTeamResult: builder.mutation<ITeamResult, IPatchTeamResult>({
      query: ({ teamId, ...body }) => ({
        url: "/teams/" + teamId + "/result",
        method: "PUT",
        credentials: "include",
        body,
      }),
      invalidatesTags: (_res, _err, { teamId }) => [
        { type: "Result", id: teamId },
      ],
    }),
  }),
});

export const {
  useGetTeamsQuery,
  useGetTeamQuery,
  useCreateTeamMutation,
  usePatchTeamMutation,
  useDeleteTeamMutation,
  useAddStudentMutation,
  usePatchStudentMutation,
  useDeleteStudentMutation,
  useGetTeamResultQuery,
  usePatchTeamResultMutation,
} = teamsApi;
