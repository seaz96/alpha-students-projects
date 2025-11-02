export type UserRole = "Admin" | "Manager" | "User";

export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  middleName: string;
  role: UserRole;
}

export interface AuthState {
  user: User | null;
  token: string | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}
