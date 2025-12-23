import { baseQuery } from "@/api/baseQuery";
import type { GetResponse } from "@/types";
import { createApi } from "@reduxjs/toolkit/query/react";
import type { IPatchMeeting, IPostMeeting, IMeeting } from "./types";

export const meetingsApi = createApi({
  baseQuery,
  reducerPath: "meetings",
  tagTypes: ["Meeting"],
  endpoints: (builder) => ({
    getMeetings: builder.query<
      GetResponse<IMeeting>,
      { teamId?: string; startDate?: string; endDate?: string }
    >({
      query: (params) => ({
        url: "/meetings",
        credentials: "include",
        params,
      }),
      providesTags: (res) =>
        res
          ? [
              ...res.data.map((m) => ({ type: "Meeting" as const, id: m.id })),
              { type: "Meeting", id: "LIST" },
            ]
          : [{ type: "Meeting", id: "LIST" }],
    }),
    getMeeting: builder.query<IMeeting, { id: string }>({
      query: ({ id }) => ({
        url: "/meetings" + id,
        credentials: "include",
      }),
      providesTags: (_r, _e, { id }) => [{ type: "Meeting", id }],
    }),
    createMeeting: builder.mutation<IMeeting, IPostMeeting>({
      query: (body) => ({
        url: "/meetings",
        credentials: "include",
        method: "POST",
        body,
      }),
      invalidatesTags: [{ type: "Meeting", id: "LIST" }],
    }),
    patchMeeting: builder.mutation<IMeeting, IPatchMeeting>({
      query: ({ meetingId, ...body }) => ({
        url: "/meetings/" + meetingId,
        method: "PATCH",
        credentials: "include",
        body,
      }),
      invalidatesTags: (_r, _e, { meetingId }) => [
        { type: "Meeting", id: meetingId },
      ],
    }),
  }),
});

export const {
  useGetMeetingQuery,
  useGetMeetingsQuery,
  useCreateMeetingMutation,
  usePatchMeetingMutation,
} = meetingsApi;
