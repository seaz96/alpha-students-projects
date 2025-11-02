import { createAppSlice } from "@/app/createAppSlice";
import type { AuthState } from "./types";
import { usersApi } from "./usersApi";

const initialState: AuthState = {
  user: null,
  token: null,
  status: "idle",
  error: null,
};

export const usersSlice = createAppSlice({
  name: "user",
  initialState,
  reducers: {
    logout: (state) => {
      state.user = null;
      state.token = null;
      state.status = "idle";
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addMatcher(usersApi.endpoints.login.matchPending, (state) => {
        state.status = "loading";
        state.error = null;
      })
      .addMatcher(usersApi.endpoints.login.matchFulfilled, (state, action) => {
        state.status = "succeeded";
        state.token = action.payload.token;
      })
      .addMatcher(usersApi.endpoints.login.matchRejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message || "Login failed";
      })
      .addMatcher(usersApi.endpoints.checkAuth.matchPending, (state) => {
        state.status = "loading";
      })
      .addMatcher(
        usersApi.endpoints.checkAuth.matchFulfilled,
        (state, action) => {
          state.status = "succeeded";
          state.user = action.payload;
        },
      )
      .addMatcher(
        usersApi.endpoints.checkAuth.matchRejected,
        (state, action) => {
          state.status = "failed";
          state.user = null;
          state.error = action.error.message || "Auth check failed";
        },
      )
      .addMatcher(usersApi.endpoints.logout.matchFulfilled, (state) => {
        state.user = null;
        state.token = null;
        state.status = "idle";
        state.error = null;
      })
      .addMatcher(
        usersApi.endpoints.patchUserInfo.matchFulfilled,
        (state, action) => {
          state.user = action.payload;
        },
      );
  },
  selectors: {
    selectAuthStatus: (state) => state.status,
    selectUser: (state) => state.user,
    selectIsAuthenticated: (state) => !!state.user,
    selectAuthError: (state) => state.error,
    selectUserRoles: (state) => state.user?.role || [],
  },
});

export const { logout } = usersSlice.actions;
export const {
  selectAuthStatus,
  selectUser,
  selectIsAuthenticated,
  selectAuthError,
  selectUserRoles,
} = usersSlice.selectors;
