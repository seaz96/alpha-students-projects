import { usersSlice } from "@/features/users/usersSlice";
import type { Action, ThunkAction } from "@reduxjs/toolkit";
import { combineSlices, configureStore } from "@reduxjs/toolkit";
import { setupListeners } from "@reduxjs/toolkit/query";
import { usersApi } from "@/features/users/usersApi";
import { casesApi } from "@/features/cases/casesApi";
import { projectsApi } from "@/features/projects/projectsApi";
import { teamsApi } from "@/features/teams/teamsApi";

const rootReducer = combineSlices(
  usersSlice,
  usersApi,
  casesApi,
  projectsApi,
  teamsApi,
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
        .concat(teamsApi.middleware);
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
