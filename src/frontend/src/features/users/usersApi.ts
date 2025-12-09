import { baseQuery } from "@/api/baseQuery";
import { createApi } from "@reduxjs/toolkit/query/react";
import type { IUser } from "./types";
import { type GetResponse } from "@/types";

export const usersApi = createApi({
  reducerPath: "usersApi",
  baseQuery,
  tagTypes: ["User"],
  endpoints: (builder) => ({
    getUsers: builder.query<
      GetResponse<IUser>,
      { limit: number; offset: number; query: string }
    >({
      query: (params) => ({
        url: "/users",
        method: "GET",
        params,
        credentials: "include",
      }),
      providesTags: ["User"],
    }),
    checkAuth: builder.query<IUser, void>({
      query: () => ({
        url: "/users/current",
        method: "GET",
        credentials: "include",
      }),
      providesTags: ["User"],
    }),
    patchUserInfo: builder.mutation<IUser, Partial<IUser>>({
      query: (user) => ({
        url: `/users/current`,
        method: "PATCH",
        body: user,
        credentials: "include",
      }),
    }),
    patchUserRole: builder.mutation<IUser, { id: string; role: string }>({
      query: (user) => ({
        url: `/users/role`,
        method: "PATCH",
        body: user,
        credentials: "include",
      }),
      invalidatesTags: ["User"],
    }),
    login: builder.mutation<
      { token: string },
      { email: string; password: string }
    >({
      query: (credentials) => ({
        url: "/users/login",
        method: "POST",
        credentials: "include",
        body: credentials,
      }),
      invalidatesTags: ["User"],
    }),
    register: builder.mutation<
      { token: string },
      { email: string; password: string }
    >({
      query: (credentials) => ({
        url: "/users/register",
        method: "POST",
        credentials: "include",
        body: credentials,
      }),
      invalidatesTags: ["User"],
    }),
    logout: builder.mutation<void, void>({
      query: () => ({
        url: "/users/logout",
        method: "POST",
      }),
      invalidatesTags: ["User"],
    }),
  }),
});

export const {
  useGetUsersQuery,
  useCheckAuthQuery,
  usePatchUserInfoMutation,
  usePatchUserRoleMutation,
  useLoginMutation,
  useRegisterMutation,
  useLogoutMutation,
} = usersApi;
