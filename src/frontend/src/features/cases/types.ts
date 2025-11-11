import type { IUser } from "../users/types";

export interface ICase {
  id: string;
  name: string;
  description: string;
  author: IUser;
  createdAt: string;
  likes: number;
  dislikes: number;
}

export interface IReview {
  isDislike: boolean;
  comment: string;
}

export interface IReviewWithAuthor extends IReview {
  author: IUser;
}

export interface CasesState {
  cases: ICase[];
}
