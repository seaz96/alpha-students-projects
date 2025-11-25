import { baseQuery } from "@/api/baseQuery";
import { createApi } from "@reduxjs/toolkit/query/react";
import type { ICreateTeamArgs, ITeam } from "./types";

export const teamsApi = createApi({
  reducerPath: "teamsApi",
  baseQuery,
  tagTypes: ["Teams"],
  endpoints: (builder) => ({
    getTeams: builder.query<
      ITeam[],
      { projectId: string; limit: number; offset: number }
    >({
      query: ({ projectId, limit, offset }) => ({
        url: "/teams",
        method: "GET",
        params: { limit, offset, projectId },
        credentials: "include",
      }),
      providesTags: ["Teams"],
    }),
    getTeam: builder.query<ITeam, string>({
      query: (id) => ({
        url: `/teams/${id}`,
        method: "GET",
        credentials: "include",
      }),
      providesTags: ["Teams"],
    }),
    createTeam: builder.mutation<ITeam, ICreateTeamArgs>({
      query: (team) => ({
        url: "/teams",
        method: "POST",
        body: team,
        credentials: "include",
      }),
      invalidatesTags: ["Teams"],
    }),
  }),
});

export const { useGetTeamsQuery, useGetTeamQuery, useCreateTeamMutation } =
  teamsApi;
