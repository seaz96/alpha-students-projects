import { baseQuery } from "@/api/baseQuery";
import { createApi } from "@reduxjs/toolkit/query/react";
import type { ICase, IReview, IReviewWithAuthor } from "./types";

export const casesApi = createApi({
  reducerPath: "casesApi",
  baseQuery,
  tagTypes: ["Case"],
  endpoints: (builder) => ({
    getCases: builder.query<ICase[], { limit: number; offset: number }>({
      query: ({ limit, offset }) => ({
        url: "/cases",
        method: "GET",
        params: { limit, offset },
        credentials: "include",
      }),
      providesTags: ["Case"],
    }),
    createCase: builder.mutation<ICase, Pick<ICase, "name" | "description">>({
      query: (caseData) => ({
        url: "/cases",
        method: "POST",
        body: caseData,
        credentials: "include",
      }),
      invalidatesTags: ["Case"],
    }),
    deleteCase: builder.mutation<void, string>({
      query: (id) => ({
        url: `/cases/${id}`,
        method: "DELETE",
        credentials: "include",
      }),
      invalidatesTags: ["Case"],
    }),
    getCase: builder.query<ICase, string>({
      query: (id) => ({
        url: `/cases/${id}`,
        method: "GET",
        credentials: "include",
      }),
      providesTags: ["Case"],
    }),
    reviewCase: builder.mutation<void, { id: string; review: IReview }>({
      query: ({ id, review }) => ({
        url: `/cases/${id}/reviews`,
        method: "PUT",
        body: review,
        credentials: "include",
      }),
      invalidatesTags: ["Case"],
    }),
    getReviews: builder.query<IReviewWithAuthor[], string>({
      query: (id) => ({
        url: `/cases/${id}/reviews`,
        method: "GET",
        credentials: "include",
      }),
      providesTags: ["Case"],
    }),
    changeCaseType: builder.mutation<
      void,
      { id: string; type: "Request" | "Submitted" }
    >({
      query: ({ id, type }) => ({
        url: `/cases/${id}/type`,
        method: "PUT",
        body: { type },
        credentials: "include",
      }),
      invalidatesTags: ["Case"],
    }),
  }),
});

export const {
  useGetCasesQuery,
  useCreateCaseMutation,
  useDeleteCaseMutation,
  useGetCaseQuery,
  useReviewCaseMutation,
  useGetReviewsQuery,
  useChangeCaseTypeMutation,
} = casesApi;
