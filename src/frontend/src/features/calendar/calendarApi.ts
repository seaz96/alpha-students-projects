import { baseQuery } from "@/api/baseQuery";
import { createApi } from "@reduxjs/toolkit/query/react";

export const calendarApi = createApi({
  reducerPath: "calendarApi",
  baseQuery,
  endpoints: (builder) => ({
    getTeamCalendar: builder.query<null, { teamId: string }>({
      query: ({ teamId }) => ({
        url: "/calendar/" + teamId,
        credentials: "include",
      }),
    }),
    getMentorCalendar: builder.query<null, { mentorId: string | null }>({
      query: ({ mentorId }) => ({
        url: "/calendar/mentor-calendar/" + mentorId,
        credentials: "include",
      }),
    }),
  }),
});

export const { useLazyGetTeamCalendarQuery, useLazyGetMentorCalendarQuery } =
  calendarApi;
