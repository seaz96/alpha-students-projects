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

export const formatPhoneNumber = (phone: string): string => {
  const cleaned = phone.replace(/\D/g, "");
  if (cleaned.length === 0) return "";
  const match = cleaned.slice(-10);
  if (match.length === 10)
    return `+7 (${match.slice(0, 3)}) ${match.slice(3, 6)} ${match.slice(6, 8)}-${match.slice(8, 10)}`;
  return phone;
};
