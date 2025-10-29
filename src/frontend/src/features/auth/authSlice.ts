import { createAppSlice } from "@/app/createAppSlice";
import type { AuthState } from "./types";
import { authApi } from "./auth.api";

const initialState: AuthState = {
  user: null,
  token: null,
  status: "idle",
  error: null,
};

export const authSlice = createAppSlice({
  name: "auth",
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
      .addMatcher(authApi.endpoints.login.matchPending, (state) => {
        state.status = "loading";
        state.error = null;
      })
      .addMatcher(authApi.endpoints.login.matchFulfilled, (state, action) => {
        state.status = "succeeded";
        state.token = action.payload.token;
      })
      .addMatcher(authApi.endpoints.login.matchRejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message || "Login failed";
      })
      .addMatcher(authApi.endpoints.checkAuth.matchPending, (state) => {
        state.status = "loading";
      })
      .addMatcher(
        authApi.endpoints.checkAuth.matchFulfilled,
        (state, action) => {
          state.status = "succeeded";
          state.user = action.payload;
        },
      )
      .addMatcher(
        authApi.endpoints.checkAuth.matchRejected,
        (state, action) => {
          state.status = "failed";
          state.user = null;
          state.error = action.error.message || "Auth check failed";
        },
      )
      .addMatcher(authApi.endpoints.logout.matchFulfilled, (state) => {
        state.user = null;
        state.token = null;
        state.status = "idle";
        state.error = null;
      });
  },
  selectors: {
    selectAuthStatus: (state) => state.status,
    selectUser: (state) => state.user,
    selectIsAuthenticated: (state) => !!state.user,
    selectAuthError: (state) => state.error,
    selectUserRoles: (state) => state.user?.roles || [],
  },
});

export const { logout } = authSlice.actions;
export const {
  selectAuthStatus,
  selectUser,
  selectIsAuthenticated,
  selectAuthError,
  selectUserRoles,
} = authSlice.selectors;
