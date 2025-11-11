import type { IUser } from "@/features/users/types";
import { clsx, type ClassValue } from "clsx";
import { twMerge } from "tailwind-merge";

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

export function getInitials(user: IUser): string {
  const firstNameInitial = user.firstName ? `${user.firstName[0]}.` : "";
  const middleNameInitial = user.middleName ? `${user.middleName[0]}.` : "";
  return `${user.lastName} ${firstNameInitial}${middleNameInitial}`;
}
