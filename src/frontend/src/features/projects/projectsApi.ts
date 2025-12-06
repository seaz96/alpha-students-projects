import { baseQuery } from "@/api/baseQuery";
import { createApi } from "@reduxjs/toolkit/query/react";
import type { GetProjectsArgs, IProject, PatchProjectArgs } from "./types";

export const projectsApi = createApi({
  reducerPath: "projcetsApi",
  baseQuery,
  tagTypes: ["Project"],
  endpoints: (builder) => ({
    getProjects: builder.query<IProject[], GetProjectsArgs>({
      query: ({ limit, offset }) => ({
        url: "/projects",
        method: "GET",
        credentials: "include",
        params: { limit, offset },
      }),
      providesTags: ["Project"],
    }),
    getProject: builder.query<IProject, string>({
      query: (id) => ({
        url: `/projects/${id}`,
        method: "GET",
        credentials: "include",
      }),
      providesTags: ["Project"],
    }),
    createProject: builder.mutation<IProject, { caseId: string }>({
      query: ({ caseId }) => ({
        url: "/projects",
        method: "POST",
        credentials: "include",
        body: { caseId },
      }),
      invalidatesTags: ["Project"],
    }),
    patchProject: builder.mutation<IProject, PatchProjectArgs>({
      query: ({ projectId, ...rest }) => ({
        url: `/projects/${projectId}`,
        method: "PATCH",
        body: rest,
        credentials: "include",
      }),
      invalidatesTags: ["Project"],
    }),
  }),
});

export const {
  useGetProjectsQuery,
  useGetProjectQuery,
  useCreateProjectMutation,
  usePatchProjectMutation,
} = projectsApi;
