import { createApi } from "@reduxjs/toolkit/query/react";
import { baseQuery } from "@/api/baseQuery";
import type { User } from "./types";

export const authApi = createApi({
  reducerPath: "authApi",
  baseQuery,
  tagTypes: ["User"],
  endpoints: (builder) => ({
    checkAuth: builder.query<User, void>({
      query: () => ({
        url: "/users/info",
        method: "GET",
        credentials: "include",
      }),
      providesTags: ["User"],
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
    logout: builder.mutation<void, void>({
      query: () => ({
        url: "/users/logout",
        method: "POST",
      }),
      invalidatesTags: ["User"],
    }),
  }),
});

export const { useCheckAuthQuery, useLoginMutation, useLogoutMutation } =
  authApi;
