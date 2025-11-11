import type { IUser } from "@/features/users/types";
import { clsx, type ClassValue } from "clsx";
import { twMerge } from "tailwind-merge";

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

export function getInitials(user: IUser): string {
  const firstNameInitial = user.firstName ? `${user.firstName[0]}.` : "";
  const lastNameInitial = user.lastName ? `${user.lastName[0]}.` : "";
  return `${user.middleName} ${firstNameInitial}${lastNameInitial}`;
}
