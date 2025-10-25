export type UserRole = "admin" | "manager" | "user";

export interface User {
  id: string;
  roles: UserRole[];
  name: string;
  username: string;
}

export interface AuthState {
  user: User | null;
  token: string | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}
