import { usersSlice } from "@/features/users/usersSlice";
import type { Action, ThunkAction } from "@reduxjs/toolkit";
import { combineSlices, configureStore } from "@reduxjs/toolkit";
import { setupListeners } from "@reduxjs/toolkit/query";
import { usersApi } from "@/features/users/usersApi";
import { casesApi } from "@/features/cases/casesApi";
import { projectsApi } from "@/features/projects/projectsApi";
import { teamsApi } from "@/features/teams/teamsApi";
import { stagesApi } from "@/features/stages/stagesApi";
import { filesApi } from "@/features/files/filesApi.ts";
import { studentPositionApi } from "@/features/studentPositions/studentPositionApi";
import { meetingsApi } from "@/features/meetings/meetingsApi";
import { todosApi } from "@/features/todo/todoApi";
import { calendarApi } from "@/features/calendar/calendarApi";

const rootReducer = combineSlices(
  usersSlice,
  usersApi,
  casesApi,
  projectsApi,
  teamsApi,
  stagesApi,
  filesApi,
  studentPositionApi,
  meetingsApi,
  todosApi,
  calendarApi,
);
export type RootState = ReturnType<typeof rootReducer>;

export const makeStore = (preloadedState?: Partial<RootState>) => {
  const store = configureStore({
    reducer: rootReducer,
    middleware: (getDefaultMiddleware) => {
      return getDefaultMiddleware()
        .concat(usersApi.middleware)
        .concat(casesApi.middleware)
        .concat(projectsApi.middleware)
        .concat(teamsApi.middleware)
        .concat(stagesApi.middleware)
        .concat(filesApi.middleware)
        .concat(studentPositionApi.middleware)
        .concat(meetingsApi.middleware)
        .concat(todosApi.middleware)
        .concat(calendarApi.middleware);
    },
    preloadedState,
  });
  setupListeners(store.dispatch);
  return store;
};

export const store = makeStore();

export type AppStore = typeof store;
export type AppDispatch = AppStore["dispatch"];
export type AppThunk<ThunkReturnType = void> = ThunkAction<
  ThunkReturnType,
  RootState,
  unknown,
  Action
>;
