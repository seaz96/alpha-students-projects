export type UserRole = "Admin" | "Manager" | "User";

export interface IUser {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  middleName: string;
  role: UserRole;
}

export interface AuthState {
  user: IUser | null;
  token: string | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}
