import type { IUser } from "../users/types";

export interface GetProjectsArgs {
  limit: number;
  offset: number;
}

export interface IProject {
  id: string;
  name: string;
  description: string;
  status: ProjectStatus;
  createdAt: string;
  author: IUser;
}

type ProjectStatus = "Unknown" | "Active" | "Archived";