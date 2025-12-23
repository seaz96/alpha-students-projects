const API_URL = import.meta.env.VITE_API_URL;

const downloadFileFromUrl = async (endpointUrl: string) => {
  try {
    const response = await fetch(endpointUrl, {
      method: "GET",
      credentials: "include",
    });

    if (!response.ok) {
      throw new Error(
        `Ошибка загрузки: ${response.status} ${response.statusText}`,
      );
    }

    const header = response.headers.get("Content-Disposition");
    let filename = "calendar.ics";

    if (header) {
      // Ищем filename*=UTF-8''...
      const filenameStarMatch = header.match(/filename\*=UTF-8''([^;]*)/);
      // Ищем filename="..."
      const filenameMatch = header.match(/filename="?([^";]*)"?/);

      if (filenameStarMatch && filenameStarMatch[1]) {
        filename = decodeURIComponent(filenameStarMatch[1]);
      } else if (filenameMatch && filenameMatch[1]) {
        filename = filenameMatch[1];
      }
    }

    // Скачивание
    const blob = await response.blob();
    const downloadUrl = window.URL.createObjectURL(blob);

    const link = document.createElement("a");
    link.href = downloadUrl;
    link.setAttribute("download", filename);

    document.body.appendChild(link);
    link.click();

    // Очистка
    link.parentNode?.removeChild(link);
    window.URL.revokeObjectURL(downloadUrl);

    return true;
  } catch (error) {
    console.error("Download failed:", error);
    throw error;
  }
};

export const downloadTeamCalendar = async (teamId: string) => {
  return downloadFileFromUrl(`${API_URL}/calendar/${teamId}`);
};

export const downloadMentorCalendar = async (mentorId?: string) => {
  const url = mentorId
    ? `${API_URL}/calendar/mentor-calendar/${mentorId}`
    : `${API_URL}/calendar/mentor-calendar`;
  return downloadFileFromUrl(url);
};
