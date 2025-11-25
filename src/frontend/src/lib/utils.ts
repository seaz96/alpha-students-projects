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

export const formatter = new Intl.DateTimeFormat("ru-RU", {
  year: "numeric",
  month: "2-digit",
  day: "2-digit",
  hour: "2-digit",
  minute: "2-digit",
});
